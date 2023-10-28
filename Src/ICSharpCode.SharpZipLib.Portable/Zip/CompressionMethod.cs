// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.CompressionMethod
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  public enum CompressionMethod
  {
    Stored = 0,
    Deflated = 8,
    Deflate64 = 9,
    BZip2 = 11, // 0x0000000B
    WinZipAES = 99, // 0x00000063
  }
}
