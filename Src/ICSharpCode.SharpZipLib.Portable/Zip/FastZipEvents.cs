// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.FastZipEvents
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Core;
using System;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class FastZipEvents
  {
    public ProcessDirectoryHandler ProcessDirectory;
    public ProcessFileHandler ProcessFile;
    public ProgressHandler Progress;
    public CompletedFileHandler CompletedFile;
    public DirectoryFailureHandler DirectoryFailure;
    public FileFailureHandler FileFailure;
    private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);

    public bool OnDirectoryFailure(string directory, Exception e)
    {
      bool flag = false;
      DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
      if (directoryFailure != null)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
        directoryFailure((object) this, e1);
        flag = e1.ContinueRunning;
      }
      return flag;
    }

    public bool OnFileFailure(string file, Exception e)
    {
      FileFailureHandler fileFailure = this.FileFailure;
      bool flag = fileFailure != null;
      if (flag)
      {
        ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
        fileFailure((object) this, e1);
        flag = e1.ContinueRunning;
      }
      return flag;
    }

    public bool OnProcessFile(string file)
    {
      bool flag = true;
      ProcessFileHandler processFile = this.ProcessFile;
      if (processFile != null)
      {
        ScanEventArgs e = new ScanEventArgs(file);
        processFile((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    public bool OnCompletedFile(string file)
    {
      bool flag = true;
      CompletedFileHandler completedFile = this.CompletedFile;
      if (completedFile != null)
      {
        ScanEventArgs e = new ScanEventArgs(file);
        completedFile((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
    {
      bool flag = true;
      ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
      if (processDirectory != null)
      {
        DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
        processDirectory((object) this, e);
        flag = e.ContinueRunning;
      }
      return flag;
    }

    public TimeSpan ProgressInterval
    {
      get => this.progressInterval_;
      set => this.progressInterval_ = value;
    }
  }
}
