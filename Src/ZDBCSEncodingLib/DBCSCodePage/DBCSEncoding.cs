// Decompiled with JetBrains decompiler
// Type: DBCSCodePage.DBCSEncoding
// Assembly: ZDBCSEncodingLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 13FE3C2F-1C9E-4DED-9F9F-903867DA5F3C
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZDBCSEncodingLib.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using ZDBCSEncodingLib;

namespace DBCSCodePage
{
  public sealed class DBCSEncoding : Encoding
  {
    private const char LEAD_BYTE_CHAR = '\uFFFE';
    private char[] _dbcsToUnicode;
    private ushort[] _unicodeToDbcs;
    private string _webName;
    private static Dictionary<string, Tuple<char[], ushort[]>> _cache;

    static DBCSEncoding()
    {
      if (!BitConverter.IsLittleEndian)
        throw new PlatformNotSupportedException("Not supported big endian platform.");
      DBCSEncoding._cache = new Dictionary<string, Tuple<char[], ushort[]>>();
    }

    private DBCSEncoding()
    {
    }

    public static async Task<DBCSEncoding> GetDBCSEncodingAsync(string name)
    {
      name = name.ToLower();
      DBCSEncoding encoding = new DBCSEncoding();
      encoding._webName = name;
      if (DBCSEncoding._cache.ContainsKey(name))
      {
        Tuple<char[], ushort[]> tuple = DBCSEncoding._cache[name];
        encoding._dbcsToUnicode = tuple.Item1;
        encoding._unicodeToDbcs = tuple.Item2;
        return encoding;
      }
      char[] dbcsToUnicode = new char[65536];
      ushort[] unicodeToDbcs = new ushort[65536];
      using (Stream stream = ((IInputStream) await BinLoader.GetBinStreamAsync(name + ".bin")).AsStreamForRead((int) ushort.MaxValue))
      {
        using (BinaryReader reader = new BinaryReader(stream))
          await Task.Run((Action) (() =>
          {
            for (int index = 0; index < (int) ushort.MaxValue; ++index)
            {
              ushort num = reader.ReadUInt16();
              unicodeToDbcs[index] = num;
            }
            for (int index = 0; index < (int) ushort.MaxValue; ++index)
            {
              ushort num = reader.ReadUInt16();
              dbcsToUnicode[index] = (char) num;
            }
          }));
      }
      DBCSEncoding._cache[name] = new Tuple<char[], ushort[]>(dbcsToUnicode, unicodeToDbcs);
      encoding._dbcsToUnicode = dbcsToUnicode;
      encoding._unicodeToDbcs = unicodeToDbcs;
      return encoding;
    }

    public override int GetByteCount(char[] chars, int index, int count)
    {
      int byteCount = 0;
      for (int index1 = 0; index1 < count; ++index1)
      {
        if (this._unicodeToDbcs[(int) chars[index]] > (ushort) byte.MaxValue)
          ++byteCount;
        ++index;
        ++byteCount;
      }
      return byteCount;
    }

    public override int GetBytes(
      char[] chars,
      int charIndex,
      int charCount,
      byte[] bytes,
      int byteIndex)
    {
      int bytes1 = 0;
      for (int index1 = 0; index1 < charCount; ++index1)
      {
        char index2 = chars[charIndex];
        ushort unicodeToDbc = this._unicodeToDbcs[(int) index2];
        if (unicodeToDbc == (ushort) 0 && index2 != char.MinValue)
          bytes[byteIndex] = (byte) 63;
        else if (unicodeToDbc < (ushort) 256)
        {
          bytes[byteIndex] = (byte) unicodeToDbc;
        }
        else
        {
          bytes[byteIndex] = (byte) ((int) unicodeToDbc >> 8 & (int) byte.MaxValue);
          ++byteIndex;
          ++bytes1;
          bytes[byteIndex] = (byte) ((uint) unicodeToDbc & (uint) byte.MaxValue);
        }
        ++charIndex;
        ++byteIndex;
        ++bytes1;
      }
      return bytes1;
    }

