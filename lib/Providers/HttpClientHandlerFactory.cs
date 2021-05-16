using System.Net;
using System.Net.Http;
using lib.Services;
using Microsoft.Extensions.Options;

namespace lib.Providers
{
	public class HttpClientHandlerFactory : IHttpClientHandlerFactory
	{
		private HttpClientHandler httpClientHandler;
		private object _lock = new object();
		private IUpstreamProxyProvider upstreamProxyProvider;
		private Settings settings;

		public HttpClientHandlerFactory(IUpstreamProxyProvider upstreamProxyProvider, IOptionsSnapshot<Settings> settings)
		{
			this.upstreamProxyProvider = upstreamProxyProvider;
			this.settings = settings.Value;
		}

		public virtual HttpClientHandler GetHandler()
		{
			if (httpClientHandler == null)
			{
				lock (_lock)
				{
					httpClientHandler = httpClientHandler ?? GetNewHandler();
				}
			}

			return httpClientHandler;
		}

		private HttpClientHandler GetNewHandler()
		{
			var result = new HttpClientHandler();

			result.AllowAutoRedirect = true;
			result.AutomaticDecompression = DecompressionMethods.All;

			if (settings.IgnoreServerCertificateErrors)
			{
				result.ServerCertificateCustomValidationCallback = (a, b, c, d) => true;
			}

			upstreamProxyProvider.Initialize(result);

			return result;
		}
	}
}