using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace lib.Services
{
	public interface IProxyRequestProcessor
	{
		Task<HttpResponseMessage> ProcessRequestAsync(HttpRequest request);
	}
}