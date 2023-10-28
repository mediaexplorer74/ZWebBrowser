// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.FileSystemScanner
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.Linq;

namespace ICSharpCode.SharpZipLib.Core
{
  public class FileSystemScanner
  {
    public ProcessDirectoryHandler ProcessDirectory;
    public ProcessFileHandler ProcessFile;
    public CompletedFileHandler CompletedFile;
    public DirectoryFailureHandler DirectoryFailure;
    public FileFailureHandler FileFailure;
    private IScanFilter fileFilter_;
    private IScanFilter directoryFilter_;
    private bool alive_;

    public FileSystemScanner(string filter) => this.fileFilter_ = (IScanFilter) new PathFilter(filter);

    public FileSystemScanner(string fileFilter, string directoryFilter)
    {
      this.fileFilter_ = (IScanFilter) new PathFilter(fileFilter);
      this.directoryFilter_ = (IScanFilter) new PathFilter(directoryFilter);
    }

    public FileSystemScanner(IScanFilter fileFilter) => this.fileFilter_ = fileFilter;

    public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
    {
      this.fileFilter_ = fileFilter;
      this.directoryFilter_ = directoryFilter;
    }

    private bool OnDirectoryFailure(string directory, Exception e)
    {
      DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
      bool flag = directoryFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
        directoryFailure((object) this, e1);
        this.alive_ = e1.ContinueRunning;
      }
      return flag;
    }

    private bool OnFileFailure(string file, Exception e)
    {
      bool flag = this.FileFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
        this.FileFailure((object) this, e1);
        this.alive_ = e1.ContinueRunning;
      }
      return flag;
    }

    private void OnProcessFile(string file)
    {
      ProcessFileHandler processFile = this.ProcessFile;
      if (processFile == null)
        return;
      ScanEventArgs e = new ScanEventArgs(file);
      processFile((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    private void OnCompleteFile(string file)
    {
      CompletedFileHandler completedFile = this.CompletedFile;
      if (completedFile == null)
        return;
      ScanEventArgs e = new ScanEventArgs(file);
      completedFile((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    private void OnProcessDirectory(string directory, bool hasMatchingFiles)
    {
      ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
      if (processDirectory == null)
        return;
      DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
      processDirectory((object) this, e);
      this.alive_ = e.ContinueRunning;
    }

    public void Scan(string directory, bool recurse)
    {
      this.alive_ = true;
      this.ScanDir(directory, recurse);
    }

    private void ScanDir(string directory, bool recurse)
    {
      try
      {
        string[] array = VFS.Current.GetFiles(directory).ToArray<string>();
        bool hasMatchingFiles = false;
        for (int index = 0; index < array.Length; ++index)
        {
          if (!this.fileFilter_.IsMatch(array[index]))
            array[index] = (string) null;
          else
            hasMatchingFiles = true;
        }
        this.OnProcessDirectory(directory, hasMatchingFiles);
        if (this.alive_)
        {
          if (hasMatchingFiles)
          {
            foreach (string file in array)
            {
              try
              {
                if (file != null)
                {
                  this.OnProcessFile(file);
                  if (!this.alive_)
                    break;
                }
              }
              catch (Exception ex)
              {
                if (!this.OnFileFailure(file, ex))
                  throw;
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (!this.OnDirectoryFailure(directory, ex))
          throw;
      }
      if (!this.alive_)
        return;
      if (!recurse)
        return;
      try
      {
        foreach (string str in VFS.Current.GetDirectories(directory).ToArray<string>())
        {
          if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(str))
          {
            this.ScanDir(str, true);
            if (!this.alive_)
              break;
          }
        }
      }
      catch (Exception ex)
      {
        if (this.OnDirectoryFailure(directory, ex))
          return;
        throw;
      }
    }
  }
}
