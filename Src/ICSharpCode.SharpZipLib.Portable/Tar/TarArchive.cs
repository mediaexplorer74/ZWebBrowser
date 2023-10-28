// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarArchive
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.IO;
using FileAttributes = ICSharpCode.SharpZipLib.VirtualFileSystem.FileAttributes;

namespace ICSharpCode.SharpZipLib.Tar
{
  public class TarArchive : IDisposable
  {
    private bool keepOldFiles;
    private bool asciiTranslate;
    private int userId;
    private string userName = string.Empty;
    private int groupId;
    private string groupName = string.Empty;
    private string rootPath;
    private string pathPrefix;
    private bool applyUserInfoOverrides;
    private TarInputStream tarIn;
    private TarOutputStream tarOut;
    private bool isDisposed;

    public event ProgressMessageHandler ProgressMessageEvent;

    protected virtual void OnProgressMessageEvent(TarEntry entry, string message)
    {
      ProgressMessageHandler progressMessageEvent = this.ProgressMessageEvent;
      if (progressMessageEvent == null)
        return;
      progressMessageEvent(this, entry, message);
    }

    protected TarArchive()
    {
    }

    protected TarArchive(TarInputStream stream) => this.tarIn = stream != null ? stream : throw new ArgumentNullException(nameof (stream));

    protected TarArchive(TarOutputStream stream) => this.tarOut = stream != null ? stream : throw new ArgumentNullException(nameof (stream));

    public static TarArchive CreateInputTarArchive(Stream inputStream)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      return !(inputStream is TarInputStream stream) ? TarArchive.CreateInputTarArchive(inputStream, 20) : new TarArchive(stream);
    }

    public static TarArchive CreateInputTarArchive(Stream inputStream, int blockFactor)
    {
      if (inputStream == null)
        throw new ArgumentNullException(nameof (inputStream));
      return !(inputStream is TarInputStream) ? new TarArchive(new TarInputStream(inputStream, blockFactor)) : throw new ArgumentException("TarInputStream not valid");
    }

    public static TarArchive CreateOutputTarArchive(Stream outputStream)
    {
      if (outputStream == null)
        throw new ArgumentNullException(nameof (outputStream));
      return !(outputStream is TarOutputStream stream) ? TarArchive.CreateOutputTarArchive(outputStream, 20) : new TarArchive(stream);
    }

    public static TarArchive CreateOutputTarArchive(Stream outputStream, int blockFactor)
    {
      if (outputStream == null)
        throw new ArgumentNullException(nameof (outputStream));
      return !(outputStream is TarOutputStream) ? new TarArchive(new TarOutputStream(outputStream, blockFactor)) : throw new ArgumentException("TarOutputStream is not valid");
    }

