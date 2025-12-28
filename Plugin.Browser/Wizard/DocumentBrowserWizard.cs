using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;
using Plugin.Browser.Properties;
using SAL.Flatbed;
using SAL.Windows;
using System.Linq;

namespace Plugin.Browser
{
	public partial class DocumentBrowserWizard : UserControl, IPluginSettings<DocumentBrowserWizardSettings>
	{
		private IPluginDescription _callerPlugin;

		private DocumentBrowserWizardSettings _settings;

		private Boolean _cancelNavigation = false;

		private String _currentRightClickedElementPath;

		public String NavigateUrl
		{
			get => txtNavigate.Text;
			set
			{
				if(!String.IsNullOrEmpty(value))
					if(Uri.IsWellFormedUriString(value, UriKind.Absolute))
					{
						txtNavigate.Text = value;
						this.Settings.NavigateUrl = value;
					} else if(value.StartsWith("javascript:", StringComparison.OrdinalIgnoreCase))
						txtNavigate.Text = value;
			}
		}

		Object IPluginSettings.Settings => this.Settings;
		public DocumentBrowserWizardSettings Settings => this._settings ?? (this._settings = new DocumentBrowserWizardSettings());

		protected PluginWindows Plugin => (PluginWindows)this.Window.Plugin;
		protected IWindow Window => (IWindow)base.Parent;

		/// <summary>Plugin that triggers the event</summary>
		private IPluginDescription CallerPlugin
		{
			get
			{
				if(this._callerPlugin == null)
				{
					if(this.Settings.CallerPluginId != null)
						this._callerPlugin = this.Plugin.HostWindows.Plugins[this.Settings.CallerPluginId];
					else
					{
						foreach(IPluginDescription plugin in this.Plugin.HostWindows.Plugins)
							if(plugin.Instance == this.Plugin)
							{
								this._callerPlugin = plugin;
								break;
							}
					}
				}
				return this._callerPlugin;
			}
			set => this._callerPlugin = value;
		}

		/// <summary>Message that will be called upon completion of compilation</summary>
		public event EventHandler<DataEventArgs> SaveNodesEvent;

		public DocumentBrowserWizard()
		{
			this.InitializeComponent();
			splitAdvancedEdit.Panel2Collapsed = true;
			splitBrowser.Panel2Collapsed = true;
			tsNodes.Visible = false;
		}

		protected override async void OnCreateControl()
		{
			this.Window.Caption = "Browser";
			this.Window.SetTabPicture(Resources.iconBrowser);
			this.Window.Shown += new EventHandler(this.Window_Shown);
			this.Window.Closed += new EventHandler(this.Window_Closed);

			this.NavigateUrl = this.Settings.NavigateUrl;

			splitBrowser.Panel2Collapsed = false;
			tsNodes.Visible = true;

			Int32 colorIndex = 1;
			foreach(KeyValuePair<String, String> node in this.Settings.Nodes)
			{
				RadioButton rbNode = new RadioButton()
				{
					Text = node.Key,
					Tag = node.Value,
					ForeColor = Utils.Colors[colorIndex++],
					Appearance = Appearance.Button,
					Checked = colorIndex == 2,
				};
				if(node.Value != null)
					ttNodes.SetToolTip(rbNode, node.Value);

				rbNode.MouseClick += new MouseEventHandler(rbNode_MouseClick);
				flowNodes.Controls.Add(rbNode);
			}

			base.OnCreateControl();
			await webView.EnsureCoreWebView2Async();
		}

		private void Window_Shown(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
				this.bnNavigate_Click(this, e);
		}

		private void Window_Closed(Object sender, EventArgs e)
			=> this.Settings.NavigateUrl = this.NavigateUrl;

		private async void SetButtonPath(String path)
		{
			foreach(RadioButton ctrl in flowNodes.Controls)
				if(ctrl.Checked)
				{
					if(ctrl.Tag != null)
					{
						await WebView2Utils.RemoveHighlightByXPath(webView, (String)ctrl.Tag);
					}

					await WebView2Utils.HighlightElementByXPath(webView, path, ctrl.ForeColor);
					ctrl.Tag = path;
					ttNodes.SetToolTip(ctrl, path);
					break;
				}
		}

