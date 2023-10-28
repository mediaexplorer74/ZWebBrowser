// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.EncryptionAlgorithm
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  public enum EncryptionAlgorithm
  {
    None = 0,
    PkzipClassic = 1,
    Des = 26113, // 0x00006601
    RC2 = 26114, // 0x00006602
    TripleDes168 = 26115, // 0x00006603
    TripleDes112 = 26121, // 0x00006609
    Aes128 = 26126, // 0x0000660E
    Aes192 = 26127, // 0x0000660F
    Aes256 = 26128, // 0x00006610
    RC2Corrected = 26370, // 0x00006702
    Blowfish = 26400, // 0x00006720
    Twofish = 26401, // 0x00006721
    RC4 = 26625, // 0x00006801
    Unknown = 65535, // 0x0000FFFF
  }
}
