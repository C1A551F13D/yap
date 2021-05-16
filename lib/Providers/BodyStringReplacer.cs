using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using lib.Services;
using Microsoft.AspNetCore.Http;

namespace lib.Providers
{
	public class BodyStringReplacer : IBodyStringReplacer
	{
		public async Task ReplaceAsync(HttpRequestMessage target, HttpRequest source, ReplaceString[] replacements)
		{
			using (var content = new StreamContent(source.Body))
			{
				var result = await content.ReadAsStringAsync();

				foreach (var replacement in replacements)
				{
					result = result.Replace(replacement.OldValue, replacement.NewValue);
				}

				target.Content = new StringContent(result);
			}
		}

		public virtual async Task ReplaceAsync(HttpResponse target, HttpResponseMessage source, ReplaceString[] replacements)
		{
			var result = await source.Content.ReadAsStringAsync();

			foreach (var replacement in replacements)
			{
				result = result.Replace(replacement.OldValue, replacement.NewValue);
			}

			var encoding = Encoding.GetEncoding(source.Content.Headers.ContentType.CharSet ?? "UTF-8");

			var body = encoding.GetBytes(result);
			await target.Body.WriteAsync(body, 0, body.Length);
		}
	}
}