    public override int GetCharCount(byte[] bytes, int index, int count) => this.GetCharCount(bytes, index, count, (DBCSEncoding.DBCSDecoder) null);

    private int GetCharCount(byte[] bytes, int index, int count, DBCSEncoding.DBCSDecoder decoder)
    {
      int charCount = 0;
      for (int index1 = 0; index1 < count; ++index1)
      {
        ushort num = 0;
        if (decoder != null && decoder.pendingByte != (byte) 0)
        {
          num = (ushort) decoder.pendingByte;
          decoder.pendingByte = (byte) 0;
        }
        if (this._dbcsToUnicode[(int) (ushort) ((uint) num << 8 | (uint) bytes[index])] == '\uFFFE')
        {
          if (index1 < count - 1)
          {
            ++index;
            ++index1;
          }
          else if (decoder != null)
          {
            decoder.pendingByte = bytes[index];
            return charCount;
          }
        }
        ++index;
        ++charCount;
      }
      return charCount;
    }

    public override int GetChars(
      byte[] bytes,
      int byteIndex,
      int byteCount,
      char[] chars,
      int charIndex)
    {
      return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, (DBCSEncoding.DBCSDecoder) null);
    }

    private int GetChars(
      byte[] bytes,
      int byteIndex,
      int byteCount,
      char[] chars,
      int charIndex,
      DBCSEncoding.DBCSDecoder decoder)
    {
      int chars1 = 0;
      for (int index1 = 0; index1 < byteCount; ++index1)
      {
        ushort num = 0;
        if (decoder != null && decoder.pendingByte != (byte) 0)
        {
          num = (ushort) decoder.pendingByte;
          decoder.pendingByte = (byte) 0;
        }
        ushort index2 = (ushort) ((uint) num << 8 | (uint) bytes[byteIndex]);
        char minValue = this._dbcsToUnicode[(int) index2];
        if (minValue == '\uFFFE')
        {
          if (index1 < byteCount - 1)
          {
            ++byteIndex;
            ++index1;
            index2 = (ushort) ((uint) index2 << 8 | (uint) bytes[byteIndex]);
            minValue = this._dbcsToUnicode[(int) index2];
          }
          else if (decoder == null)
          {
            minValue = char.MinValue;
          }
          else
          {
            decoder.pendingByte = bytes[byteIndex];
            return chars1;
          }
        }
        chars[charIndex] = minValue != char.MinValue || index2 == (ushort) 0 ? minValue : '?';
        ++byteIndex;
        ++charIndex;
        ++chars1;
      }
      return chars1;
    }

    public override int GetMaxByteCount(int charCount)
    {
      if (charCount < 0)
        throw new ArgumentOutOfRangeException(nameof (charCount));
      long num = (long) (charCount + 1) * 2L;
      return num <= (long) int.MaxValue ? (int) num : throw new ArgumentOutOfRangeException(nameof (charCount));
    }

    public override int GetMaxCharCount(int byteCount)
    {
      if (byteCount < 0)
        throw new ArgumentOutOfRangeException(nameof (byteCount));
      long num = (long) (byteCount + 3);
      return num <= (long) int.MaxValue ? (int) num : throw new ArgumentOutOfRangeException(nameof (byteCount));
    }

    public override Decoder GetDecoder() => (Decoder) new DBCSEncoding.DBCSDecoder(this);

    public override string WebName => this._webName;

    private sealed class DBCSDecoder : Decoder
    {
      private DBCSEncoding _encoding;
      public byte pendingByte;

      public DBCSDecoder(DBCSEncoding encoding) => this._encoding = encoding;

      public override int GetCharCount(byte[] bytes, int index, int count) => this._encoding.GetCharCount(bytes, index, count, this);

      public override int GetChars(
        byte[] bytes,
        int byteIndex,
        int byteCount,
        char[] chars,
        int charIndex)
      {
        return this._encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex, this);
      }
    }
  }
}
