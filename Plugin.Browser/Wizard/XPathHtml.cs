using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Plugin.Browser
{
	/// <summary>Класс разбора HTML и применение к структуре узлов Trident'а, самописный XPath</summary>
	public class XPathHtml : IEnumerable<HtmlElement>
	{
		private struct XNode
		{
			public String Name { get; set; }
			public Int32? Index { get; set; }
			public KeyValuePair<String, String>? Attribute { get; set; }
		}

		private const Char NodeSeparator = '/';
		private const Char AttributeSeparator = '=';
		private const Char PathArgumentStart = '[';
		private const Char PathArgumentEnd = ']';

		protected HtmlElement Root { get; }

		protected String Path => String.Join(XPathHtml.NodeSeparator.ToString(), this.PathNodes);

		protected String[] PathNodes { get; }

		public XPathHtml(HtmlElement root, String path)
		{
			_ = root ?? throw new ArgumentNullException(nameof(root));
			if(String.IsNullOrEmpty(path))
				throw new ArgumentNullException(nameof(path));

			this.Root = root;
			this.PathNodes = path.Split(new Char[] { XPathHtml.NodeSeparator, }, StringSplitOptions.RemoveEmptyEntries);
		}

		public IEnumerator<HtmlElement> GetEnumerator()
			=> this.FindNodesRec(0, this.Root).GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> this.GetEnumerator();

		private IEnumerable<HtmlElement> FindNodesRec(Int32 index, HtmlElement root)
		{
			foreach(HtmlElement element in XPathHtml.FindNodes(this.PathNodes[index], root))
				if(index + 1 == this.PathNodes.Length)
					yield return element;
				else
					foreach(HtmlElement child in this.FindNodesRec(index + 1, element))
						yield return child;
		}

		/// <summary>Поиск узла в дереве</summary>
		/// <param name="index">Индекс позиции в ХПути</param>
		/// <param name="root">Стартовый узел, с которого надо начать</param>
		/// <returns></returns>
		private static IEnumerable<HtmlElement> FindNodes(String path, HtmlElement root)
		{
			_ = root ?? throw new ArgumentNullException(nameof(root));
			if(String.IsNullOrEmpty(path))
				throw new ArgumentNullException(nameof(path));

			XNode node = XPathHtml.ParsePath(path);

			if(node.Index.HasValue)//Поиск элемента по индексу
			{
				Int32 index = node.Index.Value;
				if(index < 0)
					index = root.Children.Count + index;

				if(root.Children.Count > index
				&& root.Children[index].TagName.Equals(node.Name, StringComparison.OrdinalIgnoreCase))
					yield return root.Children[index];
			} else if(node.Attribute.HasValue)//Поиск элемента по наименованию атрибута
			{
				foreach(HtmlElement element in root.Children)
					if(element.TagName.Equals(node.Name, StringComparison.OrdinalIgnoreCase)
						&& node.Attribute.Value.Value.Equals(Utils.GetElementPropertyValue(element, node.Attribute.Value.Key), StringComparison.OrdinalIgnoreCase))
						yield return element;
			} else
				foreach(HtmlElement element in root.Children)
					if(element.TagName.Equals(node.Name, StringComparison.OrdinalIgnoreCase))
						yield return element;
		}

		private static XNode ParsePath(String path)
		{
			XNode result = new XNode();

			Int32 startIndex = path.IndexOf(XPathHtml.PathArgumentStart);
			Int32 endIndex = path.IndexOf(XPathHtml.PathArgumentEnd);

			if(startIndex > -1 && endIndex > -1)
			{//Найдены условия для поиска узла
				result.Name = path.Substring(0, startIndex);

				String attribute = path.Substring(startIndex + 1, endIndex - startIndex - 1);
				if(Int32.TryParse(attribute, out Int32 dummy))
					result.Index = dummy;
				else
				{
					Int32 index = attribute.IndexOf(AttributeSeparator);
					if(index > -1)
						result.Attribute = new KeyValuePair<String, String>(attribute.Substring(0, index), attribute.Substring(index + 1));
				}
			} else
				result.Name = path;
			return result;
		}
	}
}