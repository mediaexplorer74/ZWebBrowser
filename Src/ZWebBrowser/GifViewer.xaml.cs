// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.GifViewer
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml.Markup;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class GifViewer : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();


        public GifViewer()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper((Page)this);
            this.navigationHelper.LoadState += new LoadStateEventHandler(this.NavigationHelper_LoadState);
            this.navigationHelper.SaveState += new SaveStateEventHandler(this.NavigationHelper_SaveState);
        }

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter == null)
                return;
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            this.DisplayGif(e.NavigationParameter.ToString()).ContinueWith((Action<Task<StorageFile>>)(v => ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, new DispatchedHandler((object)new GifViewer.\u003C\u003Ec__DisplayClass7_0()
            {
        \u003C\u003E4__this = this,
        v = v
      }, __methodptr(\u003CNavigationHelper_LoadState\u003Eb__1)))));
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e) => this.navigationHelper.OnNavigatedFrom(e);

        private async Task<StorageFile> DisplayGif(string imgPath)
        {
            StorageFile htmlf = await this.WriteTempHtml("dtemp.html", "<!DOCTYPE html><html lang='zh-CN'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" + "<style> body {background-color: #000000;  } </style>" + "</head><body>" + "<img src=\"" + Path.GetFileName(imgPath) + "\" alt=\"testalt\"/>" + "</body></html>");
            StorageFile storageFile = await (await StorageFile.GetFileFromPathAsync(imgPath)).CopyAsync((IStorageFolder)ApplicationData.Current.TemporaryFolder, Path.GetFileName(imgPath), (NameCollisionOption)1);
            return htmlf;
        }

        private async Task<StorageFile> WriteTempHtml(string name, string content)
        {
            StorageFile file = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(name, (CreationCollisionOption)1);
            using (DataWriter dw = new DataWriter((IOutputStream)await file.OpenAsync((FileAccessMode)1)))
            {
                dw.WriteBytes(Encoding.UTF8.GetBytes(content));
                int num = (int)await (IAsyncOperation<uint>)dw.StoreAsync();
            }
            return file;
        }

    }
}


/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZWebBrowser
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class GifViewer : Page
    {
        public GifViewer()
        {
            this.InitializeComponent();
        }
    }
}
*/
