// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Scripts.ScriptsStore
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;

namespace ZHttpStockLib.Scripts
{
  public class ScriptsStore : ScriptsStoreBase
  {
    private static ScriptsStore _inst;
    private readonly int historyKeeps = 20;

    private static string ScriptFolderPath => ApplicationData.Current.LocalFolder.Path + "\\scripts";

    public static async Task<ScriptsStore> GetInstanceAsync()
    {
      if (ScriptsStore._inst == null)
      {
        ScriptsStore._inst = new ScriptsStore();
        StorageFolder sf;
        try
        {
          sf = await StorageFolder.GetFolderFromPathAsync(ScriptsStore.ScriptFolderPath);
        }
        catch (FileNotFoundException ex)
        {
          Debug.WriteLine(ex.Message);
          sf = await ApplicationData.Current.LocalFolder.CreateFolderAsync("scripts");
        }
        ScriptsStore._inst.ScriptFolder = sf;
      }
      return ScriptsStore._inst;
    }

    public async Task ClearOutdatedScripts()
    {
      StorageFile[] array = (await this.ScriptFolder.GetFilesAsync()).OrderByDescending<StorageFile, DateTimeOffset>((Func<StorageFile, DateTimeOffset>) (x => x.DateCreated)).ToArray<StorageFile>();
      if (((IEnumerable<StorageFile>) array).Count<StorageFile>() <= this.historyKeeps)
        return;
      foreach (StorageFile storageFile in ((IEnumerable<StorageFile>) array).Skip<StorageFile>(this.historyKeeps))
        await storageFile.DeleteAsync((StorageDeleteOption) 1);
    }
  }
}
