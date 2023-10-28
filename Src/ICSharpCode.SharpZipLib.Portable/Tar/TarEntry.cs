// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarEntry
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.Linq;

namespace ICSharpCode.SharpZipLib.Tar
{
  public class TarEntry : ICloneable
  {
    private string file;
    private TarHeader header;

    private TarEntry() => this.header = new TarHeader();

    public TarEntry(byte[] headerBuffer)
    {
      this.header = new TarHeader();
      this.header.ParseBuffer(headerBuffer);
    }

    public TarEntry(TarHeader header) => this.header = header != null ? (TarHeader) header.Clone() : throw new ArgumentNullException(nameof (header));

    public object Clone() => (object) new TarEntry()
    {
      file = this.file,
      header = (TarHeader) this.header.Clone(),
      Name = this.Name
    };

    public static TarEntry CreateTarEntry(string name)
    {
      TarEntry tarEntry = new TarEntry();
      TarEntry.NameTarHeader(tarEntry.header, name);
      return tarEntry;
    }

    public static TarEntry CreateEntryFromFile(string fileName)
    {
      TarEntry entryFromFile = new TarEntry();
      entryFromFile.GetFileTarHeader(entryFromFile.header, fileName);
      return entryFromFile;
    }

    public override bool Equals(object obj) => obj is TarEntry tarEntry && this.Name.Equals(tarEntry.Name);

    public override int GetHashCode() => this.Name.GetHashCode();

    public bool IsDescendent(TarEntry toTest) => toTest != null ? toTest.Name.StartsWith(this.Name) : throw new ArgumentNullException(nameof (toTest));

    public TarHeader TarHeader => this.header;

    public string Name
    {
      get => this.header.Name;
      set => this.header.Name = value;
    }

    public int UserId
    {
      get => this.header.UserId;
      set => this.header.UserId = value;
    }

    public int GroupId
    {
      get => this.header.GroupId;
      set => this.header.GroupId = value;
    }

    public string UserName
    {
      get => this.header.UserName;
      set => this.header.UserName = value;
    }

    public string GroupName
    {
      get => this.header.GroupName;
      set => this.header.GroupName = value;
    }

    public void SetIds(int userId, int groupId)
    {
      this.UserId = userId;
      this.GroupId = groupId;
    }

    public void SetNames(string userName, string groupName)
    {
      this.UserName = userName;
      this.GroupName = groupName;
    }

    public DateTime ModTime
    {
      get => this.header.ModTime;
      set => this.header.ModTime = value;
    }

    public string File => this.file;

    public long Size
    {
      get => this.header.Size;
      set => this.header.Size = value;
    }

    public bool IsDirectory
    {
      get
      {
        if (this.file != null)
          return VFS.Current.DirectoryExists(this.file);
        return this.header != null && (this.header.TypeFlag == (byte) 53 || this.Name.EndsWith("/"));
      }
    }

    public void GetFileTarHeader(TarHeader header, string file)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      this.file = file != null ? file : throw new ArgumentNullException(nameof (file));
      string str1 = file;
      if (str1.IndexOf(VFS.Current.CurrentDirectory) == 0)
        str1 = str1.Substring(VFS.Current.CurrentDirectory.Length);
      string str2 = str1.Replace(VFS.Current.DirectorySeparatorChar, '/');
      while (str2.StartsWith("/"))
        str2 = str2.Substring(1);
      header.LinkName = string.Empty;
      header.Name = str2;
      if (VFS.Current.DirectoryExists(file))
      {
        header.Mode = 1003;
        header.TypeFlag = (byte) 53;
        if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
          header.Name += "/";
        header.Size = 0L;
      }
      else
      {
        header.Mode = 33216;
        header.TypeFlag = (byte) 48;
        header.Size = VFS.Current.GetFileInfo(file.Replace('/', VFS.Current.DirectorySeparatorChar)).Length;
      }
      header.ModTime = VFS.Current.GetFileInfo(file.Replace('/', VFS.Current.DirectorySeparatorChar)).LastWriteTime.ToUniversalTime();
      header.DevMajor = 0;
      header.DevMinor = 0;
    }

    public TarEntry[] GetDirectoryEntries()
    {
      if (this.file == null || !VFS.Current.DirectoryExists(this.file))
        return new TarEntry[0];
      string[] array = VFS.Current.GetDirectoriesAndFiles(this.file).ToArray<string>();
      TarEntry[] directoryEntries = new TarEntry[array.Length];
      for (int index = 0; index < array.Length; ++index)
        directoryEntries[index] = TarEntry.CreateEntryFromFile(array[index]);
      return directoryEntries;
    }

    public void WriteEntryHeader(byte[] outBuffer) => this.header.WriteHeader(outBuffer);

    public static void AdjustEntryName(byte[] buffer, string newName) => TarHeader.GetNameBytes(newName, buffer, 0, 100);

    public static void NameTarHeader(TarHeader header, string name)
    {
      if (header == null)
        throw new ArgumentNullException(nameof (header));
      bool flag = name != null ? name.EndsWith("/") : throw new ArgumentNullException(nameof (name));
      header.Name = name;
      header.Mode = flag ? 1003 : 33216;
      header.UserId = 0;
      header.GroupId = 0;
      header.Size = 0L;
      header.ModTime = DateTime.UtcNow;
      header.TypeFlag = flag ? (byte) 53 : (byte) 48;
      header.LinkName = string.Empty;
      header.UserName = string.Empty;
      header.GroupName = string.Empty;
      header.DevMajor = 0;
      header.DevMinor = 0;
    }
  }
}
