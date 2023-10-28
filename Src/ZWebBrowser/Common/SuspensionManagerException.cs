// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Common.SuspensionManagerException
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;

namespace ZWebBrowser.Common
{
  public class SuspensionManagerException : Exception
  {
    public SuspensionManagerException()
    {
    }

    public SuspensionManagerException(Exception e)
      : base("SuspensionManager failed", e)
    {
    }
  }
}
