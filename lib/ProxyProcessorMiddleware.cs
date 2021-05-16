using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;

namespace lib
{
	public class ProxyProcessorMiddleware
	{
		public ProxyProcessorMiddleware(RequestDelegate next)
		{
		}

		public async Task InvokeAsync(HttpContext context, IHttpRequestProcessor httpRequestProcessor)
		{
			await httpRequestProcessor.ProcessContextAsync(context);
		}
	}
}