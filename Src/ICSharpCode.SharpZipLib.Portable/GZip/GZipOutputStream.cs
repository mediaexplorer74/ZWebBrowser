// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipOutputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.GZip
{
  public class GZipOutputStream : DeflaterOutputStream
  {
    protected Crc32 crc = new Crc32();
    private GZipOutputStream.OutputState state_;

    public GZipOutputStream(Stream baseOutputStream)
      : this(baseOutputStream, 4096)
    {
    }

    public GZipOutputStream(Stream baseOutputStream, int size)
      : base(baseOutputStream, new Deflater(-1, true), size)
    {
    }

    public void SetLevel(int level)
    {
      if (level < 1)
        throw new ArgumentOutOfRangeException(nameof (level));
      this.deflater_.SetLevel(level);
    }

    public int GetLevel() => this.deflater_.GetLevel();

    public override void Write(byte[] buffer, int offset, int count)
    {
      if (this.state_ == GZipOutputStream.OutputState.Header)
        this.WriteHeader();
      if (this.state_ != GZipOutputStream.OutputState.Footer)
        throw new InvalidOperationException("Write not permitted in current state");
      this.crc.Update(buffer, offset, count);
      base.Write(buffer, offset, count);
    }

    protected override void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      try
      {
        this.Finish();
      }
      finally
      {
        if (this.state_ != GZipOutputStream.OutputState.Closed)
        {
          this.state_ = GZipOutputStream.OutputState.Closed;
          if (this.IsStreamOwner)
            this.baseOutputStream_.Dispose();
        }
      }
    }

    public override void Finish()
    {
      if (this.state_ == GZipOutputStream.OutputState.Header)
        this.WriteHeader();
      if (this.state_ != GZipOutputStream.OutputState.Footer)
        return;
      this.state_ = GZipOutputStream.OutputState.Finished;
      base.Finish();
      uint num1 = (uint) ((ulong) this.deflater_.TotalIn & (ulong) uint.MaxValue);
      uint num2 = (uint) ((ulong) this.crc.Value & (ulong) uint.MaxValue);
      byte[] buffer = new byte[8]
      {
        (byte) num2,
        (byte) (num2 >> 8),
        (byte) (num2 >> 16),
        (byte) (num2 >> 24),
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) (num1 >> 24)
      };
      this.baseOutputStream_.Write(buffer, 0, buffer.Length);
    }

    private void WriteHeader()
    {
      if (this.state_ != GZipOutputStream.OutputState.Header)
        return;
      this.state_ = GZipOutputStream.OutputState.Footer;
      int num = (int) ((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
      byte[] buffer = new byte[10]
      {
        (byte) 31,
        (byte) 139,
        (byte) 8,
        (byte) 0,
        (byte) num,
        (byte) (num >> 8),
        (byte) (num >> 16),
        (byte) (num >> 24),
        (byte) 0,
        byte.MaxValue
      };
      this.baseOutputStream_.Write(buffer, 0, buffer.Length);
    }

    private enum OutputState
    {
      Header,
      Footer,
      Finished,
      Closed,
    }
  }
}
