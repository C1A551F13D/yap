using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;

namespace lib.Providers
{
	public class HttpRequestProcessor : IHttpRequestProcessor
	{
		private IHttpResponseProcessor httpResponseProcessor;

		public HttpRequestProcessor(IHttpResponseProcessor httpResponseProcessor)
		{
			this.httpResponseProcessor = httpResponseProcessor;
		}

		public async Task ProcessContextAsync(HttpContext context)
		{
			await httpResponseProcessor.ProcessContextAsync(context);
		}
	}
}