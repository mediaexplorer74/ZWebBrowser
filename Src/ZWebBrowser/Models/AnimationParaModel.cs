// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.AnimationParaModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.ComponentModel;

namespace ZWebBrowser.Models
{
  public class AnimationParaModel : INotifyPropertyChanged
  {
    private double _insMsgFromY;
    private double _insMsgToY;

    public double InsMsgFromY
    {
      get => this._insMsgFromY;
      set
      {
        this._insMsgFromY = value;
        this.PropChanged(nameof (InsMsgFromY));
      }
    }

    public double InsMsgToY
    {
      get => this._insMsgToY;
      set
      {
        this._insMsgToY = value;
        this.PropChanged(nameof (InsMsgToY));
      }
    }

    private void PropChanged(string propName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propName));
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
