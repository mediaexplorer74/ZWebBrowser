// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.App
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Globalization;
using Windows.UI.Popups;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media.Animation;
using ZHttpStockLib.Commands;
using UnhandledExceptionEventArgs = System.UnhandledExceptionEventArgs;

/*
namespace ZWebBrowser
{
    sealed partial class App : Application
    {
        private TransitionCollection transitions;
        private CommandManager _commandManager = CommandManager.CreateInstance();
        private ZHttpStockLib.Setting.Setting _appSetting;
        //[GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
        //private bool _contentLoaded;
        //private XamlTypeInfoProvider _provider;

        public App()
        {
            this.InitializeComponent();
           
            //RnD
            //WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(
            //    new Func<SuspendingEventHandler, EventRegistrationToken>(
            //     ((Application)this).add_Suspending),
            //     new Action<EventRegistrationToken>(((Application)this).remove_Suspending),
            //     new SuspendingEventHandler(this.OnSuspending));
            
            //WindowsRuntimeMarshal.AddEventHandler<UnhandledExceptionEventHandler>(
            //    new Func<UnhandledExceptionEventHandler, EventRegistrationToken>(
            //        ((Application)this).add_UnhandledException), 
            //    new Action<EventRegistrationToken>(((Application)this)
            //    .remove_UnhandledException), new UnhandledExceptionEventHandler(
            //        this.App_UnhandledException));
        }

        private async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            IUICommand iuiCommand = await new MessageDialog(
                "This app is going to be down due to the following exception:" 
                + e.Message).ShowAsync();
        }

        public CommandManager CommandManagerInst => this._commandManager;

        public ZHttpStockLib.Setting.Setting AppSettings
        {
            get
            {
                if (this._appSetting == null)
                    this._appSetting = new ZHttpStockLib.Setting.Setting();
                return this._appSetting;
            }
        }

        protected virtual void OnLaunched(LaunchActivatedEventArgs e)
        {
            if (!(Window.Current.Content is Frame frame1))
            {
                frame1 = new Frame();
                frame1.CacheSize = 1;
                ((FrameworkElement)frame1).Language = ApplicationLanguages.Languages[0];
                ApplicationExecutionState previousExecutionState = e.PreviousExecutionState;
                Window.Current.Content = (UIElement)frame1;
            }

            if (((ContentControl)frame1).Content == null)
            {
                if (((ContentControl)frame1).ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (Transition contentTransition in (IEnumerable<Transition>)(
                        (ContentControl)frame1).ContentTransitions)
                        ((ICollection<Transition>)this.transitions).Add(contentTransition);
                }

              ((ContentControl)frame1).ContentTransitions = (TransitionCollection)null;

                Frame frame2 = frame1;

                WindowsRuntimeMarshal.AddEventHandler<NavigatedEventHandler>(
                    new Func<NavigatedEventHandler, EventRegistrationToken>(
                        frame2.add_Navigated),
                    new Action<EventRegistrationToken>(frame2.remove_Navigated),
                    new NavigatedEventHandler(this.RootFrame_FirstNavigated));

                if (!frame1.Navigate(typeof(MainPage), (object)e.Arguments))
                    throw new Exception("Failed to create initial page");
            }
            Window.Current.Activate();
        }

        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            Frame frame = sender as Frame;
            TransitionCollection transitionCollection1 = this.transitions;
            if (transitionCollection1 == null)
            {
                TransitionCollection transitionCollection2 = new TransitionCollection();
                ((ICollection<Transition>)transitionCollection2).Add(
                    (Transition)new NavigationThemeTransition());
                transitionCollection1 = transitionCollection2;
            }
          ((ContentControl)frame).ContentTransitions = transitionCollection1;
            // ISSUE: virtual method pointer
            WindowsRuntimeMarshal.RemoveEventHandler<NavigatedEventHandler>
                (new Action<EventRegistrationToken>((object)frame, 
                __vmethodptr(frame, remove_Navigated)), 
                new NavigatedEventHandler(this.RootFrame_FirstNavigated));
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            e.SuspendingOperation.GetDeferral().Complete();
        }

        protected virtual void OnFileActivated(FileActivatedEventArgs args)
        {
            Frame frame = new Frame();
            frame.Navigate(typeof(FilesReceivedPage), (object)args.Files);
            Window.Current.put_Content((UIElement)frame);
            Window.Current.Activate();
        }

        internal async Task ShowMsgDialogAsync(string msg)
        {
            IUICommand iuiCommand = await new MessageDialog(msg).ShowAsync();
        }

        internal async Task<bool> ShowConfirmDialogAsync(string msg)
        {
            MessageDialog messageDialog = new MessageDialog(msg);
            UICommand ok = new UICommand("Yes");
            UICommand uiCommand = new UICommand("No");
            messageDialog.Commands.Add((IUICommand)ok);
            messageDialog.Commands.Add((IUICommand)uiCommand);
            return await messageDialog.ShowAsync() == ok;
        }

        internal async Task ShowInstantMsgAsync(
          string msg,
          TextBlock block,
          Border border,
          Storyboard sb)
        {
            block.Text = msg;
            ((UIElement)border).Opacity = 1.0;
            sb.Begin();
            Storyboard storyboard = sb;
            WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(
                new Func<EventHandler<object>, EventRegistrationToken>(((Timeline)storyboard)
                .add_Completed), new Action<EventRegistrationToken>(((Timeline)storyboard)
                .remove_Completed), (EventHandler<object>)((sender, obj) =>
            {
                block.Text = "";
                ((UIElement)border).Opacity = (0.0);
            }));
        }       
    }
}
*/

/*
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
*/

namespace ZWebBrowser
{
   
    public sealed partial class App : Application
    {
        public CommandManager _commandManager = CommandManager.CreateInstance();
        public ZHttpStockLib.Setting.Setting _appSetting;

        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                // Создание фрейма, который станет контекстом навигации, и переход к первой странице
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: load app state
                }

                // place frame at current window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                // activate current window
                Window.Current.Activate();
            }
        }

       
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

       
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            //TODO: Store app state and stop background processes
            
            deferral.Complete();
        }
    }
}

