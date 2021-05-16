using System.Net.Http;

namespace lib.Services
{
	public interface IHttpClientHandlerFactory
	{
		HttpClientHandler GetHandler();
	}
}