// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.ContentViewerPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.IO;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using ZHttpStockLib.Compression;
using ZHttpStockLib.Contents;
using ZHttpStockLib.Contents.Temp;
using ZHttpStockLib.Path;
using ZWebBrowser.Common;
using ZWebBrowser.Models;


namespace ZWebBrowser
{
    public sealed partial class ContentViewerPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private ContentFileListModel contentFileListViewModel = new ContentFileListModel();
        private IEnumerable<ContentFileModel> fullItems;
        private DataTransferManager dataTransferManager;
       
        public ContentViewerPage()
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

        public ContentFileListModel ContentFileListViewModel => this.contentFileListViewModel;

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.StartProgressRing("Loading...");
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            Task.Run<IEnumerable<ContentFileModel>>((Func<IEnumerable<ContentFileModel>>)(
                () => this.LoadContentList())).ContinueWith(
                (Action<Task<IEnumerable<ContentFileModel>>>)(
                v => ((DependencyObject)this).Dispatcher.RunAsync(
                    (CoreDispatcherPriority)0, new DispatchedHandler(
                        (object)new ContentViewerPage.\u003C\u003Ec__DisplayClass12_0()
            {
        \u003C\u003E4__this = this,
        v = v
      }, __methodptr(\u003CNavigationHelper_LoadState\u003Eb__2)))));
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            WindowsRuntimeMarshal.AddEventHandler<EventHandler<BackPressedEventArgs>>(
                new Func<EventHandler<BackPressedEventArgs>,
                EventRegistrationToken>(HardwareButtons.add_BackPressed), 
                new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed),
                new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
            this.navigationHelper.OnNavigatedTo(e);
            this.dataTransferManager = DataTransferManager.GetForCurrentView();
            DataTransferManager dataTransferManager = this.dataTransferManager;

            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>>(new Func<TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>, EventRegistrationToken>(
                    dataTransferManager.add_DataRequested), 
                    new Action<EventRegistrationToken>(dataTransferManager.remove_DataRequested), 
                    new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(
                        (object)this, __methodptr(OnDataRequested)));
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<BackPressedEventArgs>>(
                new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed),
                new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
            this.navigationHelper.OnNavigatedFrom(e);
            // ISSUE: method pointer
            WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<DataTransferManager,
                DataRequestedEventArgs>>(new Action<EventRegistrationToken>(
                    this.dataTransferManager.remove_DataRequested),
                    new TypedEventHandler<DataTransferManager,
                    DataRequestedEventArgs>((object)this, __methodptr(OnDataRequested)));
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (((ListViewBase)this.listView).SelectionMode != 2)
                return;
            ((ListViewBase)this.listView).put_IsItemClickEnabled(true);
            ((ListViewBase)this.listView).put_SelectionMode((ListViewSelectionMode)1);
            ((Selector)this.listView).put_SelectedIndex(-1);
            e.put_Handled(true);
        }

        private void ClearContentList() => this.ContentFileListViewModel.Items.Clear();

        private IEnumerable<ContentFileModel> LoadContentList() 
            => ContentManager.GetInstance().GetContentList().AllItems()
            .Select<ContentFile, ContentFileModel>((Func<ContentFile, 
                ContentFileModel>)(v => new ContentFileModel(v)));

        private async void listView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((ListViewBase)this.listView).SelectionMode != 1)
                return;
            string path = (e.ClickedItem as ContentFileModel).File.File.Path;

            if (Regex.IsMatch(path, "(.*?)\\.(jpg|jpeg|png|bmp)$"))
                Launcher.LaunchFileAsync((IStorageFile)await StorageFile
                    .GetFileFromPathAsync(path));
            else if (Regex.IsMatch(path, "(.*?)\\.gif$"))
                this.Frame.Navigate(typeof(GifViewer), (object)path);
            else if (Regex.IsMatch(path, "(.*?)\\.((htm)|(html))$"))
                this.Frame.Navigate(typeof(MainPage), (object)path);
            else
                Launcher.LaunchFileAsync((IStorageFile)await StorageFile
                    .GetFileFromPathAsync(path));

            ((Selector)this.listView).SelectedItem = (object)(e.ClickedItem as ContentFileModel);
            path = (string)null;
        }

        private void appBarReload_Click(object sender, RoutedEventArgs e) 
            => this.ReloadContentList();

        private void ReloadContentList()
        {
            this.ClearContentList();
            this.StartProgressRing("Loading...");
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            Task.Run<IEnumerable<ContentFileModel>>((Func<IEnumerable<ContentFileModel>>)(
                () => this.LoadContentList())).ContinueWith(
                (Action<Task<IEnumerable<ContentFileModel>>>)(v => 
                ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, 
                new DispatchedHandler((object)new
                ContentViewerPage.\u003C\u003Ec__DisplayClass21_0()
            {
        \u003C\u003E4__this = this,
        v = v
      }, __methodptr(\u003CReloadContentList\u003Eb__2)))));
        }

        private void filterBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.fullItems == null || this.fullItems.Count<ContentFileModel>() < 1)
                return;
            string cd = this.filterBox.Text;
            this.ClearContentList();
            if (!string.IsNullOrEmpty(cd))
                this.ContentFileListViewModel.AddRange(this.fullItems.Where<ContentFileModel>(
                    (Func<ContentFileModel, bool>)(v => v.Title.Contains(cd))));
            else
                this.ContentFileListViewModel.AddRange(this.fullItems);
        }

        private void StackPanel_Holding(object sender, HoldingRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            e.put_Handled(true);
        }

        private async void ImageMenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is MenuFlyoutItem item))
                return;
            if (((FrameworkElement)item).DataContext is ContentFileModel model)
            {
                // ISSUE: object of a compiler-generated type is created
                // ISSUE: variable of a compiler-generated type
                ContentViewerPage.\u003C\u003Ec__DisplayClass24_0 cDisplayClass240 
                    = new ContentViewerPage.\u003C\u003Ec__DisplayClass24_0();
                // ISSUE: reference to a compiler-generated field
                cDisplayClass240.\u003C\u003E4__this = this;
                // ISSUE: reference to a compiler-generated field
                cDisplayClass240.path = model.File.File.Path;
                if (((FrameworkElement)item).Tag.ToString() == "ViewText")
                {
                    // ISSUE: method pointer
                    ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0,
                        new DispatchedHandler((object)cDisplayClass240, 
                        __methodptr(\u003CImageMenuFlyoutItem_Click\u003Eb__0)));
                }
                else if (((FrameworkElement)item).Tag.ToString() == "Browse")
                {
                    // ISSUE: reference to a compiler-generated field
                    this.Frame.Navigate(typeof(MainPage), (object)cDisplayClass240.path);
                }
                else if (((FrameworkElement)item).Tag.ToString() == "OpenWith")
                {
                    // ISSUE: reference to a compiler-generated field
                    await this.OpenWithExt(cDisplayClass240.path);
                }
                else if (((FrameworkElement)item).Tag.ToString() == "Rename")
                {
                    await this.RenameFile(model.File);
                    model.Title = "dummy";
                }
            }
            model = (ContentFileModel)null;
        }

        private async Task RenameFile(ContentFile file)
        {
            bool renamedOk = false;
            while (true)
            {
                bool flag = !renamedOk;
                string newName;
                if (flag)
                {
                    string str = await this.PromptNewNameAsync(file.Name);
                    flag = !string.IsNullOrWhiteSpace(newName = str);
                }
                if (flag)
                {
                    try
                    {
                        await file.RenameAsync(newName);
                        renamedOk = true;
                    }
                    catch (Exception ex)
                    {
                        if (ExceptionHResult.FileExists == ex.HResult)
                        {
                            renamedOk = false;
                            await ((App)Application.Current).ShowMsgDialogAsync(
                                "File name already exists.");
                        }
                        else if (ExceptionHResult.InvalidFileName == ex.HResult)
                        {
                            renamedOk = false;
                            await ((App)Application.Current).ShowMsgDialogAsync(
                                "Invalid File Name.");
                        }
                        else
                            break;
                    }
                }
                else
                    goto label_18;
            }
            Exception e;
            throw e;
        label_18:;
        }

        private async Task<string> PromptNewNameAsync(string origName)
        {
            ExtPromptDialog pd = new ExtPromptDialog(origName, "Rename", "New Name");
            return 1 != await pd.ShowAsync() ? (string)null : pd.SelectedExt;
        }

        private async Task OpenWithExt(string path)
        {
            string ext = await this.PromptExtensionAsync(System.IO.Path.GetExtension(path));
            if (string.IsNullOrEmpty(ext))
                return;
            await this.DoOpenWithExtAsync(ext, path);
        }

        private async Task<string> PromptExtensionAsync(string origExt)
        {
            ExtPromptDialog d = new ExtPromptDialog(origExt, "Open", "Open with ext");
            return 1 != await d.ShowAsync() || string.IsNullOrWhiteSpace(d.SelectedExt)
                ? (string)null : d.SelectedExt;
        }

        private async Task DoOpenWithExtAsync(string ext, string path)
        {
            int num1 = 0;
            string toName1;
            try
            {
                string path1 = System.IO.Path.ChangeExtension(path, ext);
                toName1 = PathValidator.IsFile(path1) ? System.IO.Path.GetFileName(path1) 
                    : throw new ArgumentException();
            }
            catch (ArgumentException ex)
            {
                num1 = 1;
            }
            if (num1 == 1)
            {
                await this.ShowMsgDialogAsync("invalid extension " + ext);
            }
            else
            {
                string toName2 = toName1;
                StorageFile fileFromPathAsync = await StorageFile.GetFileFromPathAsync(path);
                StorageFile tempAsync = await TmpContentFile.CopyToTempAsync(toName2, 
                    fileFromPathAsync);
                toName2 = (string)null;
                int num2 = await Launcher.LaunchFileAsync((IStorageFile)tempAsync) ? 1 : 0;
            }
        }

        private void appBarView_Click(object sender, RoutedEventArgs e)
        {
            ContentFile contentFile = (ContentFile)null;
            if (((Selector)this.listView).SelectedItem != null)
            {
                ContentFile file = ((ContentFileModel)((Selector)this.listView).SelectedItem).File;
                if (Regex.IsMatch(file.Path, "(.*?)\\.(jpg|jpeg|png|bmp)$"))
                    contentFile = file;
            }
            if (contentFile != null)
                this.Frame.Navigate(typeof(FlipViewerPage), (object)contentFile);
            else
                this.Frame.Navigate(typeof(FlipViewerPage));
        }

        private void appBarMulSel_Click(object sender, RoutedEventArgs e)
        {
            if (((ListViewBase)this.listView).SelectionMode == 1)
            {
                ((ListViewBase)this.listView).put_IsItemClickEnabled(false);
                ((ListViewBase)this.listView).put_SelectionMode((ListViewSelectionMode)2);
                ((Selector)this.listView).put_SelectedIndex(-1);
            }
            else
            {
                ((ListViewBase)this.listView).put_IsItemClickEnabled(true);
                ((ListViewBase)this.listView).put_SelectionMode((ListViewSelectionMode)1);
                ((Selector)this.listView).put_SelectedIndex(-1);
            }
        }

        private async void appBarDelete_Click(object sender, RoutedEventArgs e)
        {
            if (((ListViewBase)this.listView).SelectedItems != null 
                && ((ListViewBase)this.listView).SelectedItems.Count > 0)
            {
                if (!await ((App)Application.Current).ShowConfirmDialogAsync(
                    "Delete " + (object)((ListViewBase)this.listView).SelectedItems.Count + " items?"))
                    return;
                IEnumerable<ContentFile> fl = 
                    ((ListViewBase)this.listView).SelectedItems.Select<object, ContentFile>(
                        (Func<object, ContentFile>)(v => ((ContentFileModel)v).File));

                await ContentManager.GetInstance().DeleteContentAsync(fl);

                foreach (ContentFileModel selectedItem in
                    (IEnumerable<object>)((ListViewBase)this.listView).SelectedItems)
                {
                    selectedItem.Visibility = ContentFileModel.VisibilityValue.Collapsed.ToString();
                }

                await ((App)Application.Current).ShowMsgDialogAsync("Deleted.");
                ((Selector)this.listView).put_SelectedIndex(-1);
                this.ReloadContentList();
            }
            else
                await ((App)Application.Current).ShowMsgDialogAsync("No files are selected");
        }

        private async Task ShowMsgDialogAsync(string msg)
        {
            IUICommand iuiCommand = await new MessageDialog(msg).ShowAsync();
        }

        private async void appBarCompress_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<StorageFile> listToCom = (IEnumerable<StorageFile>)null;
            if (2 == ((ListViewBase)this.listView).SelectionMode)
            {
                if (((ListViewBase)this.listView).SelectedItems != null
                    && ((ListViewBase)this.listView).SelectedItems.Count > 0)
                {
                    listToCom = ((ListViewBase)this.listView).SelectedItems.Select<object, StorageFile>((Func<object, StorageFile>)(v => ((ContentFileModel)v).File.File));
                }
                else
                {
                    await ((App)Application.Current).ShowMsgDialogAsync("No files are selected.");
                }
            }
            else if (await ((App)Application.Current).ShowConfirmDialogAsync("Zip all files in this app?"))
                listToCom = ContentManager.GetInstance().ContentLists.AllItems().Select<ContentFile, StorageFile>((Func<ContentFile, StorageFile>)(v => v.File));
            
            if (listToCom == null)
                return;
            this.StartProgressRing("Zipping...");
            this.SetButtonsStates(false);
            ContentFile destFile = await ContentFile.CreateAsync("compressed.zip");
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            string str = await CompressionEngine.ZipEngine().Compress(
                listToCom.ToArray<StorageFile>(), destFile.File, 
                (Action<int, int>)((compressed, total) 
                => ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, 
                new DispatchedHandler((object)
                new ContentViewerPage.\u003C\u003Ec__DisplayClass34_0()
            {
                compressed = compressed,
                total = total
            }, __methodptr(\u003CappBarCompress_Click\u003Eb__3)))));

            this.StopProgressRing();
            this.SetButtonsStates(true);
            await ((App)Application.Current).ShowMsgDialogAsync(
                "Generated file '" + destFile.Name + "'");
            this.ReloadContentList();
            destFile = (ContentFile)null;
        }

        private void OnDataRequested(DataTransferManager sender, DataRequestedEventArgs e)
        {
            if (this.GetShareContent(e.Request))
            {
                if (!string.IsNullOrEmpty(e.Request.Data.Properties.Title))
                    return;
                e.Request.FailWithDisplayText("Missing items");
            }
            else
                e.Request.FailWithDisplayText("Please select at least one item to share.");
        }

        private bool GetShareContent(DataRequest request)
        {
            bool shareContent = false;
            IEnumerable<StorageFile> storageFiles = (IEnumerable<StorageFile>)null;
            if (2 == ((ListViewBase)this.listView).SelectionMode && ((ListViewBase)this.listView).SelectedItems != null && ((ListViewBase)this.listView).SelectedItems.Count > 0)
                storageFiles = ((ListViewBase)this.listView).SelectedItems.Select<object, StorageFile>((Func<object, StorageFile>)(v => ((ContentFileModel)v).File.File));
            if (storageFiles != null)
            {
                DataPackage data = request.Data;
                data.Properties.put_Title("Share Storage Item");
                data.SetStorageItems((IEnumerable<IStorageItem>)storageFiles);
                shareContent = true;
            }
            return shareContent;
        }

        private async Task ShowShareUIAsync()
        {
            if (2 == ((ListViewBase)this.listView).SelectionMode
                && ((ListViewBase)this.listView).SelectedItems != null
                && ((ListViewBase)this.listView).SelectedItems.Count > 0)
            {
                DataTransferManager.ShowShareUI();
            }
            else
            {
                await ((App)Application.Current).ShowMsgDialogAsync("No files are selected.");
            }
        }

        private async void appBarShare_Click(object sender, RoutedEventArgs e)
            => await this.ShowShareUIAsync();

        private void StartProgressRing(string text)
        {
            StatusBarProgressIndicator progressIndicator 
                = StatusBar.GetForCurrentView().ProgressIndicator;
            progressIndicator.put_Text(text);
            progressIndicator.put_ProgressValue(new double?());
            progressIndicator.ShowAsync();
        }

        private void StopProgressRing()
        {
            StatusBarProgressIndicator progressIndicator 
                = StatusBar.GetForCurrentView().ProgressIndicator;
            progressIndicator.put_Text("");
            progressIndicator.HideAsync();
        }

        private void SetButtonsStates(bool enabled)
        {
            ((Control)this.appBarCompress).put_IsEnabled(enabled);
            ((Control)this.appBarDelete).put_IsEnabled(enabled);
            ((Control)this.appBarReload).put_IsEnabled(enabled);
            ((Control)this.appBarShare).put_IsEnabled(enabled);
            ((Control)this.appBarView).put_IsEnabled(enabled);
        }      
    }
}//namespace end

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
    public sealed partial class ContentViewerPage : Page
    {
        public ContentViewerPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
