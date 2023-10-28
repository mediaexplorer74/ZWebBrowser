// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Checksums.IChecksum
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Checksums
{
  public interface IChecksum
  {
    long Value { get; }

    void Reset();

    void Update(int value);

    void Update(byte[] buffer);

    void Update(byte[] buffer, int offset, int count);
  }
}
