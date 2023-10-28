// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.HelpViewPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class HelpViewPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private readonly ResourceLoader resourceLoader =
            ResourceLoader.GetForCurrentView("Resources");



        public HelpViewPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper((Page)this);

            this.navigationHelper.LoadState
                += new LoadStateEventHandler(this.NavigationHelper_LoadState);

            this.navigationHelper.SaveState
                += new SaveStateEventHandler(this.NavigationHelper_SaveState);
        }

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
            => this.LoadHelpContent();

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
            => this.RemoveHelpContent();

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
            => this.navigationHelper.OnNavigatedFrom(e);

        private void LoadHelpContent()
            => this.textBlock.Text = this.resourceLoader.GetString("HelpContent");

        private void RemoveHelpContent() => this.textBlock.Text = string.Empty;

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
    public sealed partial class HelpViewPage : Page
    {
        public HelpViewPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
