using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Plugin.Browser
{
	internal static class Utils
	{
		#region Fields
		private const Char NodeSeparator = '/';
		internal static Color[] Colors = { Color.Black, Color.Gray, Color.Maroon, Color.Olive, Color.Green, Color.Teal, Color.Navy, Color.Purple, Color.White, Color.Silver, Color.Red, Color.Yellow, Color.Lime, Color.Aqua, Color.Blue, Color.Fuchsia };
		private static String[] ValidAttributes = new String[] { "className", "id", "style", "width", "height", "innerText", };
		private static String OldStyleAttribute = Guid.NewGuid().ToString("N");
		#endregion Fields

		/// <summary>Получить значение элемента</summary>
		/// <param name="element">Элемент в котором искать значение</param>
		/// <param name="property">Свойство значение которого необходимо вернуть</param>
		/// <remarks>
		/// 1) Поиск производится через атрибуты
		/// 2) Поиск производится через рефлексию к __ComObject DomElement
		/// </remarks>
		/// <returns>Значение свойства</returns>
		public static String GetElementPropertyValue(HtmlElement element, String property)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));
			if(String.IsNullOrEmpty(property))
				throw new ArgumentNullException(nameof(property));

			String result = element.GetAttribute(property);
			if(String.IsNullOrEmpty(result))
			{//В MSIE 9 получить свойство через DomElement не получается. Т.к. там он ComObject.
				var objProperty = element.DomElement.GetType().GetProperty(property);
				if(objProperty != null)
				{
					Object value = objProperty.GetValue(element.DomElement, null);
					result = value == null ? String.Empty : value.ToString();
				}
			}
			return result;
		}

		public static String GetElementPath(HtmlElement element)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));

			String result = String.Empty;
			while(element.Parent != null)
			{
				Int32 count = 0;
				if(element.Parent != null && element.Parent.Children.Count > 1)
					foreach(HtmlElement child in element.Parent.Children)
					{
						count++;
						if(child == element)
							break;
					}
				if(element.TagName.Equals("body", StringComparison.OrdinalIgnoreCase))
					break;//BODY tag
				result = Utils.NodeSeparator + element.TagName.ToLowerInvariant() + (count == 0 ? String.Empty : "[" + (count - 1) + "]") + result;
				element = element.Parent;
			}
			return result;
		}

		public static void HilightElement(HtmlElement element, Color color)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));

			if(element.Style != null)
				element.SetAttribute(Utils.OldStyleAttribute, element.Style);
			element.Style = "border:1px solid " + ColorTranslator.ToHtml(color);
		}

		public static void RemoveHilight(HtmlElement element)
			=> element.Style = element.GetAttribute(Utils.OldStyleAttribute);

		/// <summary>Получить валидные значения атрибутов элемента</summary>
		/// <remarks>Сначала пробегаюсь по списку атрибутов, а затем, если в массиве валидных атрибутов ещё что-то осталось, то через рефлексию по пропертям</remarks>
		/// <param name="element">Элемент</param>
		/// <returns>Список значений и атрибутов</returns>
		public static IEnumerable<KeyValuePair<String, String>> GetValidHtmlAttributes(HtmlElement element)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));

			List<String> validAttributes = new List<String>(Utils.ValidAttributes);

			//String typeName = Microsoft.VisualBasic.Information.TypeName(element.DomElement);
			//MessageBox.Show(typeName);

			for(Int32 index = validAttributes.Count - 1; index >= 0; index--)
			{
				String attribute = validAttributes[index];
				String value = element.GetAttribute(attribute);
				if(!String.IsNullOrEmpty(value) && !"System.__ComObject".Equals(value, StringComparison.OrdinalIgnoreCase))
				{
					yield return new KeyValuePair<String, String>(attribute, value);
					validAttributes.RemoveAt(index);
				}
			}

			if(validAttributes.Count > 0)
			{//А теперь через рефлексию к элементу
				var properties = element.GetType().GetProperties();
				foreach(var property in properties)
					for(Int32 index = validAttributes.Count - 1; index >= 0; index--)
					{
						String attribute = validAttributes[index];
						if(property.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase))
						{
							Object value = property.GetValue(element, null);
							if(value != null)
							{
								yield return new KeyValuePair<String, String>(attribute, value.ToString());
								validAttributes.RemoveAt(index);
							}
							break;
						}
					}
			}
			if(validAttributes.Count > 0)
			{//А теперь через рефлексию к DomElement
				var properties = element.DomElement.GetType().GetProperties();
				foreach(var property in properties)
					for(Int32 index = validAttributes.Count - 1; index >= 0; index--)
					{
						String attribute = validAttributes[index];
						if(property.Name.Equals(attribute, StringComparison.OrdinalIgnoreCase))
						{
							Object value = property.GetValue(element.DomElement, null);
							yield return new KeyValuePair<String, String>(attribute, value == null ? null : value.ToString());
							validAttributes.RemoveAt(index);
							break;
						}
					}
			}
		}
	}
}