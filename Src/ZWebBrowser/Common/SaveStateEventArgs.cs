// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.SaveStateEventArgs
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Collections.Generic;

namespace ZWebBrowser.Common
{
  public class SaveStateEventArgs : EventArgs
  {
    public Dictionary<string, object> PageState { get; private set; }

    public SaveStateEventArgs(Dictionary<string, object> pageState) => this.PageState = pageState;
  }
}
