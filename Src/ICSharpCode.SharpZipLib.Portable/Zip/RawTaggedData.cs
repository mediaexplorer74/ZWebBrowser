// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.RawTaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class RawTaggedData : ITaggedData
  {
    private short _tag;
    private byte[] _data;

    public RawTaggedData(short tag) => this._tag = tag;

    public short TagID
    {
      get => this._tag;
      set => this._tag = value;
    }

    public void SetData(byte[] data, int offset, int count)
    {
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this._data = new byte[count];
      Array.Copy((Array) data, offset, (Array) this._data, 0, count);
    }

    public byte[] GetData() => this._data;

    public byte[] Data
    {
      get => this._data;
      set => this._data = value;
    }
  }
}
