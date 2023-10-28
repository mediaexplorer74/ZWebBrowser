// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Compression.ZipCompression
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ZHttpStockLib.Compression
{
  public class ZipCompression
  {
    private int _running;

    private bool Compressing
    {
      get => this._running != 0;
      set => this._running = value ? 1 : 0;
    }

    public async Task<string> Compress(
      StorageFile[] files,
      StorageFile destFile,
      Action<int, int> progressChangedAction)
    {
      try
      {
        this.Compressing = true;
        int fileTotal = files != null ? ((IEnumerable<StorageFile>) files).Count<StorageFile>() : 0;
        int compressed = 0;
        using (IRandomAccessStream stream = await destFile.OpenAsync((FileAccessMode) 1))
        {
          using (ZipOutputStream s = new ZipOutputStream(stream.GetOutputStreamAt(0UL).AsStreamForWrite()))
          {
            s.SetLevel(9);
            byte[] buffer = new byte[4096];
            StorageFile[] storageFileArray = files;
            for (int index = 0; index < storageFileArray.Length; ++index)
            {
              StorageFile storageFile = storageFileArray[index];
              s.PutNextEntry(new ZipEntry(storageFile.Name)
              {
                DateTime = DateTime.Now
              });
              using (IRandomAccessStream fs = await storageFile.OpenAsync((FileAccessMode) 0))
              {
                //RnD
                DataReader dr = default;
                
                //RnD
                int num1 = 0;
                if (num1 != 2 && num1 != 3)
                  dr = new DataReader((IInputStream) fs);
                try
                {
                                  //RnD
                  uint sourceBytes = 0;
                  do
                  {
                    int num2 = (int) sourceBytes;
                    sourceBytes = await (IAsyncOperation<uint>) dr.LoadAsync((uint) buffer.Length);
                    if (sourceBytes > 0U)
                    {
                      dr.ReadBuffer(sourceBytes).ToArray().CopyTo((Array) buffer, 0);
                      await Task.Run((Action) (() => s.Write(buffer, 0, (int) sourceBytes)));
                    }
                  }
                  while (sourceBytes > 0U);
                }
                finally
                {
                  ((IDisposable) dr)?.Dispose();
                }
                dr = (DataReader) null;
              }
              ++compressed;
              progressChangedAction(compressed, fileTotal);
            }
            storageFileArray = (StorageFile[]) null;
            s.Finish();
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        this.Compressing = false;
      }
      return destFile.Name;
    }
  }
}
