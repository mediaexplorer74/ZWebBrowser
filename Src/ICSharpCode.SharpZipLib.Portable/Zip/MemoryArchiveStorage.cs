// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.MemoryArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Core;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class MemoryArchiveStorage : BaseArchiveStorage
  {
    private MemoryStream temporaryStream_;
    private MemoryStream finalStream_;

    public MemoryArchiveStorage()
      : base(FileUpdateMode.Direct)
    {
    }

    public MemoryArchiveStorage(FileUpdateMode updateMode)
      : base(updateMode)
    {
    }

    public MemoryStream FinalStream => this.finalStream_;

    public override Stream GetTemporaryOutput()
    {
      this.temporaryStream_ = new MemoryStream();
      return (Stream) this.temporaryStream_;
    }

    public override Stream ConvertTemporaryToFinal()
    {
      this.finalStream_ = this.temporaryStream_ != null ? new MemoryStream(this.temporaryStream_.ToArray()) : throw new ZipException("No temporary stream has been created");
      return (Stream) this.finalStream_;
    }

    public override Stream MakeTemporaryCopy(Stream stream)
    {
      this.temporaryStream_ = new MemoryStream();
      stream.Position = 0L;
      StreamUtils.Copy(stream, (Stream) this.temporaryStream_, new byte[4096]);
      return (Stream) this.temporaryStream_;
    }

    public override Stream OpenForDirectUpdate(Stream stream)
    {
      Stream destination;
      if (stream == null || !stream.CanWrite)
      {
        destination = (Stream) new MemoryStream();
        if (stream != null)
        {
          stream.Position = 0L;
          StreamUtils.Copy(stream, destination, new byte[4096]);
          stream.Dispose();
        }
      }
      else
        destination = stream;
      return destination;
    }

    public override void Dispose()
    {
      if (this.temporaryStream_ == null)
        return;
      this.temporaryStream_.Dispose();
    }
  }
}
