// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Util.MemoryStreamWithContentType
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Runtime.CompilerServices;
using Windows.Foundation;
using Windows.Storage.Streams;

namespace ZWebBrowser.Util
{
  internal class MemoryStreamWithContentType : 
    IRandomAccessStreamWithContentType,
    IRandomAccessStream,
    IDisposable,
    IInputStream,
    IOutputStream,
    IContentTypeProvider
  {
    private IRandomAccessStream _iras;
    private string _contentType;

    public MemoryStreamWithContentType(IRandomAccessStream iras, string contentType)
    {
      this._iras = iras;
      this._contentType = contentType;
    }

    public bool CanRead => this._iras.CanRead;

    public bool CanWrite => this._iras.CanWrite;

    public string ContentType => this._contentType;

    public ulong Position => this._iras.Position;

    public ulong Size
    {
      get => this._iras.Size;
      set => this._iras.put_Size(value);
    }

    public IRandomAccessStream CloneStream() => this._iras.CloneStream();

    public void Dispose() => ((IDisposable) this._iras).Dispose();

    public IAsyncOperation<bool> FlushAsync() => ((IOutputStream) this._iras).FlushAsync();

    public IInputStream GetInputStreamAt(ulong position) => this._iras.GetInputStreamAt(position);

    public IOutputStream GetOutputStreamAt(ulong position) => this._iras.GetOutputStreamAt(position);

    public IAsyncOperationWithProgress<IBuffer, uint> ReadAsync(
      IBuffer buffer,
      uint count,
      InputStreamOptions options)
    {
      return ((IInputStream) this._iras).ReadAsync(buffer, count, options);
    }

    public void Seek(ulong position) => this._iras.Seek(position);

    public IAsyncOperationWithProgress<uint, uint> WriteAsync(IBuffer buffer) => ((IOutputStream) this._iras).WriteAsync(buffer);

    IAsyncOperationWithProgress<IBuffer, uint> IInputStream.ReadAsync(
      IBuffer buffer,
      uint count,
      InputStreamOptions options)
    {
      return this.ReadAsync(buffer, count, options);
    }

    IAsyncOperationWithProgress<uint, uint> IOutputStream.WriteAsync(IBuffer buffer) => this.WriteAsync(buffer);

    IAsyncOperation<bool> IOutputStream.FlushAsync() => this.FlushAsync();

    [SpecialName]
    void IRandomAccessStream.put_Size(ulong value) => this.Size = value;
  }
}
