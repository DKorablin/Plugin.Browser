using System;
using System.Collections.Generic;

namespace Plugin.Browser
{
	public class DocumentBrowserWizardSettings
	{
		/// <summary>Плагин, вызывающий событие</summary>
		public String CallerPluginId { get; set; }

		/// <summary>Массив узлов, где ключ - константа, а в значение будет XPath для парсинга иерархии нодов в Trident'е</summary>
		public Dictionary<String, String> Nodes { get; set; }

		/// <summary>Ссылка на страницу, на которой осуществить парсинг массива узлов</summary>
		public String NavigateUrl { get; set; }
	}
}