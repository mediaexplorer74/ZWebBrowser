// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.MainPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Phone.UI.Input;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using Windows.Web;
using ZHttpStockLib.Contents;
using ZHttpStockLib.Scripts;
using ZHttpStockLib.Security;
using ZHttpStockLib.UI;
using ZWebBrowser.Common;
using ZWebBrowser.Models;
using ZWebBrowser.Util;

namespace ZWebBrowser
{
    public sealed partial class MainPage : Page
    {
        private NavigationHelper navigationHelper;
        private ProgressBarController _progressBarController;
        private AnimationParaModel _animationParaContext;

        private static readonly Uri HomeUri = new Uri("ms-appx-web:///Html/index.html", UriKind.Absolute);
        
        /*
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private Storyboard instantMsgStoryBoard;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private MenuFlyoutItem mfiExecuteScript;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private MenuFlyoutItem mfiScriptOutput;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private ProgressBar progressBar;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private WebView WebViewControl;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private ProgressRing progressRing;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private Border instantMsgBorder;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private CompositeTransform instantMsgTransform;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private TextBlock instantMsgBlock;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private CommandBar appCommandBar;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonStop;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonBack;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonForward;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonGo;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonSavePage;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarButtonCommands;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarContentViewer;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarScript;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarSettings;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarHelp;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private AppBarButton appBarExit;
        [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        private bool _contentLoaded;
        */

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = (NavigationCacheMode)1;

            //this.navigationHelper = new NavigationHelper((Page)this);

            //RnD
            //this.navigationHelper.LoadState += new LoadStateEventHandler(this.NavigationHelper_LoadState);
            //this.navigationHelper.SaveState += new SaveStateEventHandler(this.NavigationHelper_SaveState);
            
            //WindowsRuntimeMarshal.AddEventHandler<SizeChangedEventHandler>(
            //    new Func<SizeChangedEventHandler, EventRegistrationToken>(
            //        ((FrameworkElement)this).add_SizeChanged), 
            //    new Action<EventRegistrationToken>(((FrameworkElement)this).remove_SizeChanged),
            //    new SizeChangedEventHandler(this.MainPage_SizeChanged));

            WebView webViewControl1 = this.WebViewControl;
            WindowsRuntimeMarshal.AddEventHandler<NotifyEventHandler>(
                new Func<NotifyEventHandler, EventRegistrationToken>(
                    webViewControl1.add_ScriptNotify), new Action<EventRegistrationToken>(
                        webViewControl1.remove_ScriptNotify),
                new NotifyEventHandler(
                            this.WebViewControl_ScriptNotify));

            WebView webViewControl2 = this.WebViewControl;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, 
                WebViewNavigationStartingEventArgs>>(
                new Func<TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>,
                EventRegistrationToken>(webViewControl2.add_NavigationStarting), 
                new Action<EventRegistrationToken>(webViewControl2.remove_NavigationStarting), new TypedEventHandler<WebView, WebViewNavigationStartingEventArgs>((object)this, __methodptr(WebViewControl_NavigationStarting)));
            WebView webViewControl3 = this.WebViewControl;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, 
                WebViewNavigationCompletedEventArgs>>(new Func<TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>, EventRegistrationToken>(webViewControl3.add_NavigationCompleted), new Action<EventRegistrationToken>(webViewControl3.remove_NavigationCompleted), new TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>((object)this, __methodptr(WebViewControl_NavigationCompleted)));
            WebView webViewControl4 = this.WebViewControl;
            WindowsRuntimeMarshal.AddEventHandler<WebViewNavigationFailedEventHandler>(
                new Func<WebViewNavigationFailedEventHandler, EventRegistrationToken>(
                    webViewControl4.add_NavigationFailed), new Action<EventRegistrationToken>(webViewControl4.remove_NavigationFailed), new WebViewNavigationFailedEventHandler(this.WebViewControl_NavigationFailed));
            WebView webViewControl5 = this.WebViewControl;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, WebViewContentLoadingEventArgs>>(new Func<TypedEventHandler<WebView, WebViewContentLoadingEventArgs>, EventRegistrationToken>(webViewControl5.add_ContentLoading), new Action<EventRegistrationToken>(webViewControl5.remove_ContentLoading), new TypedEventHandler<WebView, WebViewContentLoadingEventArgs>((object)this, __methodptr(WebViewControl_ContentLoading)));
            WebView webViewControl6 = this.WebViewControl;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs>>(new Func<TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs>, EventRegistrationToken>(webViewControl6.add_DOMContentLoaded), new Action<EventRegistrationToken>(webViewControl6.remove_DOMContentLoaded), new TypedEventHandler<WebView, WebViewDOMContentLoadedEventArgs>((object)this, __methodptr(WebViewControl_DOMContentLoaded)));
            ((FrameworkElement)this).put_DataContext((object)this.AnimationParaContext);
        }

