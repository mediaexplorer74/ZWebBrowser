// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Commands.CommandQueueItem
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System.ComponentModel;

namespace ZHttpStockLib.Commands
{
  public abstract class CommandQueueItem : INotifyPropertyChanged
  {
    private CommandQueueStatus _status;
    private double _progressValue;
    private string _name;

    public event PropertyChangedEventHandler PropertyChanged;

    public string Name
    {
      get => this._name;
      internal set => this._name = value;
    }

    public CommandQueueStatus Status
    {
      get => this._status;
      internal set
      {
        this._status = value;
        this.PropChanged(nameof (Status));
      }
    }

    public double ProgressValue
    {
      get => this._progressValue;
      internal set
      {
        this._progressValue = value;
        this.PropChanged(nameof (ProgressValue));
      }
    }

    private void PropChanged(string propname)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propname));
    }
  }
}
