using System;
using System.Collections.Generic;
using SAL.Flatbed;

namespace Plugin.Browser
{
	/// <summary>Arguments for retrieving a list of names and XPaths for searching HTML nodes using Trident</summary>
	public class SelectedNodesEventArgs : DataEventArgs
	{
		private readonly KeyValuePair<String, String>[] _nodes;

		/// <summary>Number of keys in the array</summary>
		public override Int32 Count => this._nodes.Length;

		/// <summary>Array of XPath names of HTML nodes</summary>
		public override IEnumerable<String> Keys
		{
			get
			{
				foreach(KeyValuePair<String, String> node in this._nodes)
					yield return node.Key;
			}
		}

		internal SelectedNodesEventArgs(KeyValuePair<String, String>[] nodes)
			=> this._nodes = nodes;

		/// <summary>Get the XPath of a node by its key</summary>
		/// <typeparam name="T">Type of data to retrieve (XPath in string form)</typeparam>
		/// <param name="key">Key to retrieve XPath</param>
		/// <returns>XPath to node</returns>
		public override T GetData<T>(String key)
		{
			if(String.IsNullOrEmpty(key))
				throw new ArgumentNullException(nameof(key));

			foreach(KeyValuePair<String, String> node in this._nodes)
				if(node.Key == key)
					return (T)(Object)node.Value;

			throw new KeyNotFoundException($"Key '{key}' not found");
		}
	}
}