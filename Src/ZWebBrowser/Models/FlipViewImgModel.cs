// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Models.FlipViewImgModel
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

namespace ZWebBrowser.Models
{
  public class FlipViewImgModel
  {
    public FlipViewImgModel(string imgUri, string desc)
    {
      this.ImgUri = imgUri;
      this.Desc = desc;
    }

    public string Desc { get; set; }

    public string ImgUri { get; set; }
  }
}
