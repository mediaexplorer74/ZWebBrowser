﻿// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Tar.TarInputStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Tar
{
  public class TarInputStream : Stream
  {
    protected bool hasHitEOF;
    protected long entrySize;
    protected long entryOffset;
    protected byte[] readBuffer;
    protected TarBuffer tarBuffer;
    private TarEntry currentEntry;
    protected TarInputStream.IEntryFactory entryFactory;
    private readonly Stream inputStream;

    public TarInputStream(Stream inputStream)
      : this(inputStream, 20)
    {
    }

    public TarInputStream(Stream inputStream, int blockFactor)
    {
      this.inputStream = inputStream;
      this.tarBuffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
    }

    public bool IsStreamOwner
    {
      get => this.tarBuffer.IsStreamOwner;
      set => this.tarBuffer.IsStreamOwner = value;
    }

    public override bool CanRead => this.inputStream.CanRead;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    public override long Length => this.inputStream.Length;

    public override long Position
    {
      get => this.inputStream.Position;
      set => throw new NotSupportedException("TarInputStream Seek not supported");
    }

    public override void Flush() => this.inputStream.Flush();

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException("TarInputStream Seek not supported");

    public override void SetLength(long value) => throw new NotSupportedException("TarInputStream SetLength not supported");

    public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException("TarInputStream Write not supported");

    public override void WriteByte(byte value) => throw new NotSupportedException("TarInputStream WriteByte not supported");

    public override int ReadByte()
    {
      byte[] buffer = new byte[1];
      return this.Read(buffer, 0, 1) <= 0 ? -1 : (int) buffer[0];
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
      if (buffer == null)
        throw new ArgumentNullException(nameof (buffer));
      int num1 = 0;
      if (this.entryOffset >= this.entrySize)
        return 0;
      long num2 = (long) count;
      if (num2 + this.entryOffset > this.entrySize)
        num2 = this.entrySize - this.entryOffset;
      if (this.readBuffer != null)
      {
        int num3 = num2 > (long) this.readBuffer.Length ? this.readBuffer.Length : (int) num2;
        Array.Copy((Array) this.readBuffer, 0, (Array) buffer, offset, num3);
        if (num3 >= this.readBuffer.Length)
        {
          this.readBuffer = (byte[]) null;
        }
        else
        {
          int length = this.readBuffer.Length - num3;
          byte[] destinationArray = new byte[length];
          Array.Copy((Array) this.readBuffer, num3, (Array) destinationArray, 0, length);
          this.readBuffer = destinationArray;
        }
        num1 += num3;
        num2 -= (long) num3;
        offset += num3;
      }
      while (num2 > 0L)
      {
        byte[] sourceArray = this.tarBuffer.ReadBlock();
        if (sourceArray == null)
          throw new TarException("unexpected EOF with " + (object) num2 + " bytes unread");
        int num4 = (int) num2;
        int length = sourceArray.Length;
        if (length > num4)
        {
          Array.Copy((Array) sourceArray, 0, (Array) buffer, offset, num4);
          this.readBuffer = new byte[length - num4];
          Array.Copy((Array) sourceArray, num4, (Array) this.readBuffer, 0, length - num4);
        }
        else
        {
          num4 = length;
          Array.Copy((Array) sourceArray, 0, (Array) buffer, offset, length);
        }
        num1 += num4;
        num2 -= (long) num4;
        offset += num4;
      }
      this.entryOffset += (long) num1;
      return num1;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing)
        return;
      this.tarBuffer.Close();
    }

    public void SetEntryFactory(TarInputStream.IEntryFactory factory) => this.entryFactory = factory;

    public int RecordSize => this.tarBuffer.RecordSize;

    [Obsolete("Use RecordSize property instead")]
    public int GetRecordSize() => this.tarBuffer.RecordSize;

    public long Available => this.entrySize - this.entryOffset;

    public void Skip(long skipCount)
    {
      byte[] buffer = new byte[8192];
      int num;
      for (long index = skipCount; index > 0L; index -= (long) num)
      {
        int count = index > (long) buffer.Length ? buffer.Length : (int) index;
        num = this.Read(buffer, 0, count);
        if (num == -1)
          break;
      }
    }

    public bool IsMarkSupported => false;

    public void Mark(int markLimit)
    {
    }

    public void Reset()
    {
    }

    public TarEntry GetNextEntry()
    {
      if (this.hasHitEOF)
        return (TarEntry) null;
      if (this.currentEntry != null)
        this.SkipToNextEntry();
      byte[] numArray1 = this.tarBuffer.ReadBlock();
      if (numArray1 == null)
        this.hasHitEOF = true;
      else if (TarBuffer.IsEndOfArchiveBlock(numArray1))
        this.hasHitEOF = true;
      if (this.hasHitEOF)
      {
        this.currentEntry = (TarEntry) null;
      }
      else
      {
        try
        {
          TarHeader tarHeader = new TarHeader();
          tarHeader.ParseBuffer(numArray1);
          if (!tarHeader.IsChecksumValid)
            throw new TarException("Header checksum is invalid");
          this.entryOffset = 0L;
          this.entrySize = tarHeader.Size;
          StringBuilder stringBuilder = (StringBuilder) null;
          if (tarHeader.TypeFlag == (byte) 76)
          {
            byte[] numArray2 = new byte[512];
            long entrySize = this.entrySize;
            stringBuilder = new StringBuilder();
            int length;
            for (; entrySize > 0L; entrySize -= (long) length)
            {
              length = this.Read(numArray2, 0, entrySize > (long) numArray2.Length ? numArray2.Length : (int) entrySize);
              if (length == -1)
                throw new InvalidHeaderException("Failed to read long name entry");
              stringBuilder.Append(TarHeader.ParseName(numArray2, 0, length).ToString());
            }
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 103)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 120)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag == (byte) 86)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          else if (tarHeader.TypeFlag != (byte) 48 && tarHeader.TypeFlag != (byte) 0 && tarHeader.TypeFlag != (byte) 53)
          {
            this.SkipToNextEntry();
            numArray1 = this.tarBuffer.ReadBlock();
          }
          if (this.entryFactory == null)
          {
            this.currentEntry = new TarEntry(numArray1);
            if (stringBuilder != null)
              this.currentEntry.Name = stringBuilder.ToString();
          }
          else
            this.currentEntry = this.entryFactory.CreateEntry(numArray1);
          this.entryOffset = 0L;
          this.entrySize = this.currentEntry.Size;
        }
        catch (InvalidHeaderException ex)
        {
          this.entrySize = 0L;
          this.entryOffset = 0L;
          this.currentEntry = (TarEntry) null;
          throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", (object) this.tarBuffer.CurrentRecord, (object) this.tarBuffer.CurrentBlock, (object) ex.Message));
        }
      }
      return this.currentEntry;
    }

    public void CopyEntryContents(Stream outputStream)
    {
      byte[] buffer = new byte[32768];
      while (true)
      {
        int count = this.Read(buffer, 0, buffer.Length);
        if (count > 0)
          outputStream.Write(buffer, 0, count);
        else
          break;
      }
    }

    private void SkipToNextEntry()
    {
      long skipCount = this.entrySize - this.entryOffset;
      if (skipCount > 0L)
        this.Skip(skipCount);
      this.readBuffer = (byte[]) null;
    }

    public interface IEntryFactory
    {
      TarEntry CreateEntry(string name);

      TarEntry CreateEntryFromFile(string fileName);

      TarEntry CreateEntry(byte[] headerBuffer);
    }

    public class EntryFactoryAdapter : TarInputStream.IEntryFactory
    {
      public TarEntry CreateEntry(string name) => TarEntry.CreateTarEntry(name);

      public TarEntry CreateEntryFromFile(string fileName) => TarEntry.CreateEntryFromFile(fileName);

      public TarEntry CreateEntry(byte[] headerBuffer) => new TarEntry(headerBuffer);
    }
  }
}
