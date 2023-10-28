// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.BaseArchiveStorage
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
  public abstract class BaseArchiveStorage : IArchiveStorage
  {
    private FileUpdateMode updateMode_;

    protected BaseArchiveStorage(FileUpdateMode updateMode) => this.updateMode_ = updateMode;

    public abstract Stream GetTemporaryOutput();

    public abstract Stream ConvertTemporaryToFinal();

    public abstract Stream MakeTemporaryCopy(Stream stream);

    public abstract Stream OpenForDirectUpdate(Stream stream);

    public abstract void Dispose();

    public FileUpdateMode UpdateMode => this.updateMode_;
  }
}
