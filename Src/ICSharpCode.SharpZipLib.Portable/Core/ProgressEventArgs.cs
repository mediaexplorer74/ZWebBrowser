// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ProgressEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Core
{
  public class ProgressEventArgs : EventArgs
  {
    private string name_;
    private long processed_;
    private long target_;
    private bool continueRunning_ = true;

    public ProgressEventArgs(string name, long processed, long target)
    {
      this.name_ = name;
      this.processed_ = processed;
      this.target_ = target;
    }

    public string Name => this.name_;

    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }

    public float PercentComplete => this.target_ > 0L ? (float) ((double) this.processed_ / (double) this.target_ * 100.0) : 0.0f;

    public long Processed => this.processed_;

    public long Target => this.target_;
  }
}
