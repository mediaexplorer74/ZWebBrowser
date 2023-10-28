// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.LoadStateEventArgs
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Collections.Generic;

namespace ZWebBrowser.Common
{
  public class LoadStateEventArgs : EventArgs
  {
    public object NavigationParameter { get; private set; }

    public Dictionary<string, object> PageState { get; private set; }

    public LoadStateEventArgs(object navigationParameter, Dictionary<string, object> pageState)
    {
      this.NavigationParameter = navigationParameter;
      this.PageState = pageState;
    }
  }
}
