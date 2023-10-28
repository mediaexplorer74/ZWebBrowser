// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Contents.ContentFile
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ZHttpStockLib.Contents
{
  public class ContentFile
  {
    private StorageFile file;

    private ContentFile(StorageFile file) => this.file = file;

    public StorageFile File => this.file;

    public string Name => this.file.Name;

    public string Path => this.file.Path;

    public async Task RenameAsync(string toName) => await this.file.RenameAsync(toName);

    public static async Task<ContentFile> CreateAsync(string name, CreationCollisionOption option = 0) => new ContentFile(await ApplicationData.Current.LocalFolder.CreateFileAsync(name, option));

    public static async Task<ContentFile> LoadAsync(string path) => new ContentFile(await StorageFile.GetFileFromPathAsync(path));

    public static async Task<ContentFile[]> LoadAllAsync() => (await ApplicationData.Current.LocalFolder.GetFilesAsync()).Select<StorageFile, ContentFile>((Func<StorageFile, ContentFile>) (v => new ContentFile(v))).ToArray<ContentFile>();

    public async Task<IRandomAccessStream> GetStreamAsync(FileAccessMode mode) => await this.file.OpenAsync(mode);

    public static string FileNameEscape(string name) => Regex.Replace(name, "\\/|\\\\|\\*|\\:|\\?|\\<|\\>|\\|", "_");

    internal async Task DeleteAsync() => await this.file.DeleteAsync();

    public async Task<uint> WriteFile(byte[] bytes) => await this.WriteFile(bytes, this.File);

    public async Task<uint> WriteFile(byte[] bytes, ContentFile file) => await this.WriteFile(bytes, file.File);

    public async Task<uint> WriteFile(byte[] bytes, StorageFile file)
    {
      uint num = 0;
      using (DataWriter dw = new DataWriter((IOutputStream) await file.OpenAsync((FileAccessMode) 1)))
      {
        dw.WriteBytes(bytes);
        num = await (IAsyncOperation<uint>) dw.StoreAsync();
      }
      return num;
    }
  }
}
