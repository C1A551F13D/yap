using lib.Providers;
using lib.Services;
using Microsoft.AspNetCore.Http;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ProxyServiceCollectionExtensions
	{
		public static IServiceCollection AddProxyProcessor(this IServiceCollection services)
		{
			services.AddScoped<IHttpRequestProcessor, HttpRequestProcessor>();
			services.AddScoped<IHttpResponseProcessor, HttpResponseProcessor>();
			services.AddScoped<IProxyRequestProcessor, ProxyRequestProcessor>();
			services.AddScoped<IProxyResponseProcessor, ProxyResponseProcessor>();
			services.AddTransient<IUpstreamProxyProvider, UpstreamProxyProvider>();
			services.AddScoped<IHttpClientHandlerFactory, HttpClientHandlerFactory>();
			services.AddScoped<IBodyStringReplacer, BodyStringReplacer>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			return services;
		}
	}
}