		/// <summary>Set a tooltip for an expanded control</summary>
		/// <param name="elementPath">The first element found in the collection</param>
		/// <param name="totalCount">The total number of elements found that match the search criteria</param>
		private async void SetAdvancedPath(String elementPath, Int32 totalCount)
		{
			lvAdvancedNodes.Items.Clear();
			txtAdvancedPath.AutoCompleteCustomSource.Clear();
			if(elementPath != null)
			{
				if(totalCount == 1)
				{
					List<ListViewItem> itemList = new List<ListViewItem>();
					var attributes = await WebView2Utils.GetElementAttributes(webView, elementPath);
					foreach(KeyValuePair<String, String> attribute in attributes)
						itemList.Add(new ListViewItem(new String[] { attribute.Key, attribute.Value == null ? "<null>" : attribute.Value, }));
					lvAdvancedNodes.Items.AddRange(itemList.ToArray());
					lvAdvancedNodes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

					List<String> itemAutoComplete = new List<String>();
					var childrenPaths = await WebView2Utils.GetElementChildrenPaths(webView, elementPath);
					foreach(String childPath in childrenPaths)
						itemAutoComplete.Add(childPath);
					txtAdvancedPath.AutoCompleteCustomSource.AddRange(itemAutoComplete.ToArray());
				} else
				{
					lvAdvancedNodes.Items.Add(new ListViewItem(new String[] { "Count", totalCount.ToString(), }));
					lvAdvancedNodes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
				}
			}
		}

		private void OnSaveEvent()
		{
			var evt = this.SaveNodesEvent;
			if(evt == null)
				this.Plugin.Trace.TraceEvent(TraceEventType.Warning, 10, String.Format("SaveEvent not attached from pluginId: {0}", this.Settings.CallerPluginId));
			else
			{
				KeyValuePair<String, String>[] nodes = new KeyValuePair<String, String>[flowNodes.Controls.Count];
				for(Int32 loop = 0; loop < nodes.Length; loop++)
				{
					RadioButton button = (RadioButton)flowNodes.Controls[loop];
					nodes[loop] = new KeyValuePair<String, String>(button.Text, button.Tag as String);
				}

				SelectedNodesEventArgs args = new SelectedNodesEventArgs(nodes);
				evt(this, args);
			}
		}

		void rbNode_MouseClick(Object sender, MouseEventArgs e)
		{
			RadioButton button = (RadioButton)sender;
			switch(e.Button)
			{
			case MouseButtons.Left:
				String path = (String)button.Tag;
				tsslNodePath.Text = path ?? String.Empty;
				break;
			}
		}

		private void cmsAdvancedTemplate_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			if(e.ClickedItem == tsmiAdvancedEdit)
			{
				splitAdvancedEdit.Panel2Collapsed = false;
				txtAdvancedPath.Text = _currentRightClickedElementPath;
				this.SetAdvancedPath(_currentRightClickedElementPath, 1);
			}
		}

		private void cmsAdvancedTemplate_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
		{
			webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = true;
			_currentRightClickedElementPath = null;
		}

		private async void txtAdvancedPath_TextChanged(Object sender, EventArgs e)
		{
			if(txtAdvancedPath.SelectionLength == txtAdvancedPath.Text.Length)
				return;

			var result = await WebView2Utils.GetXPathResult(webView, txtAdvancedPath.Text);

			String oldPath = txtAdvancedPath.Tag as String;
			String newPath = result.Count == 0 ? null : txtAdvancedPath.Text;

			if(oldPath != newPath)
			{
				tsbnAdvancedSave.Enabled = result.Count > 0;
				txtAdvancedPath.Tag = newPath;
				this.SetAdvancedPath(result.FirstOrDefault(), result.Count);
			}
		}

		private void tsbnAdvancedSave_Click(Object sender, EventArgs e)
			=> this.SetButtonPath(txtAdvancedPath.Text);

