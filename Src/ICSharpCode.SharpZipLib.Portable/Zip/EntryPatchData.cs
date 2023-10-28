// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EntryPatchData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  internal class EntryPatchData
  {
    private long sizePatchOffset_;
    private long crcPatchOffset_;

    public long SizePatchOffset
    {
      get => this.sizePatchOffset_;
      set => this.sizePatchOffset_ = value;
    }

    public long CrcPatchOffset
    {
      get => this.crcPatchOffset_;
      set => this.crcPatchOffset_ = value;
    }
  }
}
