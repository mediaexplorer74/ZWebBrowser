// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.ExtPromptDialog
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;

namespace ZWebBrowser
{
    public sealed partial class ExtPromptDialog : ContentDialog
    {
        private string newExt;      
        

        public ExtPromptDialog(string origExt, 
            string dialogTitle = null,
            string textHeader = null)
        {
            this.InitializeComponent();
            this.SelectedExt = origExt;
            this.DialogTitle = dialogTitle;
            this.TextHeader = textHeader;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ContentDialog, 
                ContentDialogOpenedEventArgs>>(
                new Func<TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>, 
                EventRegistrationToken>(((ContentDialog)this).add_Opened), 
                new Action<EventRegistrationToken>(((ContentDialog)this).remove_Opened),
                new TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>((object)this,
                __methodptr(ExtPromptDialog_Opened)));
        }

        public string DialogTitle { get; set; }

        public string TextHeader { get; set; }

        public string SelectedExt
        {
            get => this.newExt;
            set => this.newExt = value;
        }

        private string SelectedExtBox
        {
            get => this.extBox.Text;
            set => this.extBox.Text = value;
        }

        private void ExtPromptDialog_Opened(ContentDialog sender, 
            ContentDialogOpenedEventArgs args)
        {
            this.SelectedExtBox = this.SelectedExt;
            this.Title = (object)this.DialogTitle;
            this.extBox.Header = (object)this.TextHeader;
            ((Control)this.extBox).Focus((FocusState)3);
            this.extBox.SelectAll();
        }

        private void ContentDialog_PrimaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
            this.SelectedExt = this.SelectedExtBox;
        }

        private void ContentDialog_SecondaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
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

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZWebBrowser
{
    public sealed partial class ExtPromptDialog : ContentDialog
    {
        public ExtPromptDialog()
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
