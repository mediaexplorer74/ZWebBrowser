// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.Util.StreamUriWinRTResolver
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;
using ZWebBrowser.Diag;

namespace ZWebBrowser.Util
{
  public sealed class StreamUriWinRTResolver : IUriToStreamResolver
  {
    public static readonly string PathTemp = "ms-appdata:///temp";
    private string pathBase;

    public StreamUriWinRTResolver(string pathBase) => this.pathBase = pathBase;

    public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri) => !(uri == (Uri) null) ? this.GetContent(uri.AbsolutePath).AsAsyncOperation<IInputStream>() : throw new Exception();

    private async Task<IInputStream> GetContent(string URIPath)
    {
      try
      {
        Uri localUri = new Uri(this.pathBase + URIPath);
        StorageFile f;
        IInputStream stream;
        try
        {
          Debug.WriteLine("reading :" + localUri.LocalPath);
          f = await StorageFile.GetFileFromPathAsync(localUri.LocalPath);
          stream = await f.OpenSequentialReadAsync();
        }
        catch (FileNotFoundException ex1)
        {
          Debug.WriteLine("not found:" + localUri.LocalPath);
          try
          {
            f = await ApplicationData.Current.TemporaryFolder.GetFileAsync("empty.txt");
          }
          catch (FileNotFoundException ex2)
          {
            Debug.WriteLine("inner not found");
            f = await this.CreateEmptyTmpFile("empty.txt");
          }
          stream = await f.OpenSequentialReadAsync();
        }
        return stream;
      }
      catch (Exception ex)
      {
        Debug.WriteLine("exception : " + URIPath);
        return (IInputStream) null;
      }
    }

    private async Task<StorageFile> CreateEmptyTmpFile(string name) => await ApplicationData.Current.TemporaryFolder.CreateFileAsync(name, (CreationCollisionOption) 0);
  }
}
