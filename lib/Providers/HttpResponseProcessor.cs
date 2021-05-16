using System.Net;
using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace lib.Providers
{
	public class HttpResponseProcessor : IHttpResponseProcessor
	{
		private IProxyRequestProcessor proxyRequestProcessor;
		private IProxyResponseProcessor proxyResponseProcessor;

		public HttpResponseProcessor(
			IProxyRequestProcessor proxyRequestProcessor,
			IProxyResponseProcessor proxyResponseProcessor
		)
		{
			this.proxyRequestProcessor = proxyRequestProcessor;
			this.proxyResponseProcessor = proxyResponseProcessor;
		}

		public async Task ProcessContextAsync(HttpContext context)
		{
			var httpResponseBodyFeature = context.Features.Get<IHttpResponseBodyFeature>();
			var response = await proxyRequestProcessor.ProcessRequestAsync(context.Request);
			var result = await proxyResponseProcessor.ProcessResponseAsync(response, httpResponseBodyFeature);

			if (result.StatusCode != (int)HttpStatusCode.NotModified)
			{
				await httpResponseBodyFeature.CompleteAsync();
			}
		}
	}
}