// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.ScanEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Core
{
  public class ScanEventArgs : EventArgs
  {
    private string name_;
    private bool continueRunning_ = true;

    public ScanEventArgs(string name) => this.name_ = name;

    public string Name => this.name_;

    public bool ContinueRunning
    {
      get => this.continueRunning_;
      set => this.continueRunning_ = value;
    }
  }
}
