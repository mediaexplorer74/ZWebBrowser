// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.PathTooLongException
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using System;

namespace ICSharpCode.SharpZipLib
{
  public class PathTooLongException : Exception
  {
    public PathTooLongException()
      : base("Path too long")
    {
    }

    public PathTooLongException(string message)
      : base(message)
    {
    }

    public PathTooLongException(string message, Exception innerException)
      : base(message, innerException)
    {
    }
  }
}
