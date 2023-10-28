// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.PreparedCommands
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
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
using ZHttpStockLib.Commands;
using ZWebBrowser.Common;
using ZWebBrowser.Models;

namespace ZWebBrowser
{
    public sealed partial class PreparedCommands : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private PreparedCommandsModel commandListModel = new PreparedCommandsModel();
        
        public PreparedCommands()
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

        public PreparedCommandsModel CommandListModel => this.commandListModel;

        public static CoreDispatcher ZDispatcher { get; set; }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.LoadCommandList();
            ((FrameworkElement)this.listView).put_DataContext((object)this.CommandListModel);
            PreparedCommands.ZDispatcher = ((DependencyObject)this).Dispatcher;
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
            => this.navigationHelper.OnNavigatedFrom(e);

        private void LoadCommandList()
        {
            foreach (PreparedCommandModel m in ((App)Application.Current)
                .CommandManagerInst.CommandList.Queue
                .Select<CommandQueueItem, PreparedCommandModel>(
                (Func<CommandQueueItem, PreparedCommandModel>)(c => new PreparedCommandModel(c))))
                this.CommandListModel.Add(m);
        }

        private void appBarAccept_Click(object sender, RoutedEventArgs e)
            => Task.Run((Action)(() => this.ProcessCommandList()));

        private void ProcessCommandList()
        {
            CommandManager commandManagerInst = ((App)Application.Current).CommandManagerInst;
            commandManagerInst.Schedule();
            commandManagerInst.Start();
        }

        private void appBarContentViewer_Click(object sender, RoutedEventArgs e) 
            => this.Frame.Navigate(typeof(ContentViewerPage));

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void appBarCancel_Click(object sender, RoutedEventArgs e) 
            => ((App)Application.Current).CommandManagerInst.Stop();
             
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
    public sealed partial class PreparedCommands : Page
    {
        public PreparedCommands()
        {
            this.InitializeComponent();
        }
    }
}
*/
