using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;

namespace lib.Providers
{
	public class ProxyResponseProcessor : IProxyResponseProcessor
	{
		private IHttpContextAccessor httpContextAccessor;
		private Settings settings;
		private IBodyStringReplacer bodyStringReplacer;

		public ProxyResponseProcessor(
			IHttpContextAccessor httpContextAccessor,
			IOptionsSnapshot<Settings> settings,
			IBodyStringReplacer bodyStringReplacer)
		{
			this.httpContextAccessor = httpContextAccessor;
			this.settings = settings.Value;
			this.bodyStringReplacer = bodyStringReplacer;
		}

		public async Task<HttpResponse> ProcessResponseAsync(HttpResponseMessage response, IHttpResponseBodyFeature httpResponseBodyFeature)
		{
			return await GetResponseAsync(response, httpResponseBodyFeature);
		}

		private async Task<HttpResponse> GetResponseAsync(HttpResponseMessage response, IHttpResponseBodyFeature httpResponseBodyFeature)
		{
			ProxyHeaders(httpContextAccessor.HttpContext.Response, response);

			httpContextAccessor.HttpContext.Response.StatusCode = (int)response.StatusCode;
			await httpResponseBodyFeature.StartAsync();
			await ProxyBodyAsync(httpContextAccessor.HttpContext.Response, response);

			return httpContextAccessor.HttpContext.Response;
		}

		private void ProxyHeaders(HttpResponse target, HttpResponseMessage source)
		{
			target.Headers.Clear();

			foreach (var header in source.Content.Headers)
			{
				if (!settings.ProxyResponse.Headers.Remove.Contains(header.Key) && !header.Key.StartsWith(":"))
				{
					target.Headers.Add(header.Key, header.Value.ToArray());
				}
			}

			foreach (var header in source.Headers)
			{
				if (!settings.ProxyResponse.Headers.Remove.Contains(header.Key) && !header.Key.StartsWith(":"))
				{
					target.Headers.Add(header.Key, header.Value.ToArray());
				}
			}

			foreach (var header in settings.ProxyResponse.Headers.Modify)
			{
				if (target.Headers.ContainsKey(header.Name) && !header.AllowMultiple)
				{
					target.Headers.Remove(header.Name);
				}

				target.Headers.Add(header.Name, header.Value);
			}
		}

		private async Task ProxyBodyAsync(HttpResponse target, HttpResponseMessage source)
		{
			if ((source.Content.Headers.ContentLength ?? 1) > 0)
			{
				if (settings.StringBasedMediaTypes.Contains(source.Content.Headers.ContentType.MediaType))
				{
					await bodyStringReplacer.ReplaceAsync(target, source, settings.ProxyResponse.Body.Replace);
				}
				else
				{
					var body = await source.Content.ReadAsByteArrayAsync();
					await target.Body.WriteAsync(body, 0, body.Length);
				}
			}
		}
	}
}