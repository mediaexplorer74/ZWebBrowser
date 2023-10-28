// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.GZip.GZipException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.GZip
{
  public class GZipException : SharpZipBaseException
  {
    public GZipException()
    {
    }

    public GZipException(string message)
      : base(message)
    {
    }

    public GZipException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
