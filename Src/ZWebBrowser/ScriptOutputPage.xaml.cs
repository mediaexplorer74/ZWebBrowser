// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.ScriptOutputPage
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
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
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Xaml.Markup;
using ZHttpStockLib.Contents;
using ZHttpStockLib.Path;
using ZHttpStockLib.Scripts;
using ZWebBrowser.Common;

namespace ZWebBrowser
{
    public sealed partial class ScriptOutputPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
      

        public ScriptOutputPage()
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

        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e) 
            => this.LoadScriptOutput();

        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        protected virtual void OnNavigatedTo(NavigationEventArgs e) 
            => this.navigationHelper.OnNavigatedTo(e);

        protected virtual void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBox.Text))
                this.textBox.put_Text("");
            this.navigationHelper.OnNavigatedFrom(e);
        }

        private void LoadScriptOutput()
        {
            string str = ScriptsManager.GetInstance().GetOutputTextAll();
            DispatchedHandler dispatchedHandler;
            // ISSUE: method pointer
            Task.Delay(TimeSpan.FromMilliseconds(100.0)).ContinueWith(
                (Action<Task>)(v => ((DependencyObject)this).Dispatcher.RunAsync(
                    (CoreDispatcherPriority)0, dispatchedHandler 
                    ?? (dispatchedHandler = new DispatchedHandler(
                        (object)this, __methodptr(\u003CLoadScriptOutput\u003Eb__1))))));
        }

        private void appBarToCmd_Click(object sender, RoutedEventArgs e) 
            => this.Frame.Navigate(typeof(Commands), (object)this.textBox.Text);

        private void appBarClear_Click(object sender, RoutedEventArgs e)
        {
            ScriptsManager.GetInstance().ClearOutputs();
            this.textBox.put_Text("");
        }

        private async void appBarToSave_Click(object sender, RoutedEventArgs e)
            => await this.SaveOutputToFile();

        private async Task SaveOutputToFile()
        {
            bool nameCanBeUsed = false;
            string newName = "outputs.txt";
            ContentFile fileToSave = (ContentFile)null;
            while (true)
            {
                bool flag = !nameCanBeUsed;
                if (flag)
                {
                    string str = await this.PromptNewNameAsync(newName);
                    flag = !string.IsNullOrWhiteSpace(newName = str);
                }
                if (flag)
                {
                    try
                    {
                        fileToSave = await ContentFile.CreateAsync(
                            newName, (CreationCollisionOption)2);
                        nameCanBeUsed = true;
                    }
                    catch (Exception ex)
                    {
                        if (ExceptionHResult.FileExists == ex.HResult)
                        {
                            nameCanBeUsed = false;
                            await ((App)Application.Current)
                                .ShowMsgDialogAsync("File name already exists.");
                        }
                        else if (ExceptionHResult.InvalidFileName == ex.HResult)
                        {
                            nameCanBeUsed = false;
                            await ((App)Application.Current)
                                .ShowMsgDialogAsync("Invalid File Name.");
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
        label_18:
            if (string.IsNullOrWhiteSpace(newName))
                return;
            int num = (int)await fileToSave.WriteFile(Encoding.UTF8.GetBytes(
                ScriptsManager.GetInstance().GetOutputTextAll()));
            await ((App)Application.Current).ShowMsgDialogAsync("Saved to " + newName);
        }

        private async Task<string> PromptNewNameAsync(string origName)
        {
            ExtPromptDialog pd = new ExtPromptDialog(origName, "Save File", "File Name");
            return 1 != await pd.ShowAsync() ? (string)null : pd.SelectedExt;
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
    public sealed partial class ScriptOutputPage : Page
    {
        public ScriptOutputPage()
        {
            this.InitializeComponent();
        }
    }
}
*/
