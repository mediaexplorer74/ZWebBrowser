// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2Exception
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib.BZip2
{
  public class BZip2Exception : SharpZipBaseException
  {
    public BZip2Exception()
    {
    }

    public BZip2Exception(string message)
      : base(message)
    {
    }

    public BZip2Exception(string message, Exception exception)
      : base(message, exception)
    {
    }
  }
}
