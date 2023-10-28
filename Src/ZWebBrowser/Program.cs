// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Program
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;

namespace ZWebBrowser
{
  public static class Program
  {
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    private static void Main(string[] args)
    {
      App app;
      Application.Start((ApplicationInitializationCallback) (p => app = new App()));
    }
  }
}
