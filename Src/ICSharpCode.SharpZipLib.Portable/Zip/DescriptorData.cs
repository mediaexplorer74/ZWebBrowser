// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.DescriptorData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  public class DescriptorData
  {
    private long size;
    private long compressedSize;
    private long crc;

    public long CompressedSize
    {
      get => this.compressedSize;
      set => this.compressedSize = value;
    }

    public long Size
    {
      get => this.size;
      set => this.size = value;
    }

    public long Crc
    {
      get => this.crc;
      set => this.crc = value & (long) uint.MaxValue;
    }
  }
}
