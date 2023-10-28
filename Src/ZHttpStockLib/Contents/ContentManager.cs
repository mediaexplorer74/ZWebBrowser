// Decompiled with JetBrains decompiler
// Type: ZHttpStockLib.Contents.ContentManager
// Assembly: ZHttpStockLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2407C9FF-3B12-4AE9-A5BE-E742A14A1C0A
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZHttpStockLib.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ZHttpStockLib.Contents
{
  public class ContentManager
  {
    private static ContentManager managerInstance;
    private ContentList contentList;

    private ContentManager()
    {
    }

        public static StorageFolder ContentFolder
        {
            get
            {
                return ApplicationData.Current.LocalFolder;
            }
        }

        public ContentList ContentLists
    {
      get
      {
        if (this.contentList == null)
          this.contentList = new ContentList();
        return this.contentList;
      }
    }

    private static ContentManager ManagerInstance
    {
      get
      {
        if (ContentManager.managerInstance == null)
          ContentManager.managerInstance = new ContentManager();
        return ContentManager.managerInstance;
      }
    }

        public static ContentManager GetInstance()
        {
            return ContentManager.ManagerInstance;
        }

        public ContentList GetContentList()
    {
      this.ContentLists.Clear();
      this.LoadContentList();
      return this.ContentLists;
    }

        private void LoadContentList()
        {
            this.ContentLists.AddRange(ContentFile.LoadAllAsync().Result);
        }

        public async Task<uint> SaveContentAsync(ContentFile contentFile, IBuffer buf)
    {
      uint num = 0;
      using (IRandomAccessStream stream = await contentFile.GetStreamAsync((FileAccessMode) 1))
      {
        using (DataWriter dw = new DataWriter((IOutputStream) stream))
        {
          dw.WriteBuffer(buf);
          num = await (IAsyncOperation<uint>) dw.StoreAsync();
          dw.DetachStream();
        }
      }
      return num;
    }

    public async Task<IBuffer> LoadContentAsync(ContentFile contentFile)
    {
      uint num1 = 0;
      IBuffer ibuffer = (IBuffer) null;
      using (IRandomAccessStream stream = await contentFile.GetStreamAsync((FileAccessMode) 0))
      {
        using (DataReader dr = new DataReader((IInputStream) stream))
        {
          uint num2;
          do
          {
            num2 = await (IAsyncOperation<uint>) dr.LoadAsync(1000U);
          }
          while (0U < (num1 = num2));
          ibuffer = dr.ReadBuffer(dr.UnconsumedBufferLength);
          dr.DetachStream();
        }
      }
      return ibuffer;
    }

    public async Task DeleteContentAsync(IEnumerable<ContentFile> fl)
    {
      foreach (ContentFile contentFile in fl)
      {
        try
        {
          await contentFile.DeleteAsync();
        }
        catch (FileNotFoundException ex)
        {
            Debug.WriteLine(ex.Message);
        }
      }
    }
  }
}
