using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using SAL.Flatbed;
using SAL.Windows;

namespace Plugin.Browser
{
	public class PluginWindows : IPlugin
	{
		private Dictionary<String, DockState> _documentTypes;

		internal ITraceSource Trace { get; }

		internal IHostWindows HostWindows { get; }

		internal IMenuItem BrowserMenu { get; set; }

		private Dictionary<String, DockState> DocumentTypes
		{
			get
			{
				if(this._documentTypes == null)
					this._documentTypes = new Dictionary<String, DockState>()
					{
						{ typeof(DocumentBrowser).ToString(), DockState.Document },
						{ typeof(DocumentBrowserWizard).ToString(), DockState.Document },
					};
				return this._documentTypes;
			}
		}

		public PluginWindows(IHostWindows hostWindows, ITraceSource trace)
		{
			this.HostWindows = hostWindows ?? throw new ArgumentNullException(nameof(hostWindows));
			this.Trace = trace ?? throw new ArgumentNullException(nameof(trace));
		}

		public IWindow GetPluginControl(String typeName, Object args)
			=> this.CreateWindow(typeName, false, args);

		/// <summary>Search for HTML nodes in the DOM structure returned by WebBrowser</summary>
		/// <param name="body">Root DOM element</param>
		/// <param name="xpath">XPath to elements relative to the root element in XPath similarity</param>
		/// <returns>Array of elements found in the DOM structure matching the XPath similarity</returns>
		public IEnumerable<HtmlElement> FindNodes(HtmlElement body, String xpath)
			=> new XPathHtml(body, xpath);

		Boolean IPlugin.OnConnection(ConnectMode mode)
		{
			IHostWindows host = this.HostWindows;
			if(host == null)
				this.Trace.TraceEvent(TraceEventType.Error, 10, "Plugin {0} requires {1}", this, typeof(IHostWindows));
			else
			{
				IMenuItem menuView = host.MainMenu.FindMenuItem("View");
				if(menuView != null)
				{
					this.BrowserMenu = menuView.Create("&Browser");
					this.BrowserMenu.Name = "View.Browser";
					this.BrowserMenu.Click += (sender,e) => { this.CreateWindow(typeof(DocumentBrowser).ToString(), false); };
					menuView.Items.Insert(0, this.BrowserMenu);
					return true;
				}
			}
			return false;
		}

		Boolean IPlugin.OnDisconnection(DisconnectMode mode)
		{
			if(this.BrowserMenu != null)
				this.HostWindows.MainMenu.Items.Remove(this.BrowserMenu);
			return true;
		}

		private IWindow CreateWindow(String typeName, Boolean searchForOpened, Object args = null)
			=> this.DocumentTypes.TryGetValue(typeName, out DockState state)
				? this.HostWindows.Windows.CreateWindow(this, typeName, searchForOpened, state, args)
				: null;
	}
}