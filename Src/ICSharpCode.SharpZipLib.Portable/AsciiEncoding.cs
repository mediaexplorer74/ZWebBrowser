// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.AsciiEncoding
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System.Text;

namespace ICSharpCode.SharpZipLib
{
  public class AsciiEncoding : Encoding
  {
    public static readonly AsciiEncoding Default = new AsciiEncoding();

    public override int GetByteCount(char[] chars, int index, int count) => count;

    public override int GetBytes(
      char[] chars,
      int charIndex,
      int charCount,
      byte[] bytes,
      int byteIndex)
    {
      for (int index = 0; index < charCount; ++index)
        bytes[byteIndex + index] = (byte) chars[charIndex + index];
      return charCount;
    }

    public override int GetCharCount(byte[] bytes, int index, int count) => count;

    public override int GetChars(
      byte[] bytes,
      int byteIndex,
      int byteCount,
      char[] chars,
      int charIndex)
    {
      for (int index = 0; index < byteCount; ++index)
        chars[charIndex + index] = (char) bytes[byteIndex + index];
      return byteCount;
    }

    public override int GetMaxByteCount(int charCount) => charCount;

    public override int GetMaxCharCount(int byteCount) => byteCount;
  }
}
