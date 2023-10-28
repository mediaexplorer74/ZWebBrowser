// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.UrlDialog
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Markup;

namespace ZWebBrowser
{
    public sealed partial class UrlDialog : ContentDialog
    {
        private string urlString;
       
        public UrlDialog(string defaultUrl)
        {
            this.urlString = defaultUrl;
            this.InitializeComponent();
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ContentDialog, 
                ContentDialogOpenedEventArgs>>(
                new Func<TypedEventHandler<ContentDialog,
                ContentDialogOpenedEventArgs>, EventRegistrationToken>(
                    ((ContentDialog)this).add_Opened),
                new Action<EventRegistrationToken>(((ContentDialog)this).remove_Opened),
                new TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>(
                    (object)this, __methodptr(UrlDialog_Opened)));
        }

        private void UrlDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            this.url.Text = this.urlString;
            ((Control)this.url).Focus((FocusState)3);
            this.url.SelectAll();
        }

        public string UrlString
        {
            get => this.urlString;
            set => this.urlString = value;
        }

        private void ContentDialog_PrimaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
            this.url.Text = this.BuildUrl(this.url.Text);
            this.urlString = this.url.Text;
        }

        private void ContentDialog_SecondaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
        }

        private string BuildUrl(string urlString) 
            => !urlString.Contains("://")
            && !string.IsNullOrWhiteSpace(urlString)
            ? "http://" + urlString : urlString;

        private void Button_Click(object sender, RoutedEventArgs e) 
            => this.url.Text = "";

        
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

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZWebBrowser
{
    public sealed partial class UrlDialog : ContentDialog
    {
        public UrlDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
*/
