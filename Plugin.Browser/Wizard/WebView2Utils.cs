using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;

namespace Plugin.Browser
{
	internal static class WebView2Utils
	{
		public static async Task InjectInterceptionScript(WebView2 webView)
		{
			String script = @"
            document.addEventListener('mousemove', e => {
                if (e.ctrlKey && !e.shiftKey && !e.altKey) {
                    let path = getElementXPath(e.target);
                    window.chrome.webview.postMessage('MouseMove:' + path);
                }
            });

            document.addEventListener('mousedown', e => {
                if (e.ctrlKey && !e.shiftKey && !e.altKey) {
                    e.preventDefault();
                    e.stopPropagation();
                    let path = getElementXPath(e.target);
                    let button = e.button === 0 ? 'Left' : e.button === 2 ? 'Right' : 'Other';
                    window.chrome.webview.postMessage('MouseDown:' + button + ':' + path);
                }
            }, true);

            function getElementXPath(element) {
                if (element.id !== '')
                    return 'id(""' + element.id + '"")';
                if (element === document.body)
                    return element.tagName;

                var ix = 0;
                var siblings = element.parentNode.childNodes;
                for (var i = 0; i < siblings.length; i++) {
                    var sibling = siblings[i];
                    if (sibling === element)
                        return getElementXPath(element.parentNode) + '/' + element.tagName + '[' + (ix + 1) + ']';
                    if (sibling.nodeType === 1 && sibling.tagName === element.tagName)
                        ix++;
                }
            };
        ";
			await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(script);
		}

		public static async Task<List<String>> GetXPathResult(WebView2 webView, String xpath)
		{
			String script = $@"
            var iterator = document.evaluate('{xpath}', document, null, XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
            var result = [];
            var node = iterator.iterateNext();
            while (node) {{
                result.push(getElementXPath(node));
                node = iterator.iterateNext();
            }}
            result;
        ";
			var jsonResult = await webView.CoreWebView2.ExecuteScriptAsync(script);
			return JsonConvert.DeserializeObject<List<String>>(jsonResult);
		}

		public static async Task HighlightElementByXPath(WebView2 webView, String xpath, Color color)
		{
			String colorStr = ColorTranslator.ToHtml(color);
			String script = $@"
            var iterator = document.evaluate('{xpath}', document, null, XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
            var node = iterator.iterateNext();
            while (node) {{
                node.style.outline = '2px solid {colorStr}';
                node = iterator.iterateNext();
            }}
        ";
			await webView.CoreWebView2.ExecuteScriptAsync(script);
		}

		public static async Task RemoveHighlightByXPath(WebView2 webView, String xpath)
		{
			String script = $@"
            var iterator = document.evaluate('{xpath}', document, null, XPathResult.ORDERED_NODE_ITERATOR_TYPE, null);
            var node = iterator.iterateNext();
            while (node) {{
                node.style.outline = '';
                node = iterator.iterateNext();
            }}
        ";
			await webView.CoreWebView2.ExecuteScriptAsync(script);
		}

		public static async Task<Dictionary<String, String>> GetElementAttributes(WebView2 webView, String xpath)
		{
			String script = $@"
            var node = document.evaluate('{xpath}', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
            var attrs = {{}};
            if (node) {{
                for (var i = 0; i < node.attributes.length; i++) {{
                    attrs[node.attributes[i].name] = node.attributes[i].value;
                }}
            }}
            JSON.stringify(attrs);
        ";
			var jsonResult = await webView.CoreWebView2.ExecuteScriptAsync(script);
			var result = JsonConvert.DeserializeObject<String>(jsonResult);
			return JsonConvert.DeserializeObject<Dictionary<String, String>>(result);
		}

		public static async Task<List<String>> GetElementChildrenPaths(WebView2 webView, String xpath)
		{
			String script = $@"
            var node = document.evaluate('{xpath}', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;
            var childrenPaths = [];
            if (node) {{
                for (var i = 0; i < node.children.length; i++) {{
                    childrenPaths.push(getElementXPath(node.children[i]));
                }}
            }}
            childrenPaths;
        ";
			var jsonResult = await webView.CoreWebView2.ExecuteScriptAsync(script);
			return JsonConvert.DeserializeObject<List<String>>(jsonResult);
		}
	}
}