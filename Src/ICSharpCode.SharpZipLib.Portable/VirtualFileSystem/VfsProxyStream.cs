// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.VirtualFileSystem.VfsProxyStream
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.VirtualFileSystem
{
  public class VfsProxyStream : VfsStream
  {
    private string _Name;
    private Stream _Stream;

    protected Stream Stream => this._Stream != null ? this._Stream : throw new ObjectDisposedException(nameof (VfsProxyStream));

    public VfsProxyStream(Stream stream, string name)
    {
      this._Stream = stream != null ? stream : throw new ArgumentNullException(nameof (stream));
      this._Name = name;
    }

    protected override void Dispose(bool disposing)
    {
      base.Dispose(disposing);
      if (!disposing || this._Stream == null)
        return;
      this._Stream.Dispose();
      this._Stream = (Stream) null;
    }

    public override void Flush() => this.Stream.Flush();

    public override int Read(byte[] buffer, int offset, int count) => this.Stream.Read(buffer, offset, count);

    public override long Seek(long offset, SeekOrigin origin) => this.Stream.Seek(offset, origin);

    public override void SetLength(long value) => this.Stream.SetLength(value);

    public override void Write(byte[] buffer, int offset, int count) => this.Stream.Write(buffer, offset, count);

    public override string Name => this._Name;

    public override bool CanRead => this.Stream.CanRead;

    public override bool CanSeek => this.Stream.CanSeek;

    public override bool CanWrite => this.Stream.CanWrite;

    public override long Length => this.Stream.Length;

    public override long Position
    {
      get => this.Stream.Position;
      set => this.Stream.Position = value;
    }
  }
}
