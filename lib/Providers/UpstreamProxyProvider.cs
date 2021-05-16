using System.Net;
using System.Net.Http;
using lib.Services;
using Microsoft.Extensions.Options;

namespace lib.Providers
{
	public class UpstreamProxyProvider : IUpstreamProxyProvider
	{
		private Settings settings;

		public UpstreamProxyProvider(IOptionsSnapshot<Settings> settings)
		{
			this.settings = settings.Value;
		}

		public void Initialize(HttpClientHandler handler)
		{
			if (!string.IsNullOrWhiteSpace(settings.UpstreamProxy.Host))
			{
				handler.Proxy = new WebProxy(settings.UpstreamProxy.Host, settings.UpstreamProxy.Port);
			}
			else
			{
				handler.Proxy = null;
			}
		}
	}
}