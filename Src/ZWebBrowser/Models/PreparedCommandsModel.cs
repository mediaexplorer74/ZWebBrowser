// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.PreparedCommandsModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.Collections.ObjectModel;

namespace ZWebBrowser.Models
{
  public class PreparedCommandsModel
  {
    private ObservableCollection<PreparedCommandModel> items = new ObservableCollection<PreparedCommandModel>();

    public void Add(PreparedCommandModel m) => this.items.Add(m);

    public ObservableCollection<PreparedCommandModel> Items => this.items;
  }
}
