// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.Zip.ITaggedData
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

namespace ICSharpCode.SharpZipLib.Zip
{
  public interface ITaggedData
  {
    short TagID { get; }

    void SetData(byte[] data, int offset, int count);

    byte[] GetData();
  }
}
