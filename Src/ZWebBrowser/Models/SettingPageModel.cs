// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.SettingPageModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.ComponentModel;

namespace ZWebBrowser.Models
{
  public class SettingPageModel : INotifyPropertyChanged
  {
    private ZHttpStockLib.Setting.Setting _setting = new ZHttpStockLib.Setting.Setting();

    public bool IsPasswordEnabled
    {
      get => this._setting.PasswordEnabledSetting;
      set
      {
        this._setting.PasswordEnabledSetting = value;
        this.PropChanged(nameof (IsPasswordEnabled));
      }
    }

    public string Password
    {
      get => this._setting.PasswordSetting;
      set
      {
        this._setting.PasswordSetting = value;
        this.PropChanged(nameof (Password));
      }
    }

    public string PasswordHint
    {
      get => this._setting.PasswordHintSetting;
      set
      {
        this._setting.PasswordHintSetting = value;
        this.PropChanged(nameof (PasswordHint));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void PropChanged(string propName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propName));
    }
  }
}
