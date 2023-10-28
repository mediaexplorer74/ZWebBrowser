// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Core.DirectoryEventArgs
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Core
{
  public class DirectoryEventArgs : ScanEventArgs
  {
    private bool hasMatchingFiles_;

    public DirectoryEventArgs(string name, bool hasMatchingFiles)
      : base(name)
    {
      this.hasMatchingFiles_ = hasMatchingFiles;
    }

    public bool HasMatchingFiles => this.hasMatchingFiles_;
  }
}
