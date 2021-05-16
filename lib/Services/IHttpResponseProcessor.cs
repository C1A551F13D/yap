using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace lib.Services
{
	public interface IHttpResponseProcessor
	{
		Task ProcessContextAsync(HttpContext context);
	}
}