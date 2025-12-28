using System;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Plugin.Browser.Properties;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.Browser
{
	internal partial class DocumentBrowser : UserControl, IPluginSettings<DocumentBrowserSettings>
	{
		private DocumentBrowserSettings _settings;

		public String NavigateUrl
		{
			get => txtNavigate.Text;
			set
			{
				if(!String.IsNullOrEmpty(value) && Uri.IsWellFormedUriString(value, UriKind.Absolute))
				{
					txtNavigate.Text = value;
					this.Settings.NavigateUrl = value;
				}
			}
		}

		Object IPluginSettings.Settings => this.Settings;

		public DocumentBrowserSettings Settings => this._settings ?? (this._settings = new DocumentBrowserSettings());

		protected PluginWindows Plugin => (PluginWindows)this.Window.Plugin;
		private IWindow Window => (IWindow)base.Parent;

		public DocumentBrowser()
			=> this.InitializeComponent();

		protected override async void OnCreateControl()
		{
			this.Window.Caption = "Browser";
			this.Window.SetTabPicture(Resources.iconBrowser);
			this.Window.Shown += new EventHandler(this.Parent_Shown);
			base.OnCreateControl();

			this.NavigateUrl = this.Settings.NavigateUrl;
			await webView.EnsureCoreWebView2Async(null);
			this.bnNavigate_Click(this, EventArgs.Empty);
		}

		private void Parent_Shown(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
				this.bnNavigate_Click(this, e);
		}

		private void bnNavigate_Click(Object sender, EventArgs e)
		{
			if (webView != null && webView.CoreWebView2 != null && !String.IsNullOrEmpty(this.NavigateUrl))
				webView.CoreWebView2.Navigate(this.NavigateUrl);
		}

		private void txtNavigate_KeyDown(Object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
			case Keys.Enter:
				this.bnNavigate_Click(sender, e);
				e.Handled = true;
				break;
			}
		}

		private void txtNavigate_TextChanged(Object sender, EventArgs e)
			=> bnNavigate.Enabled = !String.IsNullOrEmpty(txtNavigate.Text);

		private void WebView_NavigationStarting(Object sender, CoreWebView2NavigationStartingEventArgs e)
		{
			base.Cursor = Cursors.WaitCursor;
			this.Plugin.Trace.TraceEvent(TraceEventType.Start, 1, "Loading...");
		}

		private void WebView_NavigationCompleted(Object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			base.Cursor = Cursors.Arrow;
			this.NavigateUrl = webView.CoreWebView2.Source;

			this.Window.Caption = webView.CoreWebView2.DocumentTitle;
			this.Plugin.Trace.TraceEvent(TraceEventType.Stop, 1, null);
		}

		private void WebView_CoreWebView2InitializationCompleted(Object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			if (webView.CoreWebView2 != null)
			{
				webView.CoreWebView2.HistoryChanged += CoreWebView2_HistoryChanged;
			}
		}

		private void CoreWebView2_HistoryChanged(Object sender, Object e)
		{
			bnBack.Enabled = webView.CanGoBack;
			bnFfwd.Enabled = webView.CanGoForward;
		}

		private void bnBack_Click(Object sender, EventArgs e)
			=> webView.GoBack();

		private void bnFfwd_Click(Object sender, EventArgs e)
			=> webView.GoForward();
	}
}