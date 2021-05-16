using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace lib.Services
{
	public interface IProxyResponseProcessor
	{
		Task<HttpResponse> ProcessResponseAsync(HttpResponseMessage response, IHttpResponseBodyFeature httpResponseBodyFeature);
	}
}