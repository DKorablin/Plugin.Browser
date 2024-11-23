using System;
using System.Collections.Generic;
using SAL.Flatbed;

namespace Plugin.Browser
{
	/// <summary>Аргументы получения списка наименование и XPath для поиска узлов в HTML'е через Trident</summary>
	public class SelectedNodesEventArgs : DataEventArgs
	{
		private readonly KeyValuePair<String, String>[] _nodes;

		/// <summary>Количество ключей в массиве</summary>
		public override Int32 Count => this._nodes.Length;

		/// <summary>Массив наименований XPath узлов в HTML'е</summary>
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

		/// <summary>Получить XPath узла по ключу узла</summary>
		/// <typeparam name="T">Тип получаемых данных (XPath в строковой форме)</typeparam>
		/// <param name="key">Ключ получения XPath</param>
		/// <returns>XPath до узла</returns>
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