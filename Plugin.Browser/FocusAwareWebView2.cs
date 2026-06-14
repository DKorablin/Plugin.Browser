using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;

namespace Plugin.Browser
{
	/// <summary>WebView2 that supports docking-framework focus integration.</summary>
	internal sealed class FocusAwareWebView2 : WebView2
	{
		private Control _focusTarget;

		static FocusAwareWebView2()
			=> EnsureWebView2LoaderLoaded();

		/// <summary>Control to notify when browser content receives focus. Defaults to this control.</summary>
		public Control FocusTarget
		{
			get => this._focusTarget ?? this;
			set => this._focusTarget = value;
		}

		/// <summary>Signals the docking framework that the browser is active, then returns keyboard focus to the browser.</summary>
		public void NotifyFocus()
		{
			if(this.ContainsFocus || this.FocusTarget.ContainsFocus)
				return;

			this.FocusTarget.Select();
			if(!this.ContainsFocus)
				this.Focus();
		}

		private static void EnsureWebView2LoaderLoaded()
		{
			String assemblyFolder = Path.GetDirectoryName(typeof(FocusAwareWebView2).Assembly.Location);
			if(String.IsNullOrEmpty(assemblyFolder))
				return;

			String runtimeFolder;
			if(RuntimeInformation.ProcessArchitecture == Architecture.X64)
				runtimeFolder = "win-x64";
			else if(RuntimeInformation.ProcessArchitecture == Architecture.X86)
				runtimeFolder = "win-x86";
			else if(RuntimeInformation.ProcessArchitecture == Architecture.Arm64)
				runtimeFolder = "win-arm64";
			else
				runtimeFolder = null;
			if(String.IsNullOrEmpty(runtimeFolder))
				return;

			String loaderPath = Path.Combine(assemblyFolder, "runtimes", runtimeFolder, "native", "WebView2Loader.dll");
			if(!File.Exists(loaderPath))
				return;

			IntPtr webView2Loader = LoadLibrary(loaderPath);
			if(webView2Loader == IntPtr.Zero)
			{
				Int32 errorCode = Marshal.GetLastWin32Error();
				throw new DllNotFoundException($"Failed to load '{loaderPath}' with Win32 error 0x{errorCode:X8}.");
			}
		}

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern IntPtr LoadLibrary(String fileName);
	}
}
