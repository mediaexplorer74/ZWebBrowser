// Decompiled with JetBrains decompiler
// Type: ZWebBrowser.ScriptDialog
// Assembly: ZWebBrowser, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CDB9F70E-D5DC-4B71-A590-D5917E333AFC
// Assembly location: C:\Users\Admin\Desktop\RE\ZWebBrowser1\ZWebBrowser.exe

using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Markup;

namespace ZWebBrowser
{
    public sealed partial class ScriptDialog : ContentDialog
    {
        private string _script = "";
       
        public ScriptDialog()
        {
            this.InitializeComponent();
            this.RecentScriptsRequested = false;
            this.EmbeddedScriptsRequested = false;
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ContentDialog,
                ContentDialogOpenedEventArgs>>(new Func<TypedEventHandler<ContentDialog, 
                ContentDialogOpenedEventArgs>, EventRegistrationToken>(((ContentDialog)this)
                .add_Opened), new Action<EventRegistrationToken>(((ContentDialog)this)
                .remove_Opened), new TypedEventHandler<ContentDialog,
                ContentDialogOpenedEventArgs>((object)this, __methodptr(ScriptDialog_Opened)));
            // ISSUE: method pointer
            WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<ContentDialog,
                ContentDialogClosedEventArgs>>(new Func<TypedEventHandler<ContentDialog, 
                ContentDialogClosedEventArgs>, EventRegistrationToken>(((ContentDialog)this)
                .add_Closed), new Action<EventRegistrationToken>(((ContentDialog)this)
                .remove_Closed), new TypedEventHandler<ContentDialog,
                ContentDialogClosedEventArgs>((object)this, __methodptr(ScriptDialog_Closed)));
        }

        private void FixTextboxFocusedbehindKeyboard(bool set)
        {
            if (set)
            {
                InputPane forCurrentView1 = InputPane.GetForCurrentView();
                // ISSUE: method pointer
                WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane,
                    InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView1.add_Showing), new Action<EventRegistrationToken>(forCurrentView1.remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object)this, __methodptr(ScriptDialog_Showing)));
                InputPane forCurrentView2 = InputPane.GetForCurrentView();
                // ISSUE: method pointer
                WindowsRuntimeMarshal.AddEventHandler<TypedEventHandler<InputPane, 
                    InputPaneVisibilityEventArgs>>(new Func<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>, EventRegistrationToken>(forCurrentView2.add_Hiding), new Action<EventRegistrationToken>(forCurrentView2.remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object)this, __methodptr(ScriptDialog_Hiding)));
            }
            else
            {
                // ISSUE: method pointer
                WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, 
                    InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Showing), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object)this, __methodptr(ScriptDialog_Showing)));
                // ISSUE: method pointer
                WindowsRuntimeMarshal.RemoveEventHandler<TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>>(new Action<EventRegistrationToken>(InputPane.GetForCurrentView().remove_Hiding), new TypedEventHandler<InputPane, InputPaneVisibilityEventArgs>((object)this, __methodptr(ScriptDialog_Hiding)));
            }
        }

        private void ScriptDialog_Showing(InputPane sender, InputPaneVisibilityEventArgs args)
            => this.OnInputPaneShow(sender, args);

        private void ScriptDialog_Hiding(InputPane sender, InputPaneVisibilityEventArgs args)
            => this.OnInputPaneHide(sender, args);

        private void OnInputPaneShow(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            ZWebBrowser.Diag.Debug.WriteLine(nameof(OnInputPaneShow));
            if (!(FocusManager.GetFocusedElement() is UIElement focusedElement))
                return;
            Point point = focusedElement.TransformToVisual((UIElement)this).TransformPoint(new Point(0.0, 0.0));
            if (args.OccludedRect.Top > 16.0)
            {
                CompositeTransform compositeTransform = new CompositeTransform();
                compositeTransform.TranslateY = (-point.Y);
                ((FrameworkElement)this.scriptBox).Height = args.OccludedRect.Top - 6.0;
                ((FrameworkElement)this.scriptBox).VerticalAlignment = (VerticalAlignment)0;
                ((UIElement)this).RenderTransform = (Transform)compositeTransform;
            }
            args.EnsuredFocusedElementInView = true;
        }

        private void OnInputPaneHide(InputPane sender, InputPaneVisibilityEventArgs args)
        {
            ZWebBrowser.Diag.Debug.WriteLine(nameof(OnInputPaneHide));
            TranslateTransform translateTransform = new TranslateTransform();
            translateTransform.Y = 0.0;
            ((UIElement)this).RenderTransform = (Transform)translateTransform;
            ((FrameworkElement)this.scriptBox).Height = double.NaN;
            ((FrameworkElement)this.scriptBox).VerticalAlignment = (VerticalAlignment)3;
            args.EnsuredFocusedElementInView = false;
        }

        public ScriptDialog(string script)
          : this()
        {
            this._script = script;
        }

        private void ScriptDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args)
        {
            this.FixTextboxFocusedbehindKeyboard(true);
            this.scriptBox.Text = this._script == null ? "" : this._script;
        }

        private void ScriptDialog_Closed(ContentDialog sender, ContentDialogClosedEventArgs args) 
            => this.FixTextboxFocusedbehindKeyboard(false);

        public string ScriptText => this._script;

        public bool RecentScriptsRequested { get; set; }

        public bool EmbeddedScriptsRequested { get; set; }

        private void ContentDialog_PrimaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
            this._script = this.scriptBox.Text;
        }

        private void ContentDialog_SecondaryButtonClick(
          ContentDialog sender,
          ContentDialogButtonClickEventArgs args)
        {
        }

        private void recentBn_Click(object sender, RoutedEventArgs e)
        {
            this.RecentScriptsRequested = true;
            this.Hide();
        }

        private void embeddedBn_Click(object sender, RoutedEventArgs e)
        {
            this.EmbeddedScriptsRequested = true;
            this.Hide();
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

// Документацию по шаблону элемента "Диалоговое окно содержимого" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace ZWebBrowser
{
    public sealed partial class ScriptDialog : ContentDialog
    {
        public ScriptDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
*/
