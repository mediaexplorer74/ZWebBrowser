// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.ContentFileModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.ComponentModel;
using System.Text.RegularExpressions;
using ZHttpStockLib.Contents;

namespace ZWebBrowser.Models
{
  public class ContentFileModel : INotifyPropertyChanged
  {
    private string _visibility = ContentFileModel.VisibilityValue.Visible.ToString();
    private ContentFile file;

    public event PropertyChangedEventHandler PropertyChanged;

    public ContentFileModel(ContentFile file) => this.file = file;

    public ContentFile File => this.file;

    public string Title
    {
      get => this.file.Name;
      set => this.PropChanged(nameof (Title));
    }

    public string Path => this.file.Path;

    public string ImagePath => Regex.IsMatch(this.file.Path, "(.*?)\\.(gif|jpg|jpeg|png|bmp)$") ? this.file.Path : (string) null;

    public string Visibility
    {
      get => this._visibility;
      set
      {
        this._visibility = value;
        this.PropChanged(nameof (Visibility));
      }
    }

    private void PropChanged(string propName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propName));
    }

    public enum VisibilityValue
    {
      Collapsed,
      Visible,
    }
  }
}
