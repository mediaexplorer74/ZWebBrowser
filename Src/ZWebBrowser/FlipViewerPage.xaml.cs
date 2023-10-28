// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.FlipViewerPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
using Windows.UI.Core;
using Windows.UI.Xaml.Markup;
using ZHttpStockLib.Contents;
using ZWebBrowser.Common;
using ZWebBrowser.Models;

namespace ZWebBrowser
{
    public sealed partial class FlipViewerPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
       

        public FlipViewerPage()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper((Page)this);

            this.navigationHelper.LoadState += new LoadStateEventHandler(
                this.NavigationHelper_LoadState);

            this.navigationHelper.SaveState += new SaveStateEventHandler
                this.NavigationHelper_SaveState);
        }

        public NavigationHelper NavigationHelper => this.navigationHelper;

        public ObservableDictionary DefaultViewModel => this.defaultViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            ContentFile sel = (ContentFile)null;
            if (e.NavigationParameter != null)
                sel = e.NavigationParameter as ContentFile;
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            this.LoadContentFiles().ContinueWith((Action<Task<IEnumerable<ContentFile>>>)(
                t => ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, 
                new DispatchedHandler((object)new FlipViewerPage.\u003C\u003Ec__DisplayClass7_0()
            {
        CS\u0024\u003C\u003E8__locals1 = this,
        t = t
      }, __methodptr(\u003CNavigationHelper_LoadState\u003Eb__1)))));
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e) 
            => this.ReleaseFlipView();

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedFrom(e);

        private async Task<IEnumerable<ContentFile>> LoadContentFiles()
            => (await Task.Run<ContentList>((Func<ContentList>)(()
                => ContentManager.GetInstance().GetContentList())))
            .AllItems().Where<ContentFile>((Func<ContentFile, bool>)(v =>
            Regex.IsMatch(v.Path, "(.*?)\\.(gif|jpg|jpeg|png|bmp)$")));

        private void FlipViewModelSetSelected(ContentFile selectedItem)
        {
            if (selectedItem == null || ((ItemsControl)this.flipView).ItemsSource == null)
                return;
            object obj = ((IEnumerable<object>)((ItemsControl)this.flipView).Items)
                .FirstOrDefault<object>((Func<object, bool>)(v => 
                ((FlipViewImgModel)v).ImgUri == selectedItem.Path));

            if (obj == null)
                return;
            ((Selector)this.flipView).SelectedItem = obj;
        }

        private void LoadFlipViewModels(IEnumerable<ContentFile> items) 
            => ((ItemsControl)this.flipView).put_ItemsSource((object)new 
                ObservableCollection<FlipViewImgModel>(
                items.Select<ContentFile, FlipViewImgModel>(
                    (Func<ContentFile, FlipViewImgModel>)(
                    v => new FlipViewImgModel(v.Path, v.Name)))));

        private void ReleaseFlipView()
        {
            object itemsSource = ((ItemsControl)this.flipView).ItemsSource;
            ((ItemsControl)this.flipView).ItemsSource = (object)null;
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
    public sealed partial class FlipViewerPage : Page
    {
        public FlipViewerPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
