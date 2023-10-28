// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.StaticDiskDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class StaticDiskDataSource : IStaticDataSource
  {
    private string fileName_;

    public StaticDiskDataSource(string fileName) => this.fileName_ = fileName;

    public Stream GetSource() => (Stream) VFS.Current.OpenReadFile(this.fileName_);
  }
}
