// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Compression.CompressionEngine
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

namespace ZHttpStockLib.Compression
{
  public class CompressionEngine
  {
    private static ZipCompression _ZipEngine = new ZipCompression();

    public static ZipCompression ZipEngine() => CompressionEngine._ZipEngine;
  }
}
