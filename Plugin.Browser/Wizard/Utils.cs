using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Plugin.Browser
{
	internal static class Utils
	{
		private const Char NodeSeparator = '/';

		internal static readonly Color[] Colors = { Color.Black, Color.Gray, Color.Maroon, Color.Olive, Color.Green, Color.Teal, Color.Navy, Color.Purple, Color.White, Color.Silver, Color.Red, Color.Yellow, Color.Lime, Color.Aqua, Color.Blue, Color.Fuchsia };

		private static String[] ValidAttributes = new String[] { "className", "id", "style", "width", "height", "innerText", };

		private static String OldStyleAttribute = Guid.NewGuid().ToString("N");

		/// <summary>Get the value of an element</summary>
		/// <param name="element">The element in which to search for the value</param>
		/// <param name="property">The property whose value needs to be returned</param>
		/// <remarks>
		/// 1) The search is performed through attributes
		/// 2) The search is performed through reflection on __ComObject DomElement
		/// </remarks>
		/// <returns>The property value</returns>
		public static String GetElementPropertyValue(HtmlElement element, String property)
		{
			_ = element ?? throw new ArgumentNullException(nameof(element));
			if(String.IsNullOrEmpty(property))
				throw new ArgumentNullException(nameof(property));

			String result = element.GetAttribute(property);
			if(String.IsNullOrEmpty(result))
			{//In MSIE 9, you can't get a property via DomElement because it's a ComObject.
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

		/// <summary>Get valid attribute values ​​for an element</summary>
		/// <remarks>First, I iterate over the list of attributes, and then, if there are any remaining valid attributes in the array, I use property reflection.</remarks>
		/// <param name="element">Element</param>
		/// <returns>List of values ​​and attributes</returns>
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
			{//And now through reflection to the element
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
			{//And now through reflection to DomElement
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