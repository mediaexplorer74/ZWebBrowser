// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.StringStaticDataSource
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System.IO;
using System.Text;

namespace ICSharpCode.SharpZipLib.Zip
{
  public class StringStaticDataSource : IStaticDataSource
  {
    private string _StringBuffer;

    public StringStaticDataSource(string str) => this._StringBuffer = str ?? string.Empty;

    public Stream GetSource() => (Stream) new MemoryStream(Encoding.UTF8.GetBytes(this._StringBuffer));
  }
}
