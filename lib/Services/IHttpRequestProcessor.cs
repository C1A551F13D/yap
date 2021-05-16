using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace lib.Services
{
	public interface IHttpRequestProcessor
	{
		Task ProcessContextAsync(HttpContext context);
	}
}