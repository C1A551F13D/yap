using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace lib.Providers
{
	public class ProxyRequestProcessor : IProxyRequestProcessor
	{
		private IHttpClientHandlerFactory httpClientHandlerFactory;
		private Settings settings;
		private IBodyStringReplacer bodyStringReplacer;

		public ProxyRequestProcessor(
			IHttpClientHandlerFactory httpClientHandlerFactory,
			IOptionsSnapshot<Settings> settings,
			IBodyStringReplacer bodyStringReplacer)
		{
			this.httpClientHandlerFactory = httpClientHandlerFactory;
			this.settings = settings.Value;
			this.bodyStringReplacer = bodyStringReplacer;
		}

		public async Task<HttpResponseMessage> ProcessRequestAsync(HttpRequest request)
		{
			using (var handler = httpClientHandlerFactory.GetHandler())
			using (var client = new HttpClient(handler))
			{
				var proxiedRequest = await GetRequestMessageAsync(request);

				return await client.SendAsync(proxiedRequest);
			}
		}

		private async Task<HttpRequestMessage> GetRequestMessageAsync(HttpRequest request)
		{
			var url = $"{settings.BaseHref}{request.Path}{request.QueryString}";
			var result = new HttpRequestMessage(new HttpMethod(request.Method), url);

			ProxyHeaders(result, request);
			await ProxyBodyAsync(result, request);

			return result;
		}

		private void ProxyHeaders(HttpRequestMessage target, HttpRequest source)
		{
			target.Headers.Clear();

			foreach (var header in source.Headers)
			{
				if (!settings.ProxyRequest.Headers.Remove.Contains(header.Key)
					&& !header.Key.StartsWith(":")
					&& !header.Key.StartsWith("Content-"))
				{
					target.Headers.Add(header.Key, header.Value.ToArray());
				}
			}

			foreach (var header in settings.ProxyRequest.Headers.Modify)
			{
				if (target.Headers.Contains(header.Name) && !header.AllowMultiple)
				{
					target.Headers.Remove(header.Name);
				}

				target.Headers.Add(header.Name, header.Value);
			}
		}

		private async Task ProxyBodyAsync(HttpRequestMessage target, HttpRequest source)
		{
			if (settings.StringBasedMediaTypes.Contains(source.ContentType))
			{
				await bodyStringReplacer.ReplaceAsync(target, source, settings.ProxyRequest.Body.Replace);
			}
		}
	}
}