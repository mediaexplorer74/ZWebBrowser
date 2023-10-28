// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanFailureEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Core
{
  public class ScanFailureEventArgs : EventArgs
  {
    private string name_;
    private Exception exception_;
    private bool continueRunning_;

    public ScanFailureEventArgs(string name, Exception e)
    {
      this.name_ = name;
      this.exception_ = e;
      this.continueRunning_ = true;
    }

    public string Name => this.name_;

    public Exception Exception => this.exception_;

    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }
  }
}
