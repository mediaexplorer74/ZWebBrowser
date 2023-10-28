// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.RelayCommand
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Windows.Input;

namespace ZWebBrowser.Common
{
  public class RelayCommand : ICommand
  {
    private readonly Action _execute;
    private readonly Func<bool> _canExecute;

    public event EventHandler CanExecuteChanged;

    public RelayCommand(Action execute)
      : this(execute, (Func<bool>) null)
    {
    }

    public RelayCommand(Action execute, Func<bool> canExecute)
    {
      this._execute = execute != null ? execute : throw new ArgumentNullException(nameof (execute));
      this._canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => this._canExecute == null || this._canExecute();

    public void Execute(object parameter) => this._execute();

    public void RaiseCanExecuteChanged()
    {
      EventHandler canExecuteChanged = this.CanExecuteChanged;
      if (canExecuteChanged == null)
        return;
      canExecuteChanged((object) this, EventArgs.Empty);
    }
  }
}