        private ProgressBarController ProgressValueController
        {
            get
            {
                if (this._progressBarController == null)
                {
                    this._progressBarController = new ProgressBarController(100);
                    this._progressBarController.ProgressChanged += new ProgressBarController.ProgressChangedHandler(this.ProgressValueChanged);
                }
                return this._progressBarController;
            }
        }

        public AnimationParaModel AnimationParaContext
        {
            get
            {
                if (this._animationParaContext == null)
                    this._animationParaContext = new AnimationParaModel();
                return this._animationParaContext;
            }
        }

        private void MainPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.SetAnimationParasForPageSize(e.NewSize);
        }

        private void SetAnimationParasForPageSize(Size pageSize)
        {
            (((UIElement)this.instantMsgBorder).RenderTransform as CompositeTransform).put_TranslateY(0.0);
            Point point = ((UIElement)this.instantMsgBorder).TransformToVisual((UIElement)this).TransformPoint(new Point(0.0, 0.0));
            double num1 = -(point.Y + ((FrameworkElement)this.instantMsgBorder).ActualHeight);
            double num2 = -point.Y;
            this.AnimationParaContext.InsMsgFromY = num1;
            this.AnimationParaContext.InsMsgToY = num2;
        }

        private void WebViewControl_NavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
        {
            this.ProgressValueController.ForceValue(100);
            this.ProgressValueController.Stop();
            this.HideProgress();
            ZWebBrowser.Diag.Debug.WriteLine("NavigationFailed:");
        }

        private void WebViewControl_NavigationCompleted(
          WebView sender,
          WebViewNavigationCompletedEventArgs args)
        {
            this.ProgressValueController.ForceValue(100);
            this.ProgressValueController.Stop();
            this.HideProgress();
            ZWebBrowser.Diag.Debug.WriteLine("NavigationCompleted:");
        }

        private void WebViewControl_NavigationStarting(
          WebView sender,
          WebViewNavigationStartingEventArgs args)
        {
            this.ShowProgress();
            this.ProgressValueController.NextValueInPhase(60);
            this.ProgressValueController.ForceValue(20);
            ZWebBrowser.Diag.Debug.WriteLine("NavigationStarting" + ((Uri)null != args.Uri ? args.Uri.OriginalString : ""));
        }

        private void WebViewControl_DOMContentLoaded(
          WebView sender,
          WebViewDOMContentLoadedEventArgs args)
        {
            ZWebBrowser.Diag.Debug.WriteLine("DOMContentLoaded");
            this.ProgressValueController.NextValueInPhase(100);
            this.ProgressValueController.ForceValue(95);
        }

        private void WebViewControl_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            ZWebBrowser.Diag.Debug.WriteLine("ContentLoading");
            this.ProgressValueController.NextValueInPhase(100);
            this.ProgressValueController.ForceValue(60);
        }

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            if (!this.ValidateAccess())
                return;
            string str = e.PageState == null || !e.PageState.ContainsKey("lasturl") ? (string)e.NavigationParameter : e.PageState["lasturl"] as string;
            if (string.IsNullOrEmpty(str))
                return;
            if (new Uri(str).IsFile)
                this.WebViewControl.NavigateToLocalStreamUri(this.WebViewControl.BuildLocalStreamUri("localwebview", Path.GetFileName(new Uri(str).AbsoluteUri)), (IUriToStreamResolver)new StreamUriWinRTResolver(new Uri(Path.GetDirectoryName(str)).AbsoluteUri));
            else
                this.WebViewControl.Navigate(new Uri(str));
        }

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
            if (this.WebViewControl.Source != (Uri)null && !string.IsNullOrWhiteSpace(this.WebViewControl.Source.OriginalString))
                e.PageState["lasturl"] = (object)this.WebViewControl.Source.OriginalString;
            this.WebViewControl.NavigateToString("<!DOCTYPE html><html lang='zh-CN'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" + "<style> body {background-color: #000000;  } </style>" + "</head><body>" + "</body></html>");
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            WindowsRuntimeMarshal.AddEventHandler<EventHandler<BackPressedEventArgs>>(new Func<EventHandler<BackPressedEventArgs>, EventRegistrationToken>(HardwareButtons.add_BackPressed), new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.MainPage_BackPressed));
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<BackPressedEventArgs>>(new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(this.MainPage_BackPressed));
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void MainPage_BackPressed(object sender, BackPressedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                e.put_Handled(true);
            }
            else if (this.WebViewControl.CanGoBack)
            {
                this.WebViewControl.GoBack();
                e.put_Handled(true);
            }
            else
            {
                Application.Current.Exit();
                e.put_Handled(true);
            }
        }

        private async void Browser_NavigationCompleted(
          WebView sender,
          WebViewNavigationCompletedEventArgs args)
        {
            if (args.IsSuccess)
                return;
            await ((App)Application.Current).ShowMsgDialogAsync("Navigation failed, you may have to check your internet connection.");
        }

        private void ForwardAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!this.WebViewControl.CanGoForward)
                return;
            this.WebViewControl.GoForward();
        }

        private void appBarButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (!this.WebViewControl.CanGoBack)
                return;
            this.WebViewControl.GoBack();
        }

        private void appBarButtonStop_Click(object sender, RoutedEventArgs e) => this.WebViewControl.Stop();

        private void appBarHelp_Click(object sender, RoutedEventArgs e) => this.ShowHelpContent();

        private void HomeAppBarButton_Click(object sender, RoutedEventArgs e) => this.WebViewControl.Navigate(MainPage.HomeUri);

        private void appBarButtonCommands_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(Commands));

        private async Task<string> WebNavigateToLocal(string v)
        {
            ContentFile contentFile = await ContentFile.LoadAsync(v);
            IBuffer source = await ContentManager.GetInstance().LoadContentAsync(contentFile);
            return Encoding.UTF8.GetString(source.ToArray(), 0, (int)source.Length);
        }

        private async void appBarButtonGo_Click(object sender, RoutedEventArgs e)
        {
            UrlDialog ud = new UrlDialog(!((Uri)null != this.WebViewControl.Source) || string.IsNullOrEmpty(this.WebViewControl.Source.OriginalString) ? "" : this.WebViewControl.Source.OriginalString);
            if (1 != await ud.ShowAsync() || string.IsNullOrWhiteSpace(ud.UrlString))
                return;
            try
            {
                this.WebViewControl.Navigate(new Uri(ud.UrlString));
            }
            catch
            {
                await this.ShowMsgDialog("Invalid address.");
            }
        }

        private void appBarContentViewer_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(ContentViewerPage));

        private void appBarExit_Click(object sender, RoutedEventArgs e) => Application.Current.Exit();

        private async void mfiExecuteScript_Click(object sender, RoutedEventArgs e) => await this.ExecuteScriptAsync((string)null);

        private void mfiScriptOutput_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(ScriptOutputPage));

        private async Task ShowMsgDialog(string msg)
        {
            IUICommand iuiCommand = await new MessageDialog(msg).ShowAsync();
        }

        private void WebViewControl_ScriptNotify(object sender, NotifyEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Value))
                return;
            ScriptsManager.GetInstance().AddOutputText(e.Value);
        }

        private void ShowProgress() => ((UIElement)this.progressBar).put_Visibility((Visibility)0);

        private void HideProgress() => ((UIElement)this.progressBar).put_Visibility((Visibility)1);

        private void ProgressValueChanged(int v) => ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, new DispatchedHandler((object)new MainPage.\u003C\u003Ec__DisplayClass38_0()
    {
      \u003C\u003E4__this = this,
          v = v
      }, __methodptr(\u003CProgressValueChanged\u003Eb__0)));

    private async Task DoExecuteScriptAsync(string scriptText)
    {
        string scriptout = (string)null;
        try
        {
            scriptout = await this.WebViewControl.InvokeScriptAsync("eval", (IEnumerable<string>)new string[1]
            {
          scriptText
            });
            await ((App)Application.Current).ShowInstantMsgAsync("Script executed.", this.instantMsgBlock, this.instantMsgBorder, this.instantMsgStoryBoard);
        }
        catch (Exception ex)
        {
            await this.ShowMsgDialog("Failed to execute script." + ex.Message);
        }
        if (string.IsNullOrEmpty(scriptout))
            return;
        ScriptsManager.GetInstance().AddOutputText(scriptout);
    }

    private async Task ExecuteScriptAsync(string script)
    {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        MainPage.\u003C\u003Ec__DisplayClass40_0 cDisplayClass400 = new MainPage.\u003C\u003Ec__DisplayClass40_0();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass400.\u003C\u003E4__this = this;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass400.content = (string)null;
        ScriptDialog ud = new ScriptDialog(script);
        if (1 == await ud.ShowAsync())
        {
            if (ud.ScriptText == null)
                return;
            if (!string.IsNullOrWhiteSpace(ud.ScriptText))
            {
                try
                {
                    await this.SaveScriptContentAsync(ud.ScriptText);
                }
                catch (Exception ex)
                {
                }
            }
            await this.DoExecuteScriptAsync(ud.ScriptText);
        }
        else if (ud.RecentScriptsRequested)
        {
            ud.RecentScriptsRequested = false;
            string name = await this.ShowRecentScriptsPickerAsync();
            if (name != null)
            {
                ScriptsStore instanceAsync = await ScriptsStore.GetInstanceAsync();
                // ISSUE: reference to a compiler-generated field
                string content = cDisplayClass400.content;
                string name1 = name;
                string str = await instanceAsync.LoadScriptAsync(name1);
                // ISSUE: reference to a compiler-generated field
                cDisplayClass400.content = str;
                // ISSUE: method pointer
                ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)0, new DispatchedHandler((object)cDisplayClass400, __methodptr(\u003CExecuteScriptAsync\u003Eb__0)));
            }
            name = (string)null;
        }
        else
        {
            if (!ud.EmbeddedScriptsRequested)
                return;
            ud.EmbeddedScriptsRequested = false;
            string name = await this.ShowEmbeddedScriptsPickerAsync();
            if (name != null)
                await this.DoExecuteScriptAsync(await (await EmbeddedScriptsStore.GetInstanceAsync()).LoadScriptAsync(name));
            name = (string)null;
        }
    }

    private async Task SaveScriptContentAsync(string scriptText)
    {
        StorageFile storageFile = await (await ScriptsStore.GetInstanceAsync()).SaveScriptAsync(scriptText);
    }

    private async Task<string> ShowRecentScriptsPickerAsync()
    {
        string name = (string)null;
        RecentScriptsDialog rsd = new RecentScriptsDialog((ScriptsStoreBase)await ScriptsStore.GetInstanceAsync());
        ContentDialogResult contentDialogResult = await rsd.ShowAsync();
        if (rsd.SelectedName != null)
            name = rsd.SelectedName;
        return name;
    }

    private async Task<string> ShowEmbeddedScriptsPickerAsync()
    {
        string name = (string)null;
        RecentScriptsDialog rsd = new RecentScriptsDialog((ScriptsStoreBase)await EmbeddedScriptsStore.GetInstanceAsync());
        ContentDialogResult contentDialogResult = await rsd.ShowAsync();
        if (rsd.SelectedName != null)
            name = rsd.SelectedName;
        return name;
    }

    private void appBarScript_Click(object sender, RoutedEventArgs e)
    {
        MenuFlyout resource = (MenuFlyout)((IDictionary<object, object>)((FrameworkElement)this).Resources)[(object)"scriptMenuFlyout"];
        ((FlyoutBase)resource).put_Placement((FlyoutPlacementMode)1);
        ((FlyoutBase)resource).ShowAt((FrameworkElement)this.appCommandBar);
    }

    private void appBarSettings_Click(object sender, RoutedEventArgs e) => this.Frame.Navigate(typeof(ZSettingsPage));

    private bool ValidateAccess()
    {
        PasswordManager instance = PasswordManager.GetInstance(((App)Application.Current).AppSettings);
        if (!((App)Application.Current).AppSettings.PasswordEnabledSetting || instance.SessionValidated)
            return true;
        // ISSUE: method pointer
        ((DependencyObject)this).Dispatcher.RunAsync((CoreDispatcherPriority)1, new DispatchedHandler((object)this, __methodptr(\u003CValidateAccess\u003Eb__46_0)));
        return false;
    }

    private void StartProgressRing() => this.progressRing.put_IsActive(true);

    private void StopProgressRing() => this.progressRing.put_IsActive(false);

    private async Task SaveCurrentPageToFile()
    {
        if (this.WebViewControl.Source != (Uri)null
            && !string.IsNullOrWhiteSpace(this.WebViewControl.Source.OriginalString))
        {
            this.Frame.Navigate(typeof(Commands), (object)this.WebViewControl.Source.OriginalString);
        }
        else
        {
            await ((App)Application.Current).ShowMsgDialogAsync("Not available for this source.");
        }
    }

    private async void appBarButtonSavePage_Click(object sender, RoutedEventArgs e)
    {
        await this.SaveCurrentPageToFile();
    }

    private void ShowHelpContent()
    {
        this.Frame.Navigate(typeof(HelpViewPage));
    }

    /*
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
        if (this._contentLoaded)
            return;
        this._contentLoaded = true;
        Application.LoadComponent((object)this, new Uri("ms-appx:///MainPage.xaml"), (ComponentResourceLocation)0);
        this.instantMsgStoryBoard = (Storyboard)((FrameworkElement)this).FindName("instantMsgStoryBoard");
        this.mfiExecuteScript = (MenuFlyoutItem)((FrameworkElement)this).FindName("mfiExecuteScript");
        this.mfiScriptOutput = (MenuFlyoutItem)((FrameworkElement)this).FindName("mfiScriptOutput");
        this.progressBar = (ProgressBar)((FrameworkElement)this).FindName("progressBar");
        this.WebViewControl = (WebView)((FrameworkElement)this).FindName("WebViewControl");
        this.progressRing = (ProgressRing)((FrameworkElement)this).FindName("progressRing");
        this.instantMsgBorder = (Border)((FrameworkElement)this).FindName("instantMsgBorder");
        this.instantMsgTransform = (CompositeTransform)((FrameworkElement)this).FindName("instantMsgTransform");
        this.instantMsgBlock = (TextBlock)((FrameworkElement)this).FindName("instantMsgBlock");
        this.appCommandBar = (CommandBar)((FrameworkElement)this).FindName("appCommandBar");
        this.appBarButtonStop = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonStop");
        this.appBarButtonBack = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonBack");
        this.appBarButtonForward = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonForward");
        this.appBarButtonGo = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonGo");
        this.appBarButtonSavePage = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonSavePage");
        this.appBarButtonCommands = (AppBarButton)((FrameworkElement)this).FindName("appBarButtonCommands");
        this.appBarContentViewer = (AppBarButton)((FrameworkElement)this).FindName("appBarContentViewer");
        this.appBarScript = (AppBarButton)((FrameworkElement)this).FindName("appBarScript");
        this.appBarSettings = (AppBarButton)((FrameworkElement)this).FindName("appBarSettings");
        this.appBarHelp = (AppBarButton)((FrameworkElement)this).FindName("appBarHelp");
        this.appBarExit = (AppBarButton)((FrameworkElement)this).FindName("appBarExit");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target)
    {
        switch (connectionId)
        {
            case 1:
                MenuFlyoutItem menuFlyoutItem1 = (MenuFlyoutItem)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(menuFlyoutItem1.add_Click), new Action<EventRegistrationToken>(menuFlyoutItem1.remove_Click), new RoutedEventHandler(this.mfiExecuteScript_Click));
                break;
            case 2:
                MenuFlyoutItem menuFlyoutItem2 = (MenuFlyoutItem)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(menuFlyoutItem2.add_Click), new Action<EventRegistrationToken>(menuFlyoutItem2.remove_Click), new RoutedEventHandler(this.mfiScriptOutput_Click));
                break;
            case 3:
                WebView webView = (WebView)target;
                // ISSUE: method pointer
                WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>>(new Func<TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>, EventRegistrationToken>(webView.add_NavigationCompleted), new Action<EventRegistrationToken>(webView.remove_NavigationCompleted), new TypedEventHandler<WebView, WebViewNavigationCompletedEventArgs>((object)this, __methodptr(Browser_NavigationCompleted)));
                break;
            case 4:
                ButtonBase buttonBase1 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase1.add_Click), new Action<EventRegistrationToken>(buttonBase1.remove_Click), new RoutedEventHandler(this.appBarButtonStop_Click));
                break;
            case 5:
                ButtonBase buttonBase2 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase2.add_Click), new Action<EventRegistrationToken>(buttonBase2.remove_Click), new RoutedEventHandler(this.appBarButtonBack_Click));
                break;
            case 6:
                ButtonBase buttonBase3 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase3.add_Click), new Action<EventRegistrationToken>(buttonBase3.remove_Click), new RoutedEventHandler(this.ForwardAppBarButton_Click));
                break;
            case 7:
                ButtonBase buttonBase4 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase4.add_Click), new Action<EventRegistrationToken>(buttonBase4.remove_Click), new RoutedEventHandler(this.appBarButtonGo_Click));
                break;
            case 8:
                ButtonBase buttonBase5 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase5.add_Click), new Action<EventRegistrationToken>(buttonBase5.remove_Click), new RoutedEventHandler(this.appBarButtonSavePage_Click));
                break;
            case 9:
                ButtonBase buttonBase6 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase6.add_Click), new Action<EventRegistrationToken>(buttonBase6.remove_Click), new RoutedEventHandler(this.appBarButtonCommands_Click));
                break;
            case 10:
                ButtonBase buttonBase7 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase7.add_Click), new Action<EventRegistrationToken>(buttonBase7.remove_Click), new RoutedEventHandler(this.appBarContentViewer_Click));
                break;
            case 11:
                ButtonBase buttonBase8 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase8.add_Click), new Action<EventRegistrationToken>(buttonBase8.remove_Click), new RoutedEventHandler(this.appBarScript_Click));
                break;
            case 12:
                ButtonBase buttonBase9 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase9.add_Click), new Action<EventRegistrationToken>(buttonBase9.remove_Click), new RoutedEventHandler(this.appBarSettings_Click));
                break;
            case 13:
                ButtonBase buttonBase10 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase10.add_Click), new Action<EventRegistrationToken>(buttonBase10.remove_Click), new RoutedEventHandler(this.appBarHelp_Click));
                break;
            case 14:
                ButtonBase buttonBase11 = (ButtonBase)target;
                WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(buttonBase11.add_Click), new Action<EventRegistrationToken>(buttonBase11.remove_Click), new RoutedEventHandler(this.appBarExit_Click));
                break;
        }
        this._contentLoaded = true;
    }
    
  }*/

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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace ZWebBrowser
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
