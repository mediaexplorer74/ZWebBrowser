// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ExtendedUnixData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class ExtendedUnixData : ITaggedData
  {
    private ExtendedUnixData.Flags _flags;
    private DateTime _modificationTime = new DateTime(1970, 1, 1);
    private DateTime _lastAccessTime = new DateTime(1970, 1, 1);
    private DateTime _createTime = new DateTime(1970, 1, 1);

    public short TagID => 21589;

    public void SetData(byte[] data, int index, int count)
    {
      using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
      {
        using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream) memoryStream))
        {
          this._flags = (ExtendedUnixData.Flags) zipHelperStream.ReadByte();
          if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags) 0 && count >= 5)
          {
            int seconds = zipHelperStream.ReadLEInt();
            this._modificationTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
          }
          if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags) 0)
          {
            int seconds = zipHelperStream.ReadLEInt();
            this._lastAccessTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0)).ToLocalTime();
          }
          if ((this._flags & ExtendedUnixData.Flags.CreateTime) == (ExtendedUnixData.Flags) 0)
            return;
          int seconds1 = zipHelperStream.ReadLEInt();
          this._createTime = (new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime() + new TimeSpan(0, 0, 0, seconds1, 0)).ToLocalTime();
        }
      }
    }

    public byte[] GetData()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream) memoryStream))
        {
          zipHelperStream.IsStreamOwner = false;
          zipHelperStream.WriteByte((byte) this._flags);
          if ((this._flags & ExtendedUnixData.Flags.ModificationTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._modificationTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          if ((this._flags & ExtendedUnixData.Flags.AccessTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._lastAccessTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          if ((this._flags & ExtendedUnixData.Flags.CreateTime) != (ExtendedUnixData.Flags) 0)
          {
            int totalSeconds = (int) (this._createTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0).ToUniversalTime()).TotalSeconds;
            zipHelperStream.WriteLEInt(totalSeconds);
          }
          return memoryStream.ToArray();
        }
      }
    }

    public static bool IsValidValue(DateTime value) => value >= new DateTime(1901, 12, 13, 20, 45, 52) || value <= new DateTime(2038, 1, 19, 3, 14, 7);

    public DateTime ModificationTime
    {
      get => this._modificationTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.ModificationTime;
        this._modificationTime = value;
      }
    }

    public DateTime AccessTime
    {
      get => this._lastAccessTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.AccessTime;
        this._lastAccessTime = value;
      }
    }

    public DateTime CreateTime
    {
      get => this._createTime;
      set
      {
        if (!ExtendedUnixData.IsValidValue(value))
          throw new ArgumentOutOfRangeException(nameof (value));
        this._flags |= ExtendedUnixData.Flags.CreateTime;
        this._createTime = value;
      }
    }

    private ExtendedUnixData.Flags Include
    {
      get => this._flags;
      set => this._flags = value;
    }

    [System.Flags]
    public enum Flags : byte
    {
      ModificationTime = 1,
      AccessTime = 2,
      CreateTime = 4,
    }
  }
}
