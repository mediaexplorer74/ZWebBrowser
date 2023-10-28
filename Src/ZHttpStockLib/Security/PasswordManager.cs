// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Security.PasswordManager
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

namespace ZHttpStockLib.Security
{
  public class PasswordManager
  {
    private static PasswordManager _pm;
    private ZHttpStockLib.Setting.Setting _settings;
    private bool _sessionValidated;

    public static PasswordManager GetInstance(ZHttpStockLib.Setting.Setting settings)
    {
      if (PasswordManager._pm == null)
        PasswordManager._pm = new PasswordManager(settings);
      return PasswordManager._pm;
    }

    private PasswordManager(ZHttpStockLib.Setting.Setting setting) => this._settings = setting;

    public bool ValidatePassword(string password) => this._settings.PasswordSetting == password;

    public bool IsPinEnabled() => this._settings.PasswordEnabledSetting;

    public string GetPasswordHint() => this._settings.PasswordHintSetting;

    public bool SessionValidated
    {
      get => this._sessionValidated;
      set => this._sessionValidated = value;
    }
  }
}
