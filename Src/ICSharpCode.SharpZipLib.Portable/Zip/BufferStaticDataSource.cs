// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.BufferStaticDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class BufferStaticDataSource : IStaticDataSource
  {
    private byte[] _Buffer;

    public BufferStaticDataSource(byte[] buffer) => this._Buffer = buffer != null ? buffer : throw new ArgumentNullException(nameof (buffer));

    public Stream GetSource() => (Stream) new MemoryStream(this._Buffer);
  }
}