    public void SetKeepOldFiles(bool keepExistingFiles)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.keepOldFiles = keepExistingFiles;
    }

    public bool AsciiTranslate
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.asciiTranslate;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.asciiTranslate = value;
      }
    }

    [Obsolete("Use the AsciiTranslate property")]
    public void SetAsciiTranslation(bool translateAsciiFiles)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.asciiTranslate = translateAsciiFiles;
    }

    public string PathPrefix
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.pathPrefix;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.pathPrefix = value;
      }
    }

    public string RootPath
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.rootPath;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.rootPath = value.Replace('\\', '/').TrimEnd('/');
      }
    }

    public void SetUserInfo(int userId, string userName, int groupId, string groupName)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      this.userId = userId;
      this.userName = userName;
      this.groupId = groupId;
      this.groupName = groupName;
      this.applyUserInfoOverrides = true;
    }

    public bool ApplyUserInfoOverrides
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.applyUserInfoOverrides;
      }
      set
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        this.applyUserInfoOverrides = value;
      }
    }

    public int UserId
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.userId;
      }
    }

    public string UserName
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.userName;
      }
    }

    public int GroupId
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.groupId;
      }
    }

    public string GroupName
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        return this.groupName;
      }
    }

    public int RecordSize
    {
      get
      {
        if (this.isDisposed)
          throw new ObjectDisposedException(nameof (TarArchive));
        if (this.tarIn != null)
          return this.tarIn.RecordSize;
        return this.tarOut != null ? this.tarOut.RecordSize : 10240;
      }
    }

    public bool IsStreamOwner
    {
      set
      {
        if (this.tarIn != null)
          this.tarIn.IsStreamOwner = value;
        else
          this.tarOut.IsStreamOwner = value;
      }
    }

    [Obsolete("Use Close instead")]
    public void CloseArchive() => this.Close();

    public void ListContents()
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      while (true)
      {
        TarEntry nextEntry = this.tarIn.GetNextEntry();
        if (nextEntry != null)
          this.OnProgressMessageEvent(nextEntry, (string) null);
        else
          break;
      }
    }

    public void ExtractContents(string destinationDirectory)
    {
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      while (true)
      {
        TarEntry nextEntry = this.tarIn.GetNextEntry();
        if (nextEntry != null)
          this.ExtractEntry(destinationDirectory, nextEntry);
        else
          break;
      }
    }

    private void ExtractEntry(string destDir, TarEntry entry)
    {
      this.OnProgressMessageEvent(entry, (string) null);
      string path = entry.Name;
      if (Path.IsPathRooted(path))
        path = path.Substring(Path.GetPathRoot(path).Length);
      string str1 = path.Replace('/', VFS.Current.DirectorySeparatorChar);
      string str2 = Path.Combine(destDir, str1);
      if (entry.IsDirectory)
      {
        TarArchive.EnsureDirectoryExists(str2);
      }
      else
      {
        TarArchive.EnsureDirectoryExists(Path.GetDirectoryName(str2));
        bool flag1 = true;
        IFileInfo fileInfo = VFS.Current.GetFileInfo(str2);
        if (fileInfo.Exists)
        {
          if (this.keepOldFiles)
          {
            this.OnProgressMessageEvent(entry, "Destination file already exists");
            flag1 = false;
          }
          else if ((fileInfo.Attributes & FileAttributes.ReadOnly) != (FileAttributes) 0)
          {
            this.OnProgressMessageEvent(entry, "Destination file already exists, and is read-only");
            flag1 = false;
          }
        }
        if (!flag1)
          return;
        bool flag2 = false;
        Stream file = (Stream) VFS.Current.CreateFile(str2);
        if (this.asciiTranslate)
          flag2 = !TarArchive.IsBinary(str2);
        StreamWriter streamWriter = (StreamWriter) null;
        if (flag2)
          streamWriter = new StreamWriter(file);
        byte[] numArray = new byte[32768];
label_15:
        int count;
        while (true)
        {
          count = this.tarIn.Read(numArray, 0, numArray.Length);
          if (count > 0)
          {
            if (!flag2)
              file.Write(numArray, 0, count);
            else
              break;
          }
          else
            goto label_24;
        }
        int index1 = 0;
        for (int index2 = 0; index2 < count; ++index2)
        {
          if (numArray[index2] == (byte) 10)
          {
            string str3 = AsciiEncoding.Default.GetString(numArray, index1, index2 - index1);
            streamWriter.WriteLine(str3);
            index1 = index2 + 1;
          }
        }
        goto label_15;
label_24:
        if (flag2)
          streamWriter.Dispose();
        else
          file.Dispose();
      }
    }

    public void WriteEntry(TarEntry sourceEntry, bool recurse)
    {
      if (sourceEntry == null)
        throw new ArgumentNullException(nameof (sourceEntry));
      if (this.isDisposed)
        throw new ObjectDisposedException(nameof (TarArchive));
      try
      {
        if (recurse)
          TarHeader.SetValueDefaults(sourceEntry.UserId, sourceEntry.UserName, sourceEntry.GroupId, sourceEntry.GroupName);
        this.WriteEntryCore(sourceEntry, recurse);
      }
      finally
      {
        if (recurse)
          TarHeader.RestoreSetValues();
      }
    }

    private void WriteEntryCore(TarEntry sourceEntry, bool recurse)
    {
      string str1 = (string) null;
      string filename = sourceEntry.File;
      TarEntry entry = (TarEntry) sourceEntry.Clone();
      if (this.applyUserInfoOverrides)
      {
        entry.GroupId = this.groupId;
        entry.GroupName = this.groupName;
        entry.UserId = this.userId;
        entry.UserName = this.userName;
      }
      this.OnProgressMessageEvent(entry, (string) null);
      if (this.asciiTranslate && !entry.IsDirectory && !TarArchive.IsBinary(filename))
      {
        str1 = VFS.Current.GetTempFileName();
        using (StreamReader streamReader = new StreamReader((Stream) VFS.Current.OpenReadFile(filename)))
        {
          using (Stream file = (Stream) VFS.Current.CreateFile(str1))
          {
            while (true)
            {
              string s = streamReader.ReadLine();
              if (s != null)
              {
                byte[] bytes = AsciiEncoding.Default.GetBytes(s);
                file.Write(bytes, 0, bytes.Length);
                file.WriteByte((byte) 10);
              }
              else
                break;
            }
            file.Flush();
          }
        }
        entry.Size = VFS.Current.GetFileInfo(str1).Length;
        filename = str1;
      }
      string str2 = (string) null;
      if (this.rootPath != null && entry.Name.StartsWith(this.rootPath, StringComparison.OrdinalIgnoreCase))
        str2 = entry.Name.Substring(this.rootPath.Length + 1);
      if (this.pathPrefix != null)
        str2 = str2 == null ? this.pathPrefix + "/" + entry.Name : this.pathPrefix + "/" + str2;
      if (str2 != null)
        entry.Name = str2;
      this.tarOut.PutNextEntry(entry);
      if (entry.IsDirectory)
      {
        if (!recurse)
          return;
        foreach (TarEntry directoryEntry in entry.GetDirectoryEntries())
          this.WriteEntryCore(directoryEntry, recurse);
      }
      else
      {
        using (Stream stream = (Stream) VFS.Current.OpenReadFile(filename))
        {
          byte[] buffer = new byte[32768];
          while (true)
          {
            int count = stream.Read(buffer, 0, buffer.Length);
            if (count > 0)
              this.tarOut.Write(buffer, 0, count);
            else
              break;
          }
        }
        if (str1 != null && str1.Length > 0)
          VFS.Current.DeleteFile(str1);
        this.tarOut.CloseEntry();
      }
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (this.isDisposed)
        return;
      this.isDisposed = true;
      if (!disposing)
        return;
      if (this.tarOut != null)
      {
        this.tarOut.Flush();
        this.tarOut.Dispose();
      }
      if (this.tarIn == null)
        return;
      this.tarIn.Dispose();
    }

    public virtual void Close() => this.Dispose(true);

    ~TarArchive() => this.Dispose(false);

    private static void EnsureDirectoryExists(string directoryName)
    {
      if (VFS.Current.DirectoryExists(directoryName))
        return;
      try
      {
        VFS.Current.CreateDirectory(directoryName);
      }
      catch (Exception ex)
      {
        throw new TarException("Exception creating directory '" + directoryName + "', " + ex.Message, ex);
      }
    }

    private static bool IsBinary(string filename)
    {
      using (Stream stream = (Stream) VFS.Current.OpenReadFile(filename))
      {
        int count = Math.Min(4096, (int) stream.Length);
        byte[] buffer = new byte[count];
        int num1 = stream.Read(buffer, 0, count);
        for (int index = 0; index < num1; ++index)
        {
          byte num2 = buffer[index];
          if (num2 < (byte) 8 || num2 > (byte) 13 && num2 < (byte) 32 || num2 == byte.MaxValue)
            return true;
        }
      }
      return false;
    }
  }
}
