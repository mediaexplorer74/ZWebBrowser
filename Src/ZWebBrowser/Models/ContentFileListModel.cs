// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.ContentFileListModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZWebBrowser.Models
{
  public class ContentFileListModel
  {
    private ObservableCollection<ContentFileModel> items = new ObservableCollection<ContentFileModel>();

    public void Add(ContentFileModel m) => this.items.Add(m);

    public void AddRange(IEnumerable<ContentFileModel> itemModels)
    {
      foreach (ContentFileModel itemModel in itemModels)
        this.items.Add(itemModel);
    }

    public ObservableCollection<ContentFileModel> Items => this.items;
  }
}
