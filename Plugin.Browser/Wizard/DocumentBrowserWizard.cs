using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using Plugin.Browser.Properties;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.Browser
{
	public partial class DocumentBrowserWizard : UserControl, IPluginSettings<DocumentBrowserWizardSettings>
	{
		#region Fields
		private IPluginDescription _callerPlugin;
		private DocumentBrowserWizardSettings _settings;
		private Boolean _cancelNavigation = false;
		//private Boolean _eventsAttached = false;
		#endregion Fields
		#region Properties
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

		/// <summary>Плагин, вызывающий событие</summary>
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

		#endregion Properties

		/// <summary>Сообщение, которое будет вызвано по завершению компиляции</summary>
		public event EventHandler<DataEventArgs> SaveNodesEvent;

		public DocumentBrowserWizard()
		{
			InitializeComponent();
			splitAdvancedEdit.Panel2Collapsed = true;
			splitBrowser.Panel2Collapsed = true;
			tsNodes.Visible = false;
		}

		protected override void OnCreateControl()
		{
			this.Window.Caption = "Browser";
			this.Window.SetTabPicture(Resources.iconBrowser);
			this.Window.Shown += new EventHandler(Window_Shown);
			this.Window.Closed += new EventHandler(Window_Closed);

			this.NavigateUrl = this.Settings.NavigateUrl;

			splitBrowser.Panel2Collapsed = false;
			tsNodes.Visible = true;

			Int32 colorIndex = 1;
			foreach(KeyValuePair<String,String> node in this.Settings.Nodes)
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
		}

		private void Window_Shown(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
				this.bnNavigate_Click(this, e);
		}

		private void Window_Closed(Object sender, EventArgs e)
			=> this.Settings.NavigateUrl = this.NavigateUrl;

		private void SetButtonPath(String path)
		{
			foreach(RadioButton ctrl in flowNodes.Controls)
				if(ctrl.Checked)
				{
					if(ctrl.Tag != null)
					{
						foreach(HtmlElement oldElement in new XPathHtml(browser.Document.Body, (String)ctrl.Tag))
							Utils.RemoveHilight(oldElement);
					}
					foreach(HtmlElement element in new XPathHtml(browser.Document.Body, path))
						if(element != null)
						{
							Utils.HilightElement(element, ctrl.ForeColor);
							ctrl.Tag = path;
							ttNodes.SetToolTip(ctrl, path);
						}
					break;
				}
		}

		/// <summary>Установить подсказку для расширенного элемента управления</summary>
		/// <param name="element">Первый найденный элемент в коллекции</param>
		/// <param name="totalCount">Общее количество найденных элементов соответствующих критерию поиска</param>
		private void SetAdvancedPath(HtmlElement element, Int32 totalCount)
		{
			lvAdvancedNodes.Items.Clear();
			txtAdvancedPath.AutoCompleteCustomSource.Clear();
			if(element != null)
			{
				if(totalCount == 1)
				{
					List<ListViewItem> itemList = new List<ListViewItem>();
					foreach(KeyValuePair<String, String> attribute in Utils.GetValidHtmlAttributes(element))
						itemList.Add(new ListViewItem(new String[] { attribute.Key, attribute.Value == null ? "<null>" : attribute.Value, }));
					lvAdvancedNodes.Items.AddRange(itemList.ToArray());
					lvAdvancedNodes.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

					List<String> itemAutoComplete = new List<String>();
					foreach(HtmlElement children in element.Children)
						itemAutoComplete.Add(Utils.GetElementPath(children));
					txtAdvancedPath.AutoCompleteCustomSource.AddRange(itemAutoComplete.ToArray());
				}else
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
				String path=(String)button.Tag;
				if(path == null)
					tsslNodePath.Text = String.Empty;
				else
				{
					tsslNodePath.Text = path;
					//HtmlElement element = Utils.FindElement(tsslNodePath.Text, browser.Document.Body);
				}
				break;
			}
		}

		private void cmsAdvancedTemplate_ItemClicked(Object sender, ToolStripItemClickedEventArgs e)
		{
			HtmlElement element = (HtmlElement)cmsAdvancedTemplate.Tag;
			if(e.ClickedItem == tsmiAdvancedEdit)
			{
				splitAdvancedEdit.Panel2Collapsed = false;
				txtAdvancedPath.Text = Utils.GetElementPath(element);
				this.SetAdvancedPath(element, 1);
			}
		}

		private void cmsAdvancedTemplate_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
		{
			browser.IsWebBrowserContextMenuEnabled = true;
			cmsAdvancedTemplate.Tag = null;
		}

		private void txtAdvancedPath_TextChanged(Object sender, EventArgs e)
		{
			if(txtAdvancedPath.SelectionLength == txtAdvancedPath.Text.Length)
				return;

			HtmlElement first = null;
			Int32 count = 0;
			XPathHtml xpath = new XPathHtml(browser.Document.Body, txtAdvancedPath.Text);
			foreach(HtmlElement element in xpath)
			{
				if(first == null)
					first = element;
				count++;
			}

			String oldPath = txtAdvancedPath.Tag as String;
			String newPath;
			if(first == null)
				newPath = null;
			/*else if(count == 1)//Затирает кастомный путь
				newPath = Utils.GetElementPath(first);*/
			else
				newPath = txtAdvancedPath.Text;

			if(oldPath != newPath)
			{
				tsbnAdvancedSave.Enabled = first != null;
				txtAdvancedPath.Tag = newPath;
				this.SetAdvancedPath(first, count);
			}
		}

		private void tsbnAdvancedSave_Click(Object sender, EventArgs e)
			=> this.SetButtonPath(txtAdvancedPath.Text);

		private void bnNavigate_Click(Object sender, EventArgs e)
		{
			if(!String.IsNullOrEmpty(this.NavigateUrl))
			{
				this._cancelNavigation = false;
				browser.Navigate(this.NavigateUrl);
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

		private void browser_Navigating(Object sender, WebBrowserNavigatingEventArgs e)
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

		private void browser_DocumentCompleted(Object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			base.Cursor = Cursors.Arrow;
			this.NavigateUrl = browser.Document.Url.ToString();
			splitAdvancedEdit.Panel2Collapsed = true;

			String title = browser.Document.Title;
			if(title.Length > 23)
				title = title.Substring(0, 10) + "..." + title.Substring(title.Length - 10);

			this.Window.Caption = String.Format(CultureInfo.CurrentCulture, "{0} - {1}", "Browser", title);
			this.Plugin.Trace.TraceEvent(TraceEventType.Stop, 1);

			//if(!this._eventsAttached)
			//{
				browser.Document.Body.MouseMove += new HtmlElementEventHandler(Body_MouseMove);
				browser.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown);
				//this._eventsAttached = true;
			//}

			foreach(RadioButton ctrl in flowNodes.Controls)
				if(ctrl.Tag != null)
				{
					XPathHtml xpath = new XPathHtml(browser.Document.Body, (String)ctrl.Tag);
					foreach(HtmlElement element in xpath)
						Utils.HilightElement(element, ctrl.ForeColor);
				}
		}

		private void browser_CanGoForwardChanged(Object sender, System.EventArgs e)
			=> bnFfwd.Enabled = browser.CanGoBack;

		private void browser_CanGoBackChanged(Object sender, System.EventArgs e)
			=> bnBack.Enabled = browser.CanGoForward;

		void Body_MouseDown(Object sender, HtmlElementEventArgs e)
		{
			switch(e.MouseButtonsPressed)
			{
			case MouseButtons.Left:
				if(e.CtrlKeyPressed && !e.ShiftKeyPressed && !e.AltKeyPressed)
				{
					this._cancelNavigation = true;
					e.BubbleEvent = false;
					HtmlElement element = browser.Document.GetElementFromPoint(e.ClientMousePosition);
					if(element != null)
						this.SetButtonPath(Utils.GetElementPath(element));
				}
				break;
			case MouseButtons.Right:
				if(e.CtrlKeyPressed && !e.ShiftKeyPressed && !e.AltKeyPressed)
				{
					this._cancelNavigation = true;
					browser.IsWebBrowserContextMenuEnabled = false;
					e.BubbleEvent = false;

					HtmlElement element=browser.Document.GetElementFromPoint(e.ClientMousePosition);
					if(element != null)
					{
						String path = Utils.GetElementPath(element);
						cmsAdvancedTemplate.Tag = element;
						cmsAdvancedTemplate.Show(browser, e.ClientMousePosition);
					}
				}
				break;
			}
		}

		void Body_MouseMove(Object sender, HtmlElementEventArgs e)
		{
			HtmlElement element = browser.Document.GetElementFromPoint(e.ClientMousePosition);
			if(element == null)
				tsslNodePath.Text = String.Empty;
			else
			{
				String path = Utils.GetElementPath(element);
				tsslNodePath.Text = path;
			}
		}

		private void bnBack_Click(Object sender, EventArgs e)
			=> browser.GoBack();

		private void bnFfwd_Click(Object sender, EventArgs e)
			=> browser.GoForward();

		private void tsAdvanced_Resize(Object sender, EventArgs e)
			=> txtAdvancedPath.Width = tsAdvanced.Width - (tsbnAdvancedSave.Width + 5);

		private void splitAdvancedEdit_MouseDoubleClick(Object sender, MouseEventArgs e)
		{
			if(splitAdvancedEdit.SplitterRectangle.Contains(e.Location))
				splitAdvancedEdit.Panel2Collapsed = true;
		}
	}
}