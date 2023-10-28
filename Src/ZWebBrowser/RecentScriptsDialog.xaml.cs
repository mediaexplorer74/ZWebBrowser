// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.RecentScriptsDialog
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using Windows.UI.Core;
using Windows.UI.Xaml.Markup;
using ZHttpStockLib.Scripts;

namespace ZWebBrowser
{
    public sealed partial class RecentScriptsDialog : ContentDialog
    {
        private ScriptsStoreBase scriptsBase;
        private ObservableCollection<ScriptItemModel> _items 
            = new ObservableCollection<ScriptItemModel>();
       

        public RecentScriptsDialog(ScriptsStoreBase scriptsBase)
        {
            this.InitializeComponent();
            this.scriptsBase = scriptsBase;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ContentDialog,
                ContentDialogOpenedEventArgs>>(
                new Func<TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>,
                EventRegistrationToken>(((ContentDialog)this).add_Opened), 
                new Action<EventRegistrationToken>(((ContentDialog)this).remove_Opened), 
                new TypedEventHandler<ContentDialog, ContentDialogOpenedEventArgs>((object)this,
                __methodptr(RecentScriptsDialog_Opened)));
        }

        private ScriptsStoreBase ScriptsStore => this.scriptsBase;

        public ObservableCollection<ScriptItemModel> Items => this._items;

        public string SelectedName { get; set; }

        private async void RecentScriptsDialog_Opened(
          ContentDialog sender,
          ContentDialogOpenedEventArgs args)
        {
            if (this.ScriptsStore is ZHttpStockLib.Scripts.ScriptsStore)
                await (this.ScriptsStore as ZHttpStockLib.Scripts.ScriptsStore)
                    .ClearOutdatedScripts();

            // ISSUE: method pointer
            this.LoadScriptList().ContinueWith((Action<Task>)(v => 
            ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, 
            new DispatchedHandler((object)this, 
            __methodptr(\u003CRecentScriptsDialog_Opened\u003Eb__11_1)))));
        }

        private void ContentDialog_PrimaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
            if (((Selector)this.listView).SelectedIndex > 0)
            {
                this.SelectedName = (((Selector)this.listView).SelectedItem as ScriptItemModel).Name;
            }
            else
            {
                this.SelectedName = (string)null;
            }
        }

        private void ContentDialog_SecondaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
        }

        private async Task LoadScriptList()
        {
            foreach (string i in (IEnumerable<string>)await this.ScriptsStore.ScriptsListAsync())
                this.Items.Add(new ScriptItemModel(i));
        }

        private void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.SelectedName = (e.ClickedItem as ScriptItemModel).Name;
            this.Hide();
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
    public sealed partial class RecentScriptsDialog : ContentDialog
    {
        public RecentScriptsDialog()
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