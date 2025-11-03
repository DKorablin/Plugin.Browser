using System;
using System.Collections.Generic;

namespace Plugin.Browser
{
	public class DocumentBrowserWizardSettings
	{
		/// <summary>The plugin that triggers the event</summary>
		public String CallerPluginId { get; set; }

		/// <summary>An array of nodes, where the key is a constant and the value is the XPath for parsing the node hierarchy in Trident</summary>
		public Dictionary<String, String> Nodes { get; set; }

		/// <summary>A link to the page where to parse the array of nodes</summary>
		public String NavigateUrl { get; set; }
	}
}