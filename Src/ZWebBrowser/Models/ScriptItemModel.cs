// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.ScriptItemModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

namespace ZWebBrowser.Models
{
  public class ScriptItemModel
  {
    private string _name;

    public ScriptItemModel(string name) => this._name = name;

    public string Name
    {
      get => this._name;
      set => this._name = value;
    }
  }
}
