using System;
using System.Windows.Forms;
using Plugin.Browser.Properties;
using SAL.Flatbed;
using SAL.Windows;
using System.Diagnostics;

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

		protected override void OnCreateControl()
		{
			this.Window.Caption = "Browser";
			this.Window.SetTabPicture(Resources.iconBrowser);
			this.Window.Shown += new EventHandler(this.Parent_Shown);
			base.OnCreateControl();

			this.NavigateUrl = this.Settings.NavigateUrl;
			this.bnNavigate_Click(this, EventArgs.Empty);
		}

		private void Parent_Shown(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
				this.bnNavigate_Click(this, e);
		}

		private void bnNavigate_Click(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
				browser.Navigate(this.NavigateUrl);
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

		private void browser_Navigating(Object sender, WebBrowserNavigatingEventArgs e)
		{
			base.Cursor = Cursors.WaitCursor;
			this.Plugin.Trace.TraceEvent(TraceEventType.Start, 1, "Loading...");
		}

		private void browser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			base.Cursor = Cursors.Arrow;
			this.NavigateUrl = browser.Document.Url.ToString();

			this.Window.Caption = browser.Document.Title;
			this.Plugin.Trace.TraceEvent(TraceEventType.Stop, 1, null);
		}

		private void browser_CanGoForwardChanged(Object sender, EventArgs e)
			=> bnFfwd.Enabled = browser.CanGoBack;

		private void browser_CanGoBackChanged(Object sender, EventArgs e)
			=> bnBack.Enabled = browser.CanGoForward;

		private void bnBack_Click(Object sender, EventArgs e)
			=> browser.GoBack();

		private void bnFfwd_Click(Object sender, EventArgs e)
			=> browser.GoForward();
	}
}