// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipEntryFactory
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.VirtualFileSystem;
using System;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class ZipEntryFactory : IEntryFactory
  {
    private INameTransform nameTransform_;
    private DateTime fixedDateTime_ = DateTime.Now;
    private ZipEntryFactory.TimeSetting timeSetting_;
    private bool isUnicodeText_;
    private int getAttributes_ = -1;
    private int setAttributes_;

    public ZipEntryFactory() => this.nameTransform_ = (INameTransform) new ZipNameTransform();

    public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
    {
      this.timeSetting_ = timeSetting;
      this.nameTransform_ = (INameTransform) new ZipNameTransform();
    }

    public ZipEntryFactory(DateTime time)
    {
      this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
      this.FixedDateTime = time;
      this.nameTransform_ = (INameTransform) new ZipNameTransform();
    }

    public INameTransform NameTransform
    {
      get => this.nameTransform_;
      set
      {
        if (value == null)
          this.nameTransform_ = (INameTransform) new ZipNameTransform();
        else
          this.nameTransform_ = value;
      }
    }

    public ZipEntryFactory.TimeSetting Setting
    {
      get => this.timeSetting_;
      set => this.timeSetting_ = value;
    }

    public DateTime FixedDateTime
    {
      get => this.fixedDateTime_;
      set => this.fixedDateTime_ = value.Year >= 1970 ? value : throw new ArgumentException("Value is too old to be valid", nameof (value));
    }

    public int GetAttributes
    {
      get => this.getAttributes_;
      set => this.getAttributes_ = value;
    }

    public int SetAttributes
    {
      get => this.setAttributes_;
      set => this.setAttributes_ = value;
    }

    public bool IsUnicodeText
    {
      get => this.isUnicodeText_;
      set => this.isUnicodeText_ = value;
    }

    public ZipEntry MakeFileEntry(string fileName) => this.MakeFileEntry(fileName, (string) null, true);

    public ZipEntry MakeFileEntry(string fileName, bool useFileSystem) => this.MakeFileEntry(fileName, (string) null, useFileSystem);

    public ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem)
    {
      ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile(entryName == null || entryName.Length <= 0 ? fileName : entryName));
      zipEntry.IsUnicodeText = this.isUnicodeText_;
      int num1 = 0;
      bool flag = this.setAttributes_ != 0;
      IFileInfo fileInfo = (IFileInfo) null;
      if (useFileSystem)
        fileInfo = VFS.Current.GetFileInfo(fileName);
      if (fileInfo != null && fileInfo.Exists)
      {
        switch (this.timeSetting_)
        {
          case ZipEntryFactory.TimeSetting.LastWriteTime:
            zipEntry.DateTime = fileInfo.LastWriteTime;
            break;
          case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
            zipEntry.DateTime = fileInfo.LastWriteTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.CreateTime:
            zipEntry.DateTime = fileInfo.CreationTime;
            break;
          case ZipEntryFactory.TimeSetting.CreateTimeUtc:
            zipEntry.DateTime = fileInfo.CreationTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTime:
            zipEntry.DateTime = fileInfo.LastAccessTime;
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
            zipEntry.DateTime = fileInfo.LastAccessTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.Fixed:
            zipEntry.DateTime = this.fixedDateTime_;
            break;
          default:
            throw new ZipException("Unhandled time setting in MakeFileEntry");
        }
        zipEntry.Size = fileInfo.Length;
        flag = true;
        num1 = (int) (fileInfo.Attributes & (FileAttributes) this.getAttributes_);
      }
      else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
        zipEntry.DateTime = this.fixedDateTime_;
      if (flag)
      {
        int num2 = num1 | this.setAttributes_;
        zipEntry.ExternalFileAttributes = num2;
      }
      return zipEntry;
    }

    public ZipEntry MakeDirectoryEntry(string directoryName) => this.MakeDirectoryEntry(directoryName, true);

    public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
    {
      ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
      zipEntry.IsUnicodeText = this.isUnicodeText_;
      zipEntry.Size = 0L;
      int num1 = 0;
      IDirectoryInfo directoryInfo = (IDirectoryInfo) null;
      if (useFileSystem)
        directoryInfo = VFS.Current.GetDirectoryInfo(directoryName);
      if (directoryInfo != null && directoryInfo.Exists)
      {
        switch (this.timeSetting_)
        {
          case ZipEntryFactory.TimeSetting.LastWriteTime:
            zipEntry.DateTime = directoryInfo.LastWriteTime;
            break;
          case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
            zipEntry.DateTime = directoryInfo.LastWriteTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.CreateTime:
            zipEntry.DateTime = directoryInfo.CreationTime;
            break;
          case ZipEntryFactory.TimeSetting.CreateTimeUtc:
            zipEntry.DateTime = directoryInfo.CreationTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTime:
            zipEntry.DateTime = directoryInfo.LastAccessTime;
            break;
          case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
            zipEntry.DateTime = directoryInfo.LastAccessTime.ToUniversalTime();
            break;
          case ZipEntryFactory.TimeSetting.Fixed:
            zipEntry.DateTime = this.fixedDateTime_;
            break;
          default:
            throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
        }
        num1 = (int) (directoryInfo.Attributes & (FileAttributes) this.getAttributes_);
      }
      else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
        zipEntry.DateTime = this.fixedDateTime_;
      int num2 = num1 | this.setAttributes_ | 16;
      zipEntry.ExternalFileAttributes = num2;
      return zipEntry;
    }

    public enum TimeSetting
    {
      LastWriteTime,
      LastWriteTimeUtc,
      CreateTime,
      CreateTimeUtc,
      LastAccessTime,
      LastAccessTimeUtc,
      Fixed,
    }
  }
}
