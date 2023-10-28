// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipInputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.IO;

namespace ICSharpCode.SharpZipLib.GZip
{
  public class GZipInputStream : InflaterInputStream
  {
    protected Crc32 crc;
    private bool readGZIPHeader;

    public GZipInputStream(Stream baseInputStream)
      : this(baseInputStream, 4096)
    {
    }

    public GZipInputStream(Stream baseInputStream, int size)
      : base(baseInputStream, new Inflater(true), size)
    {
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      while (this.readGZIPHeader || this.ReadHeader())
      {
        int count1 = base.Read(buffer, offset, count);
        if (count1 > 0)
          this.crc.Update(buffer, offset, count1);
        if (this.inf.IsFinished)
          this.ReadFooter();
        if (count1 > 0)
          return count1;
      }
      return 0;
    }

    private bool ReadHeader()
    {
      this.crc = new Crc32();
      if (this.inputBuffer.Available <= 0)
      {
        this.inputBuffer.Fill();
        if (this.inputBuffer.Available <= 0)
          return false;
      }
      Crc32 crc32 = new Crc32();
      int num1 = this.inputBuffer.ReadLeByte();
      if (num1 < 0)
        throw new EndOfStreamException("EOS reading GZIP header");
      crc32.Update(num1);
      if (num1 != 31)
        throw new GZipException("Error GZIP header, first magic byte doesn't match");
      int num2 = this.inputBuffer.ReadLeByte();
      if (num2 < 0)
        throw new EndOfStreamException("EOS reading GZIP header");
      if (num2 != 139)
        throw new GZipException("Error GZIP header,  second magic byte doesn't match");
      crc32.Update(num2);
      int num3 = this.inputBuffer.ReadLeByte();
      if (num3 < 0)
        throw new EndOfStreamException("EOS reading GZIP header");
      if (num3 != 8)
        throw new GZipException("Error GZIP header, data not in deflate format");
      crc32.Update(num3);
      int num4 = this.inputBuffer.ReadLeByte();
      if (num4 < 0)
        throw new EndOfStreamException("EOS reading GZIP header");
      crc32.Update(num4);
      if ((num4 & 224) != 0)
        throw new GZipException("Reserved flag bits in GZIP header != 0");
      for (int index = 0; index < 6; ++index)
      {
        int num5 = this.inputBuffer.ReadLeByte();
        if (num5 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        crc32.Update(num5);
      }
      if ((num4 & 4) != 0)
      {
        int num6 = this.inputBuffer.ReadLeByte();
        int num7 = this.inputBuffer.ReadLeByte();
        if (num6 < 0 || num7 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        crc32.Update(num6);
        crc32.Update(num7);
        int num8 = num7 << 8 | num6;
        for (int index = 0; index < num8; ++index)
        {
          int num9 = this.inputBuffer.ReadLeByte();
          if (num9 < 0)
            throw new EndOfStreamException("EOS reading GZIP header");
          crc32.Update(num9);
        }
      }
      if ((num4 & 8) != 0)
      {
        int num10;
        while ((num10 = this.inputBuffer.ReadLeByte()) > 0)
          crc32.Update(num10);
        if (num10 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        crc32.Update(num10);
      }
      if ((num4 & 16) != 0)
      {
        int num11;
        while ((num11 = this.inputBuffer.ReadLeByte()) > 0)
          crc32.Update(num11);
        if (num11 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        crc32.Update(num11);
      }
      if ((num4 & 2) != 0)
      {
        int num12 = this.inputBuffer.ReadLeByte();
        if (num12 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        int num13 = this.inputBuffer.ReadLeByte();
        if (num13 < 0)
          throw new EndOfStreamException("EOS reading GZIP header");
        if ((num12 << 8 | num13) != ((int) crc32.Value & (int) ushort.MaxValue))
          throw new GZipException("Header CRC value mismatch");
      }
      this.readGZIPHeader = true;
      return true;
    }

    private void ReadFooter()
    {
      byte[] outBuffer = new byte[8];
      long num1 = this.inf.TotalOut & (long) uint.MaxValue;
      this.inputBuffer.Available += this.inf.RemainingInput;
      this.inf.Reset();
      int num2;
      for (int length = 8; length > 0; length -= num2)
      {
        num2 = this.inputBuffer.ReadClearTextBuffer(outBuffer, 8 - length, length);
        if (num2 <= 0)
          throw new EndOfStreamException("EOS reading GZIP footer");
      }
      int num3 = (int) outBuffer[0] & (int) byte.MaxValue | ((int) outBuffer[1] & (int) byte.MaxValue) << 8 | ((int) outBuffer[2] & (int) byte.MaxValue) << 16 | (int) outBuffer[3] << 24;
      if (num3 != (int) this.crc.Value)
        throw new GZipException("GZIP crc sum mismatch, theirs \"" + (object) num3 + "\" and ours \"" + (object) (int) this.crc.Value);
      uint num4 = (uint) ((int) outBuffer[4] & (int) byte.MaxValue | ((int) outBuffer[5] & (int) byte.MaxValue) << 8 | ((int) outBuffer[6] & (int) byte.MaxValue) << 16 | (int) outBuffer[7] << 24);
      if (num1 != (long) num4)
        throw new GZipException("Number of bytes mismatch in footer");
      this.readGZIPHeader = false;
    }
  }
}
