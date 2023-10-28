// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Settings
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

namespace ZHttpStockLib
{
  public class Settings
  {
    public static Settings Defaults() => new Settings()
    {
      MaxTransferThreadCount = 2
    };

    public static Settings LoadFromRepository() => Settings.Defaults();

    public int MaxTransferThreadCount { get; set; }
  }
}
