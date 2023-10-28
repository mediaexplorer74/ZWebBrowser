// Decompiled with JetBrains decompiler
// Type: ICSharpCode.SharpZipLib.BZip2.BZip2
// Assembly: ICSharpCode.SharpZipLib.Portable, Version=0.86.0.51803, Culture=neutral, PublicKeyToken=null
// MVID: 7E5F2306-F1E0-4135-A1A9-3924C3BDBA5D
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ICSharpCode.SharpZipLib.Portable.dll

using ICSharpCode.SharpZipLib.Core;
using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.BZip2
{
  public static class BZip2
  {
    public static void Decompress(Stream inStream, Stream outStream, bool isStreamOwner)
    {
      if (inStream != null)
      {
        if (outStream != null)
        {
          try
          {
            using (BZip2InputStream source = new BZip2InputStream(inStream))
            {
              source.IsStreamOwner = isStreamOwner;
              StreamUtils.Copy((Stream) source, outStream, new byte[4096]);
              return;
            }
          }
          finally
          {
            if (isStreamOwner)
              outStream.Dispose();
          }
        }
      }
      throw new Exception("Null Stream");
    }

    public static void Compress(Stream inStream, Stream outStream, bool isStreamOwner, int level)
    {
      if (inStream != null)
      {
        if (outStream != null)
        {
          try
          {
            using (BZip2OutputStream destination = new BZip2OutputStream(outStream, level))
            {
              destination.IsStreamOwner = isStreamOwner;
              StreamUtils.Copy(inStream, (Stream) destination, new byte[4096]);
              return;
            }
          }
          finally
          {
            if (isStreamOwner)
              inStream.Dispose();
          }
        }
      }
      throw new Exception("Null Stream");
    }
  }
}
