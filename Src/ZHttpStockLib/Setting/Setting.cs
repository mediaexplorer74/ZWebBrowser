// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Setting.Setting
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System.Collections.Generic;
using Windows.Storage;

namespace ZHttpStockLib.Setting
{
  public class Setting
  {
    private ApplicationDataContainer _settings;
    private const string CommonSettingContainer = "CommonSettingContainer";
    private const string CheckBoxSettingKeyName = "PasswordEnabledSetting";
    private const string PasswordSettingKeyName = "PasswordSetting";
    private const string PasswordHintSettingKeyName = "PasswordHintSetting";
    private const bool CheckBoxSettingDefault = false;
    private const string PasswordSettingDefault = "";
    private const string PasswordHintSettingDefault = "";

    public Setting() => this._settings = ApplicationData.Current.LocalSettings;

    private ApplicationDataContainer SettingInst
    {
      get
      {
        if (!this._settings.Containers.ContainsKey("CommonSettingContainer"))
          this._settings.CreateContainer("CommonSettingContainer", (ApplicationDataCreateDisposition) 0);
        return this._settings.Containers["CommonSettingContainer"];
      }
    }

    public bool AddOrUpdateValue(string Key, object value)
    {
      bool flag = false;
      if (((IDictionary<string, object>) this.SettingInst.Values).ContainsKey(Key))
      {
        if (((IDictionary<string, object>) this.SettingInst.Values)[Key] != value)
        {
          ((IDictionary<string, object>) this.SettingInst.Values)[Key] = value;
          flag = true;
        }
      }
      else
      {
        ((IDictionary<string, object>) this.SettingInst.Values).Add(Key, value);
        flag = true;
      }
      return flag;
    }

    public T GetValueOrDefault<T>(string Key, T defaultValue) => !((IDictionary<string, object>) this.SettingInst.Values).ContainsKey(Key) ? defaultValue : (T) ((IDictionary<string, object>) this.SettingInst.Values)[Key];

    public void Save()
    {
    }

    public bool PasswordEnabledSetting
    {
      get => this.GetValueOrDefault<bool>(nameof (PasswordEnabledSetting), false);
      set
      {
        if (!this.AddOrUpdateValue(nameof (PasswordEnabledSetting), (object) value))
          return;
        this.Save();
      }
    }

    public string PasswordSetting
    {
      get => this.GetValueOrDefault<string>(nameof (PasswordSetting), "");
      set
      {
        if (!this.AddOrUpdateValue(nameof (PasswordSetting), (object) value))
          return;
        this.Save();
      }
    }

    public string PasswordHintSetting
    {
      get => this.GetValueOrDefault<string>(nameof (PasswordHintSetting), "");
      set
      {
        if (!this.AddOrUpdateValue(nameof (PasswordHintSetting), (object) value))
          return;
        this.Save();
      }
    }
  }
}
