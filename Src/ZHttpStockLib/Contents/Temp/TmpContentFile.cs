// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Contents.Temp.TmpContentFile
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Security.Cryptography;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ZHttpStockLib.Contents.Temp
{
  public class TmpContentFile
  {
    public static async Task<string> CreateTempAsync(string ext) => (await ApplicationData.Current.TemporaryFolder.CreateFileAsync(TmpContentFile.GenerateRandomName() + ext, (CreationCollisionOption) 1)).Path;

    public static async Task<uint> WriteAsync(string path, byte[] buf)
    {
      uint num;
      using (DataWriter dw = new DataWriter((IOutputStream) await (await StorageFile.GetFileFromPathAsync(path)).OpenAsync((FileAccessMode) 1)))
      {
        dw.WriteBytes(buf);
        num = await (IAsyncOperation<uint>) dw.StoreAsync();
      }
      return num;
    }

    public static async Task<StorageFile> CopyToTempAsync(string toName, StorageFile fileToCopy) => await fileToCopy.CopyAsync((IStorageFolder) ApplicationData.Current.TemporaryFolder, toName, (NameCollisionOption) 1);

    private static string GenerateRandomName()
    {
      IBuffer random = CryptographicBuffer.GenerateRandom(8U);
      byte[] numArray = (byte[]) null;
      ref byte[] local = ref numArray;
      CryptographicBuffer.CopyToByteArray(random, out local);
      return BitConverter.ToString(numArray);
    }
  }
}
