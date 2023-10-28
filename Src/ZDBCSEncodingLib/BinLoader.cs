// Decompiled with JetBrains decompiler
// Type: ZDBCSEncodingLib.BinLoader
// Assembly: ZDBCSEncodingLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 13FE3C2F-1C9E-4DED-9F9F-903867DA5F3C
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZDBCSEncodingLib.dll

using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage.Streams;

namespace ZDBCSEncodingLib
{
  public class BinLoader
  {
    public static async Task<IRandomAccessStream> GetBinStreamAsync(string name)
    {
      IRandomAccessStream binStreamAsync;
      try
      {
        binStreamAsync = (IRandomAccessStream) await (await (await Package.Current.InstalledLocation.GetFolderAsync("Maps")).GetFileAsync(name)).OpenReadAsync();
      }
      catch
      {
        throw new InvalidOperationException("Invalid encoding binary file name.");
      }
      return binStreamAsync;
    }
  }
}
