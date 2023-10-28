// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.AppSettingsPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class AppSettingsPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        

        public AppSettingsPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper((Page)this);
            this.navigationHelper.LoadState += new LoadStateEventHandler(
                this.NavigationHelper_LoadState);
            this.navigationHelper.SaveState += new SaveStateEventHandler(
                this.NavigationHelper_SaveState);
        }

        public NavigationHelper NavigationHelper 
            => this.navigationHelper;

        public ObservableDictionary DefaultViewModel 
            => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedFrom(e);

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
    public sealed partial class AppSettingsPage : Page
    {
        public AppSettingsPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
