using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace Plugin.Browser
{
	/// <summary>WebView2 that supports docking-framework focus integration.</summary>
	internal sealed class FocusAwareWebView2 : WebView2
	{
		private Control _focusTarget;

		/// <summary>Control to notify when browser content receives focus. Defaults to this control.</summary>
		public Control FocusTarget
		{
			get => this._focusTarget ?? this;
			set => this._focusTarget = value;
		}

		/// <summary>Signals the docking framework that the browser is active, then returns keyboard focus to the browser.</summary>
		public void NotifyFocus()
		{
			this.FocusTarget.Select();
			this.Focus();
		}
	}
}
