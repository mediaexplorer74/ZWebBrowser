// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.ContentEditorPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using DBCSCodePage;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using ZHttpStockLib.Contents;
using ZHttpStockLib.Contents.Temp;
using ZHttpStockLib.Path;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class ContentEditorPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private string filePath;
        private static readonly string EncodingUTF8 = "UTF-8";
        private static readonly string EncodingANSI = "ANSI";
       
        

        public ContentEditorPage()
        {
            this.InitializeComponent();
            this.InitFileEncodingBox();

            this.navigationHelper = new NavigationHelper((Page)this);

            this.navigationHelper.LoadState += new LoadStateEventHandler(
                this.NavigationHelper_LoadState);

            this.navigationHelper.SaveState += new SaveStateEventHandler(
                this.NavigationHelper_SaveState);
        }

        public string FileEncoding
        {
            get => ((Selector)this.encodingBox).SelectedItem as string;
            set => ((Selector)this.encodingBox).put_SelectedItem((object)value);
        }

        private void InitFileEncodingBox()
        {
            ((ICollection<object>)((ItemsControl)this.encodingBox).Items).Clear();
            ((ICollection<object>)((ItemsControl)this.encodingBox).Items).Add(
                (object)ContentEditorPage.EncodingUTF8);

            ((ICollection<object>)((ItemsControl)this.encodingBox).Items).Add(
                (object)ContentEditorPage.EncodingANSI);
        }

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            string path = e.NavigationParameter.ToString();
            this.filePath = path;
            if (ContentEditorPage.EncodingUTF8 == this.FileEncoding)
            {
                // ISSUE: variable of a compiler-generated type
                ContentEditorPage.\u003C\u003Ec__DisplayClass14_0 cDisplayClass140;
                Task.Run((Func<Task>)(async () =>
                {
                    string str = await this.LoadContent(path, ContentEditorPage.EncodingUTF8);
                    // ISSUE: object of a compiler-generated type is created
                    // ISSUE: variable of a compiler-generated type
                    ContentEditorPage.\u003C\u003Ec__DisplayClass14_1 cDisplayClass141
                    = new ContentEditorPage.\u003C\u003Ec__DisplayClass14_1();
                    // ISSUE: reference to a compiler-generated field
                    cDisplayClass141.CS\u0024\u003C\u003E8__locals1 = cDisplayClass140;
                    StringBuilder stringBuilder = new StringBuilder(str.Length);
                    stringBuilder.Append(str);
                    // ISSUE: reference to a compiler-generated field
                    cDisplayClass141.buf = stringBuilder.ToString();
                    // ISSUE: method pointer
                    ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0,
                        new DispatchedHandler((object)cDisplayClass141, __methodptr(\u003CNavigationHelper_LoadState\u003Eb__1)));
                }));
            }
            else
                this.FileEncoding = ContentEditorPage.EncodingUTF8;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.contentBox.Text))
                this.contentBox.put_Text("");
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private async Task<string> LoadContent(string path, string encoding)
        {
            ContentFile contentFile = await ContentFile.LoadAsync(path);
            IBuffer buf = await ContentManager.GetInstance().LoadContentAsync(contentFile);

            return (await this.GetFileEncodingInst(encoding))
                .GetString(buf.ToArray(), 0, (int)buf.Length);
        }

        private async void appBarShowInWeb_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.contentBox.Text))
                return;
            string tpath = await TmpContentFile.CreateTempAsync(".html");
            Encoding fileEncodingInst = await this.GetFileEncodingInst(this.FileEncoding);
            string text = this.contentBox.Text;
            if (await TmpContentFile.WriteAsync(tpath, fileEncodingInst.GetBytes(text)) > 0U)
                this.Frame.Navigate(typeof(MainPage), (object)tpath);
            tpath = (string)null;
        }

        private void encodingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string filePath = this.filePath;
            if (filePath == null)
                return;
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            this.LoadContent(filePath, this.FileEncoding).ContinueWith(
                (Action<Task<string>>)(v => ((DependencyObject)this)
                .Dispatcher.RunAsync((CoreDispatcherPriority)0, 
                new DispatchedHandler((object)new ContentEditorPage
                .\u003C\u003Ec__DisplayClass20_0()
            {
        \u003C\u003E4__this = this,
        v = v
      }, __methodptr(\u003CencodingBox_SelectionChanged\u003Eb__1)))));
        }

        private async Task<Encoding> GetFileEncodingInst(
            string encoding)
        {
            return ContentEditorPage.EncodingANSI == encoding
                      ? (Encoding)await DBCSEncoding.GetDBCSEncodingAsync("gb2312")
                      : Encoding.UTF8;
        }

        private void appBarBack_Click(object sender, RoutedEventArgs e)
        {
            if (!this.Frame.CanGoBack)
                return;
            this.Frame.GoBack();
        }

        private async void appBarSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.filePath == null)
                return;
            string path = this.filePath;
            string content = this.contentBox.Text;
            Encoding fileEncodingInst = await this.GetFileEncodingInst(this.FileEncoding);
            await this.SaveTextEditorContentAsync(path, content, fileEncodingInst);
            path = (string)null;
            content = (string)null;
        }

        private async Task SaveTextEditorContentAsync(string path, string content, Encoding enc)
        {
            await this.PromptNewNameForSaveAsync(
                (await StorageFile.GetFileFromPathAsync(path)).Name, content, enc);
        }

        private async Task PromptNewNameForSaveAsync(string origFilename, 
            string content, Encoding enc)
        {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: variable of a compiler-generated type
            ContentEditorPage.\u003C\u003Ec__DisplayClass25_0 cDisplayClass250
                = new ContentEditorPage.\u003C\u003Ec__DisplayClass25_0();
            // ISSUE: reference to a compiler-generated field
            cDisplayClass250.\u003C\u003E4__this = this;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass250.origFilename = origFilename;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass250.content = content;
            // ISSUE: reference to a compiler-generated field
            cDisplayClass250.enc = enc;
            // ISSUE: reference to a compiler-generated field
            ExtPromptDialog epd = new ExtPromptDialog(cDisplayClass250.origFilename,
                "Save As", "File Name");
            ContentFile contentFile = (ContentFile)null;
            if (1 != await epd.ShowAsync())
                return;
            string newName = epd.SelectedExt;
            if (!string.IsNullOrWhiteSpace(newName))
            {
                try
                {
                    contentFile = await ContentFile.CreateAsync(newName,
                        (CreationCollisionOption)2);
                }
                catch (Exception ex)
                {
                    if (ExceptionHResult.FileExists == ex.HResult)
                    {
                        if (await this.PromptReplaceExistingAsync(newName))
                        {
                            contentFile = await ContentFile.CreateAsync(
                                newName, (CreationCollisionOption)1);
                            ex = (Exception)null;
                        }
                        else
                        {
                            // ISSUE: method pointer
                            ((DependencyObject)this).Dispatcher.RunAsync(
                                (CoreDispatcherPriority)0, new DispatchedHandler(
                                    (object)cDisplayClass250,
                                    __methodptr(\u003CPromptNewNameForSaveAsync\u003Eb__0)));
                            return;
                        }
                    }
                    else
                    {
                        if (ExceptionHResult.InvalidFileName != ex.HResult)
                            throw ex;
                        await ((App)Application.Current).ShowMsgDialogAsync(
                            "Invalid File Name : " + newName);
                        // ISSUE: method pointer
                        ((DependencyObject)this).Dispatcher.RunAsync(
                            (CoreDispatcherPriority)0, 
                            new DispatchedHandler((object)cDisplayClass250, 
                            __methodptr(\u003CPromptNewNameForSaveAsync\u003Eb__1)));
                        return;
                    }
                }
                // ISSUE: reference to a compiler-generated field
                // ISSUE: reference to a compiler-generated field
                await this.DoSaveEditorContentAsync(contentFile, 
                    cDisplayClass250.content, cDisplayClass250.enc);
            }
            newName = (string)null;
        }

        private async Task<bool> PromptReplaceExistingAsync(string name)
        {
            return await ((App)Application.Current).ShowConfirmDialogAsync("Replace '" + name + "' ?");
        }

        private async Task DoSaveEditorContentAsync(ContentFile file, string content, Encoding enc)
        {
            int num = (int)await file.WriteFile(enc.GetBytes(content), file);
        }

        private void appBarSelectAll_Click(object sender, RoutedEventArgs e)
        {
            ((Control)this.contentBox).Focus((FocusState)3);
            this.contentBox.SelectAll();
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
    public sealed partial class ContentEditorPage : Page
    {
        public ContentEditorPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