		private void bnNavigate_Click(Object sender, EventArgs e)
		{
			if(webView != null && webView.CoreWebView2 != null && !String.IsNullOrEmpty(this.NavigateUrl))
			{
				this._cancelNavigation = false;
				webView.CoreWebView2.Navigate(this.NavigateUrl);
			}
		}

		private void tsbnNodesSave_Click(Object sender, EventArgs e)
			=> this.OnSaveEvent();

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
			if(this._cancelNavigation)
			{
				e.Cancel = true;
				this._cancelNavigation = false;
			} else
			{
				base.Cursor = Cursors.WaitCursor;
				this.Plugin.Trace.TraceEvent(TraceEventType.Start, 1, "Loading...");
			}
		}

		private async void WebView_NavigationCompleted(Object sender, CoreWebView2NavigationCompletedEventArgs e)
		{
			base.Cursor = Cursors.Arrow;
			this.NavigateUrl = webView.CoreWebView2.Source;
			splitAdvancedEdit.Panel2Collapsed = true;

			String title = webView.CoreWebView2.DocumentTitle;
			if(title.Length > 23)
				title = title.Substring(0, 10) + "..." + title.Substring(title.Length - 10);

			this.Window.Caption = String.Format(CultureInfo.CurrentCulture, "{0} - {1}", "Browser", title);
			this.Plugin.Trace.TraceEvent(TraceEventType.Stop, 1);

			await WebView2Utils.InjectInterceptionScript(webView);

			foreach(RadioButton ctrl in flowNodes.Controls)
				if(ctrl.Tag != null)
				{
					await WebView2Utils.HighlightElementByXPath(webView, (String)ctrl.Tag, ctrl.ForeColor);
				}
		}

		private void WebView_CoreWebView2InitializationCompleted(Object sender, CoreWebView2InitializationCompletedEventArgs e)
		{
			if(webView.CoreWebView2 != null)
			{
				webView.CoreWebView2.HistoryChanged += CoreWebView2_HistoryChanged;
			}
		}

		private void CoreWebView2_HistoryChanged(Object sender, Object e)
		{
			bnBack.Enabled = webView.CanGoBack;
			bnFfwd.Enabled = webView.CanGoForward;
		}

		private void WebView_WebMessageReceived(Object sender, CoreWebView2WebMessageReceivedEventArgs e)
		{
			var message = e.TryGetWebMessageAsString();
			if(message.StartsWith("MouseDown:"))
			{
				var parts = message.Split(new[] { ':' }, 3);
				var button = parts[1];
				var path = parts[2];
				Body_MouseDown(button, path);
			} else if(message.StartsWith("MouseMove:"))
			{
				var path = message.Substring("MouseMove:".Length);
				Body_MouseMove(path);
			}
		}

		void Body_MouseDown(String button, String elementXPath)
		{
			switch(button)
			{
			case "Left":
				this._cancelNavigation = true;
				if(!String.IsNullOrEmpty(elementXPath))
					this.SetButtonPath(elementXPath);
				break;
			case "Right":
				this._cancelNavigation = true;
				webView.CoreWebView2.Settings.AreDefaultContextMenusEnabled = false;

				if(!String.IsNullOrEmpty(elementXPath))
				{
					_currentRightClickedElementPath = elementXPath;
					cmsAdvancedTemplate.Show(webView, PointToClient(Cursor.Position));
				}
				break;
			}
		}

		void Body_MouseMove(String elementXPath)
		{
			tsslNodePath.Text = elementXPath ?? String.Empty;
		}

		private void bnBack_Click(Object sender, EventArgs e)
			=> webView.GoBack();

		private void bnFfwd_Click(Object sender, EventArgs e)
			=> webView.GoForward();

		private void tsAdvanced_Resize(Object sender, EventArgs e)
			=> txtAdvancedPath.Width = tsAdvanced.Width - (tsbnAdvancedSave.Width + 5);

		private void splitAdvancedEdit_MouseDoubleClick(Object sender, MouseEventArgs e)
		{
			if(splitAdvancedEdit.SplitterRectangle.Contains(e.Location))
				splitAdvancedEdit.Panel2Collapsed = true;
		}
	}
}