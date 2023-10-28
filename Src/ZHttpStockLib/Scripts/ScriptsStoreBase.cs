// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Scripts.ScriptsStoreBase
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ZHttpStockLib.Scripts
{
  public class ScriptsStoreBase
  {
    private StorageFolder _scriptFolder;

    public StorageFolder ScriptFolder
    {
      get => this._scriptFolder;
      internal set => this._scriptFolder = value;
    }

    public async Task<StorageFile> SaveScriptAsync(string scriptContent)
    {
      StorageFile file = await this.CreateFileAsync(this.NewFileName());
      int num = (int) await this.WriteFile(scriptContent, file);
      return file;
    }

    public async Task<string> LoadScriptAsync(string name) => await this.ReadFile(await this.GetScriptFileFromNameAsync(name));

    public async Task<IList<string>> ScriptsListAsync() => (IList<string>) (await this.ScriptFolder.GetFilesAsync()).OrderByDescending<StorageFile, DateTimeOffset>((Func<StorageFile, DateTimeOffset>) (x => x.DateCreated)).Select<StorageFile, string>((Func<StorageFile, string>) (v => v.DisplayName)).ToList<string>();

    private async Task<StorageFile> GetScriptFileFromNameAsync(string name) => await this.ScriptFolder.GetFileAsync(name + ".txt");

    private string NewFileName() => string.Format("{0:yyyyMMdd_HHmmss_ffff}.txt", new object[1]
    {
      (object) DateTime.Now
    });

    private async Task<StorageFile> CreateFileAsync(string name) => await this.ScriptFolder.CreateFileAsync(name, (CreationCollisionOption) 0);

    private async Task<uint> WriteFile(string content, StorageFile file)
    {
      uint num1 = 0;
      using (DataWriter dw = new DataWriter((IOutputStream) await file.OpenAsync((FileAccessMode) 1)))
      {
        int num2 = (int) dw.WriteString(content);
        num1 = await (IAsyncOperation<uint>) dw.StoreAsync();
      }
      return num1;
    }

    private async Task<string> ReadFile(StorageFile file)
    {
      uint num1 = 0;
      string str = (string) null;
      using (DataReader dr = new DataReader((IInputStream) await file.OpenReadAsync()))
      {
        uint num2;
        do
        {
          num2 = await (IAsyncOperation<uint>) dr.LoadAsync(1000U);
        }
        while (0U < (num1 = num2));
        str = dr.ReadString(dr.UnconsumedBufferLength);
      }
      return str;
    }
  }
}
