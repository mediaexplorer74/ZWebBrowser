// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DiskArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class DiskArchiveStorage : BaseArchiveStorage
  {
    private Stream temporaryStream_;
    private string fileName_;
    private string temporaryName_;

    public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
      : base(updateMode)
    {
      this.fileName_ = file.Name != null ? file.Name : throw new ZipException("Cant handle non file archives");
    }

    public DiskArchiveStorage(ZipFile file)
      : this(file, FileUpdateMode.Safe)
    {
    }

    public override Stream GetTemporaryOutput()
    {
      if (this.temporaryName_ != null)
      {
        this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.temporaryName_, true);
        this.temporaryStream_ = (Stream) VFS.Current.CreateFile(this.temporaryName_);
      }
      else
      {
        this.temporaryName_ = VFS.Current.GetTempFileName();
        this.temporaryStream_ = (Stream) VFS.Current.CreateFile(this.temporaryName_);
      }
      return this.temporaryStream_;
    }

    public override Stream ConvertTemporaryToFinal()
    {
      if (this.temporaryStream_ == null)
        throw new ZipException("No temporary stream has been created");
      Stream stream = (Stream) null;
      string tempFileName = DiskArchiveStorage.GetTempFileName(this.fileName_, false);
      bool flag = false;
      try
      {
        this.temporaryStream_.Dispose();
        VFS.Current.MoveFile(this.fileName_, tempFileName);
        VFS.Current.MoveFile(this.temporaryName_, this.fileName_);
        flag = true;
        VFS.Current.DeleteFile(tempFileName);
        return (Stream) VFS.Current.OpenReadFile(this.fileName_);
      }
      catch (Exception ex)
      {
        stream = (Stream) null;
        if (!flag)
        {
          VFS.Current.MoveFile(tempFileName, this.fileName_);
          VFS.Current.DeleteFile(this.temporaryName_);
        }
        throw;
      }
    }

    public override Stream MakeTemporaryCopy(Stream stream)
    {
      stream.Dispose();
      this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.fileName_, true);
      VFS.Current.CopyFile(this.fileName_, this.temporaryName_, true);
      this.temporaryStream_ = (Stream) VFS.Current.OpenReadFile(this.temporaryName_);
      return this.temporaryStream_;
    }

    public override Stream OpenForDirectUpdate(Stream stream)
    {
      Stream stream1;
      if (stream == null || !stream.CanWrite)
      {
        stream?.Dispose();
        stream1 = (Stream) VFS.Current.OpenReadFile(this.fileName_);
      }
      else
        stream1 = stream;
      return stream1;
    }

    public override void Dispose()
    {
      if (this.temporaryStream_ == null)
        return;
      this.temporaryStream_.Dispose();
    }

    private static string GetTempFileName(string original, bool makeTempFile)
    {
      string tempFileName = (string) null;
      if (original == null)
      {
        tempFileName = VFS.Current.GetTempFileName();
      }
      else
      {
        int num = 0;
        int second = DateTime.Now.Second;
        while (tempFileName == null)
        {
          ++num;
          string str = string.Format("{0}.{1}{2}.tmp", (object) original, (object) second, (object) num);
          if (!VFS.Current.FileExists(str))
          {
            if (makeTempFile)
            {
              try
              {
                using ((Stream) VFS.Current.CreateFile(str))
                  ;
                tempFileName = str;
              }
              catch
              {
                second = DateTime.Now.Second;
              }
            }
            else
              tempFileName = str;
          }
        }
      }
      return tempFileName;
    }
  }
}
