using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace lib.Services
{
	public interface IBodyStringReplacer
	{
		Task ReplaceAsync(HttpRequestMessage target, HttpRequest source, ReplaceString[] replacements);
		Task ReplaceAsync(HttpResponse target, HttpResponseMessage source, ReplaceString[] replacements);
	}
}