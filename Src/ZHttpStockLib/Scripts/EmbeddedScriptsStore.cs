// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Scripts.EmbeddedScriptsStore
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;

namespace ZHttpStockLib.Scripts
{
  public class EmbeddedScriptsStore : ScriptsStoreBase
  {
    private static EmbeddedScriptsStore _inst;

    public static async Task<EmbeddedScriptsStore> GetInstanceAsync()
    {
      if (EmbeddedScriptsStore._inst == null)
      {
        EmbeddedScriptsStore._inst = new EmbeddedScriptsStore();
        StorageFolder sf;
        try
        {
          sf = await Package.Current.InstalledLocation.GetFolderAsync("EmbeddedScriptsFile");
        }
        catch (FileNotFoundException ex)
        {
           Debug.WriteLine(ex.Message);
           throw new FileNotFoundException("Installation files not found, try to reinstall the application.");
        }
        EmbeddedScriptsStore._inst.ScriptFolder = sf;
      }
      return EmbeddedScriptsStore._inst;
    }
  }
}
