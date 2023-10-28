// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.PreparedCommandModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.ComponentModel;
using Windows.UI.Core;
using ZHttpStockLib.Commands;

namespace ZWebBrowser.Models
{
  public class PreparedCommandModel : INotifyPropertyChanged
  {
    private CommandQueueItem cmd;

    public event PropertyChangedEventHandler PropertyChanged;

    public PreparedCommandModel(CommandQueueItem cmd)
    {
      this.cmd = cmd;
      this.cmd.PropertyChanged += new PropertyChangedEventHandler(this.Cmd_PropertyChanged);
    }

    private void Cmd_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if ("Status" == e.PropertyName)
      {
        this.PropChanged("Status");
      }
      else
      {
        if (!("ProgressValue" == e.PropertyName))
          return;
        this.PropChanged("ProgressValue");
      }
    }

    public CommandQueueItem Cmd => this.cmd;

    public string Title => this.cmd.Name;

    public string Status => this.Cmd.Status.ToString();

    public double ProgressValue => this.Cmd.ProgressValue;

    private void PropChanged(string propName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PreparedCommandModel.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140 = new PreparedCommandModel.\u003C\u003Ec__DisplayClass14_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass140.propName = propName;
      if (this.PropertyChanged == null)
        return;
      // ISSUE: method pointer
      PreparedCommands.ZDispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) cDisplayClass140, __methodptr(\u003CPropChanged\u003Eb__0)));
    }
  }
}
