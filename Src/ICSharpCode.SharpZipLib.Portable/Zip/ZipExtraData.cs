// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ZipExtraData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public sealed class ZipExtraData : IDisposable
  {
    private int _index;
    private int _readValueStart;
    private int _readValueLength;
    private MemoryStream _newEntry;
    private byte[] _data;

    public ZipExtraData() => this.Clear();

    public ZipExtraData(byte[] data)
    {
      if (data == null)
        this._data = new byte[0];
      else
        this._data = data;
    }

    public byte[] GetEntryData()
    {
      if (this.Length > (int) ushort.MaxValue)
        throw new ZipException("Data exceeds maximum length");
      return (byte[]) this._data.Clone();
    }

    public void Clear()
    {
      if (this._data != null && this._data.Length == 0)
        return;
      this._data = new byte[0];
    }

    public int Length => this._data.Length;

    public Stream GetStreamForTag(int tag)
    {
      Stream streamForTag = (Stream) null;
      if (this.Find(tag))
        streamForTag = (Stream) new MemoryStream(this._data, this._index, this._readValueLength, false);
      return streamForTag;
    }

    private ITaggedData GetData(short tag)
    {
      ITaggedData data = (ITaggedData) null;
      if (this.Find((int) tag))
        data = ZipExtraData.Create(tag, this._data, this._readValueStart, this._readValueLength);
      return data;
    }

    private static ITaggedData Create(short tag, byte[] data, int offset, int count)
    {
      ITaggedData taggedData;
      switch (tag)
      {
        case 10:
          taggedData = (ITaggedData) new NTTaggedData();
          break;
        case 21589:
          taggedData = (ITaggedData) new ExtendedUnixData();
          break;
        default:
          taggedData = (ITaggedData) new RawTaggedData(tag);
          break;
      }
      taggedData.SetData(data, offset, count);
      return taggedData;
    }

    public int ValueLength => this._readValueLength;

    public int CurrentReadIndex => this._index;

    public int UnreadCount
    {
      get
      {
        if (this._readValueStart > this._data.Length || this._readValueStart < 4)
          throw new ZipException("Find must be called before calling a Read method");
        return this._readValueStart + this._readValueLength - this._index;
      }
    }

    public bool Find(int headerID)
    {
      this._readValueStart = this._data.Length;
      this._readValueLength = 0;
      this._index = 0;
      int num1 = this._readValueStart;
      int num2 = headerID - 1;
      while (num2 != headerID && this._index < this._data.Length - 3)
      {
        num2 = this.ReadShortInternal();
        num1 = this.ReadShortInternal();
        if (num2 != headerID)
          this._index += num1;
      }
      bool flag = num2 == headerID && this._index + num1 <= this._data.Length;
      if (flag)
      {
        this._readValueStart = this._index;
        this._readValueLength = num1;
      }
      return flag;
    }

    public void AddEntry(ITaggedData taggedData)
    {
      if (taggedData == null)
        throw new ArgumentNullException(nameof (taggedData));
      this.AddEntry((int) taggedData.TagID, taggedData.GetData());
    }

    public void AddEntry(int headerID, byte[] fieldData)
    {
      if (headerID > (int) ushort.MaxValue || headerID < 0)
        throw new ArgumentOutOfRangeException(nameof (headerID));
      int length1 = fieldData == null ? 0 : fieldData.Length;
      if (length1 > (int) ushort.MaxValue)
        throw new ArgumentOutOfRangeException(nameof (fieldData), "exceeds maximum length");
      int length2 = this._data.Length + length1 + 4;
      if (this.Find(headerID))
        length2 -= this.ValueLength + 4;
      if (length2 > (int) ushort.MaxValue)
        throw new ZipException("Data exceeds maximum length");
      this.Delete(headerID);
      byte[] numArray = new byte[length2];
      this._data.CopyTo((Array) numArray, 0);
      int length3 = this._data.Length;
      this._data = numArray;
      this.SetShort(ref length3, headerID);
      this.SetShort(ref length3, length1);
      fieldData?.CopyTo((Array) numArray, length3);
    }

    public void StartNewEntry() => this._newEntry = new MemoryStream();

    public void AddNewEntry(int headerID)
    {
      byte[] array = this._newEntry.ToArray();
      this._newEntry = (MemoryStream) null;
      this.AddEntry(headerID, array);
    }

    public void AddData(byte data) => this._newEntry.WriteByte(data);

    public void AddData(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this._newEntry.Write(data, 0, data.Length);
    }

    public void AddLeShort(int toAdd)
    {
      this._newEntry.WriteByte((byte) toAdd);
      this._newEntry.WriteByte((byte) (toAdd >> 8));
    }

    public void AddLeInt(int toAdd)
    {
      this.AddLeShort((int) (short) toAdd);
      this.AddLeShort((int) (short) (toAdd >> 16));
    }

    public void AddLeLong(long toAdd)
    {
      this.AddLeInt((int) (toAdd & (long) uint.MaxValue));
      this.AddLeInt((int) (toAdd >> 32));
    }

    public bool Delete(int headerID)
    {
      bool flag = false;
      if (this.Find(headerID))
      {
        flag = true;
        int num = this._readValueStart - 4;
        byte[] destinationArray = new byte[this._data.Length - (this.ValueLength + 4)];
        Array.Copy((Array) this._data, 0, (Array) destinationArray, 0, num);
        int sourceIndex = num + this.ValueLength + 4;
        Array.Copy((Array) this._data, sourceIndex, (Array) destinationArray, num, this._data.Length - sourceIndex);
        this._data = destinationArray;
      }
      return flag;
    }

    public long ReadLong()
    {
      this.ReadCheck(8);
      return (long) this.ReadInt() & (long) uint.MaxValue | (long) this.ReadInt() << 32;
    }

    public int ReadInt()
    {
      this.ReadCheck(4);
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8) + ((int) this._data[this._index + 2] << 16) + ((int) this._data[this._index + 3] << 24);
      this._index += 4;
      return num;
    }

    public int ReadShort()
    {
      this.ReadCheck(2);
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8);
      this._index += 2;
      return num;
    }

    public int ReadByte()
    {
      int num = -1;
      if (this._index < this._data.Length && this._readValueStart + this._readValueLength > this._index)
      {
        num = (int) this._data[this._index];
        ++this._index;
      }
      return num;
    }

    public void Skip(int amount)
    {
      this.ReadCheck(amount);
      this._index += amount;
    }

    private void ReadCheck(int length)
    {
      if (this._readValueStart > this._data.Length || this._readValueStart < 4)
        throw new ZipException("Find must be called before calling a Read method");
      if (this._index > this._readValueStart + this._readValueLength - length)
        throw new ZipException("End of extra data");
      if (this._index + length < 4)
        throw new ZipException("Cannot read before start of tag");
    }

    private int ReadShortInternal()
    {
      if (this._index > this._data.Length - 2)
        throw new ZipException("End of extra data");
      int num = (int) this._data[this._index] + ((int) this._data[this._index + 1] << 8);
      this._index += 2;
      return num;
    }

    private void SetShort(ref int index, int source)
    {
      this._data[index] = (byte) source;
      this._data[index + 1] = (byte) (source >> 8);
      index += 2;
    }

    public void Dispose()
    {
      if (this._newEntry == null)
        return;
      this._newEntry.Dispose();
    }
  }
}
