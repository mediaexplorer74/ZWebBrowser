// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.ZSettingsModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using ZHttpStockLib;

namespace ZWebBrowser.Models
{
  public class ZSettingsModel
  {
    private Settings _settings;

    public ZSettingsModel(Settings s) => this._settings = s;

    public string MaxTransferThreadCount
    {
      get => this._settings.MaxTransferThreadCount.ToString();
      set => this._settings.MaxTransferThreadCount = int.Parse(value);
    }
  }
}
