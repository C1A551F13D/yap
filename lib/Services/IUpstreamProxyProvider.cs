using System.Net.Http;

namespace lib.Services
{
	public interface IUpstreamProxyProvider
	{
		void Initialize(HttpClientHandler handler);
	}
}