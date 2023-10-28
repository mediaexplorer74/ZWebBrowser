// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.RecentScriptsPage
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
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class RecentScriptsPage : Page
    {
        private NavigationHelper navigationHelper;

        private ObservableDictionary defaultViewModel 
            = new ObservableDictionary();

        private ObservableCollection<ScriptItemModel> 
            _items = new ObservableCollection<ScriptItemModel>();

        

        public RecentScriptsPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper((Page)this);

            this.navigationHelper.LoadState += new LoadStateEventHandler(
                this.NavigationHelper_LoadState);

            this.navigationHelper.SaveState += new SaveStateEventHandler(
                this.NavigationHelper_SaveState);
        }

        private ObservableCollection<ScriptItemModel> Items => this._items;

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
            => this.LoadScriptList().ContinueWith((Action<Task>)(v => 
            ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0,
                new DispatchedHandler((object)this,
                    __methodptr(\u003CNavigationHelper_LoadState\u003Eb__10_1)))));

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
            => this.navigationHelper.OnNavigatedFrom(e);

        private async Task LoadScriptList()
        {
            foreach (string i in (IEnumerable<string>)await (
                await ScriptsStore.GetInstanceAsync()).ScriptsListAsync())
                this.Items.Add(new ScriptItemModel(i));
        }

        private void newScriptBn_Click(object sender, RoutedEventArgs e)
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZWebBrowser
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class RecentScriptsPage : Page
    {
        public RecentScriptsPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
