// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Path.PathValidator
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;

namespace ZHttpStockLib.Path
{
  public class PathValidator
  {
    public static bool IsFile(string path) => new Uri(path).IsFile && !string.IsNullOrWhiteSpace(System.IO.Path.GetFileName(path));
  }
}
