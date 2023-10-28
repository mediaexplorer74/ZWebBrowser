// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.LoginAuthPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Collections.Generic;
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
using Windows.Phone.UI.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Markup;
using ZHttpStockLib.Security;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class LoginAuthPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        private int failCount;
        
        //Explore it. What is this? 
        // Grid LayoutRoot
        // Grid ContentRoot
        // PasswordBox pwBox
        // TextBlock hintHdrBlock
        // TextBlock hintBlock
        

        public LoginAuthPage()
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
            => this.failCount = 0;

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedTo(e);
            WindowsRuntimeMarshal.AddEventHandler<EventHandler<BackPressedEventArgs>>
                (new Func<EventHandler<BackPressedEventArgs>, EventRegistrationToken>
                (HardwareButtons.add_BackPressed), new Action<EventRegistrationToken>
                (HardwareButtons.remove_BackPressed), new EventHandler<BackPressedEventArgs>(
                    this.HardwareButtons_BackPressed));
        }

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
            WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<BackPressedEventArgs>>(
                new Action<EventRegistrationToken>(HardwareButtons.remove_BackPressed),
                new EventHandler<BackPressedEventArgs>(this.HardwareButtons_BackPressed));
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Application.Current.Exit();
            e.put_Handled(true);
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            PasswordManager instance = PasswordManager.GetInstance(((App)Application.Current)
                .AppSettings);
            if (instance.ValidatePassword(this.pwBox.Password))
            {
                instance.SessionValidated = true;
                if (!this.Frame.CanGoBack)
                    return;
                this.Frame.GoBack();
            }
            else
            {
                ++this.failCount;
                await this.ShowMsgDialog("Invalid password.");
                if (this.failCount <= 5)
                    return;
                this.ShowPasswordHint();
            }
        }

        private async Task ShowMsgDialog(string msg)
        {
            IUICommand iuiCommand = await new MessageDialog(msg).ShowAsync();
        }

        private void ShowPasswordHint()
        {
            //RnD
            PasswordManager instance = PasswordManager.GetInstance(((App)Application.Current)
              ._appSetting); // .AppSettings);

            string passwordHint;
            this.hintBlock.Text = passwordHint = instance.GetPasswordHint();
            if (string.IsNullOrWhiteSpace(passwordHint))
                return;
            ((UIElement)this.hintHdrBlock).Visibility = (Visibility)0;
            ((UIElement)this.hintBlock).Visibility(Visibility)0;
        }

        private void HidePasswordHint()
        {
            ((UIElement)this.hintHdrBlock).Visibility = (Visibility)1;
            ((UIElement)this.hintBlock).Visibility = (Visibility)1;
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
    public sealed partial class LoginAuthPage : Page
    {
        public LoginAuthPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
