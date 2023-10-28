// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.NTTaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class NTTaggedData : ITaggedData
  {
    private DateTime _lastAccessTime = DateTime.FromFileTime(0L);
    private DateTime _lastModificationTime = DateTime.FromFileTime(0L);
    private DateTime _createTime = DateTime.FromFileTime(0L);

    public short TagID => 10;

    public void SetData(byte[] data, int index, int count)
    {
      using (MemoryStream memoryStream = new MemoryStream(data, index, count, false))
      {
        using (ZipHelperStream zipHelperStream = new ZipHelperStream((Stream) memoryStream))
        {
          zipHelperStream.ReadLEInt();
          while (zipHelperStream.Position < zipHelperStream.Length)
          {
            int num = zipHelperStream.ReadLEShort();
            int offset = zipHelperStream.ReadLEShort();
            if (num == 1)
            {
              if (offset < 24)
                break;
              this._lastModificationTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              this._lastAccessTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              this._createTime = DateTime.FromFileTime(zipHelperStream.ReadLELong());
              break;
            }
            zipHelperStream.Seek((long) offset, SeekOrigin.Current);
          }
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
          zipHelperStream.WriteLEInt(0);
          zipHelperStream.WriteLEShort(1);
          zipHelperStream.WriteLEShort(24);
          zipHelperStream.WriteLELong(this._lastModificationTime.ToFileTime());
          zipHelperStream.WriteLELong(this._lastAccessTime.ToFileTime());
          zipHelperStream.WriteLELong(this._createTime.ToFileTime());
          return memoryStream.ToArray();
        }
      }
    }

    public static bool IsValidValue(DateTime value)
    {
      bool flag = true;
      try
      {
        value.ToFileTimeUtc();
      }
      catch
      {
        flag = false;
      }
      return flag;
    }

    public DateTime LastModificationTime
    {
      get => this._lastModificationTime;
      set => this._lastModificationTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public DateTime CreateTime
    {
      get => this._createTime;
      set => this._createTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }

    public DateTime LastAccessTime
    {
      get => this._lastAccessTime;
      set => this._lastAccessTime = NTTaggedData.IsValidValue(value) ? value : throw new ArgumentOutOfRangeException(nameof (value));
    }
  }
}
