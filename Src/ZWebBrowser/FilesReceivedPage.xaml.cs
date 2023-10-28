// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.FilesReceivedPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using ZHttpStockLib.Contents;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class FilesReceivedPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
       

        public FilesReceivedPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper((Page)this);
            this.navigationHelper.LoadState += new LoadStateEventHandler(
                this.NavigationHelper_LoadState);
            this.navigationHelper.SaveState += new SaveStateEventHandler(
                this.NavigationHelper_SaveState);
        }

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private IReadOnlyList<IStorageItem> ReceivedItemList { get; set; }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (e.NavigationParameter == null)
                return;
            this.ReceivedItemList = e.NavigationParameter as IReadOnlyList<IStorageItem>;
            this.msgBlock.put_Text("Received " + (object)this.ReceivedItemList.Count + " items");
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedFrom(e);

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await this.ShowMsgDialogAsync("Saved " + (object)
                await this.SaveReceivedFilesAsync(this.ReceivedItemList) + " items");

            this.Frame.Navigate(typeof(MainPage));

            this.Frame.BackStack.Remove(this.Frame.BackStack.FirstOrDefault<PageStackEntry>(
                (Func<PageStackEntry, bool>)(v => 
                (object)v.SourcePageType == (object)typeof(FilesReceivedPage))));
        }

        private async Task<int> SaveReceivedFilesAsync(
            IReadOnlyList<IStorageItem> receivedItemList)
        {
            int cnt = 0;
            if (this.ReceivedItemList != null && this.ReceivedItemList.Count > 0)
            {
                int count = this.ReceivedItemList.Count;
                foreach (IStorageItem receivedItem in 
                    (IEnumerable<IStorageItem>)this.ReceivedItemList)
                {
                    if (receivedItem is StorageFile)
                    {
                        StorageFile storageFile1 = receivedItem as StorageFile;
                        string extension = Path.GetExtension(receivedItem.Name);
                        ZWebBrowser.Diag.Debug.WriteLine("ext : " + extension);
                        string str1 = !extension.EndsWith("gifz") 
                            ? (!extension.EndsWith("htmlz") 
                            ? (!extension.EndsWith("htmz") 
                            ? receivedItem.Name 
                            : Path.ChangeExtension(receivedItem.Name, ".htm")) 
                            : Path.ChangeExtension(receivedItem.Name, ".html")) 
                            : Path.ChangeExtension(receivedItem.Name, ".gif");

                        StorageFolder contentFolder = ContentManager.ContentFolder;
                        string str2 = str1;

                        StorageFile storageFile2 = await storageFile1.CopyAsync(
                            (IStorageFolder)contentFolder, str2, (NameCollisionOption)0);
                        ++cnt;
                    }
                }
            }
            return cnt;
        }

        private async Task ShowMsgDialogAsync(string msg)
        {
            IUICommand iuiCommand = await new MessageDialog(msg).ShowAsync();
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
    public sealed partial class FilesReceivedPage : Page
    {
        public FilesReceivedPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
