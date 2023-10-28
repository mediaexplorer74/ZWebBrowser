// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputBuffer
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
  public class InflaterInputBuffer
  {
    private int rawLength;
    private byte[] rawData;
    private int clearTextLength;
    private byte[] clearText;
    private int available;
    private Stream inputStream;

    public InflaterInputBuffer(Stream stream)
      : this(stream, 4096)
    {
    }

    public InflaterInputBuffer(Stream stream, int bufferSize)
    {
      this.inputStream = stream;
      if (bufferSize < 1024)
        bufferSize = 1024;
      this.rawData = new byte[bufferSize];
      this.clearText = this.rawData;
    }

    public int RawLength => this.rawLength;

    public byte[] RawData => this.rawData;

    public int ClearTextLength => this.clearTextLength;

    public byte[] ClearText => this.clearText;

    public int Available
    {
      get => this.available;
      set => this.available = value;
    }

    public void SetInflaterInput(Inflater inflater)
    {
      if (this.available <= 0)
        return;
      inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
      this.available = 0;
    }

    public void Fill()
    {
      this.rawLength = 0;
      int num;
      for (int length = this.rawData.Length; length > 0; length -= num)
      {
        num = this.inputStream.Read(this.rawData, this.rawLength, length);
        if (num > 0)
          this.rawLength += num;
        else
          break;
      }
      this.clearTextLength = this.rawLength;
      this.available = this.clearTextLength;
    }

    public int ReadRawBuffer(byte[] buffer) => this.ReadRawBuffer(buffer, 0, buffer.Length);

    public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      int destinationIndex = offset;
      int val1 = length;
      while (val1 > 0)
      {
        if (this.available <= 0)
        {
          this.Fill();
          if (this.available <= 0)
            return 0;
        }
        int length1 = Math.Min(val1, this.available);
        Array.Copy((Array) this.rawData, this.rawLength - this.available, (Array) outBuffer, destinationIndex, length1);
        destinationIndex += length1;
        val1 -= length1;
        this.available -= length1;
      }
      return length;
    }

    public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
    {
      if (length < 0)
        throw new ArgumentOutOfRangeException(nameof (length));
      int destinationIndex = offset;
      int val1 = length;
      while (val1 > 0)
      {
        if (this.available <= 0)
        {
          this.Fill();
          if (this.available <= 0)
            return 0;
        }
        int length1 = Math.Min(val1, this.available);
        Array.Copy((Array) this.clearText, this.clearTextLength - this.available, (Array) outBuffer, destinationIndex, length1);
        destinationIndex += length1;
        val1 -= length1;
        this.available -= length1;
      }
      return length;
    }

    public int ReadLeByte()
    {
      if (this.available <= 0)
      {
        this.Fill();
        if (this.available <= 0)
          throw new ZipException("EOF in header");
      }
      byte num = this.rawData[this.rawLength - this.available];
      --this.available;
      return (int) num;
    }

    public int ReadLeShort() => this.ReadLeByte() | this.ReadLeByte() << 8;

    public int ReadLeInt() => this.ReadLeShort() | this.ReadLeShort() << 16;

    public long ReadLeLong() => (long) (uint) this.ReadLeInt() | (long) this.ReadLeInt() << 32;
  }
}
