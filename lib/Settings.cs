using System;

namespace lib
{
	public class Settings
	{
		public string BaseHref { get; set; } = string.Empty;
		public bool IgnoreServerCertificateErrors { get; set; } = false;
		public UpstreamProxySettings UpstreamProxy { get; set; } = new UpstreamProxySettings();

		public Proxy ProxyRequest { get; set; } = new Proxy();
		public Proxy ProxyResponse { get; set; } = new Proxy();
		public string[] StringBasedMediaTypes { get; set; } = new[]
		{
			"text/css",
			"text/csv",
			"text/html",
			"text/calendar",
			"text/javascript",
			"application/json",
			"application/ld+json",
			"text/javascript",
			"application/x-httpd-php",
			"application/rtf",
			"application/x-sh",
			"image/svg+xml",
			"text/plain",
			"application/xhtml+xml",
			"application/xml",
		};
	}

	public class UpstreamProxySettings
	{
		public string Host { get; set; } = string.Empty;
		public int Port { get; set; } = 0;
	}

	public class Proxy
	{
		public ProxyHeaders Headers { get; set; } = new ProxyHeaders();
		public ProxyBody Body { get; set; } = new ProxyBody();
	}

	public class ProxyHeaders
	{
		public string[] Remove { get; set; } = Array.Empty<string>();
		public ModifyHeader[] Modify { get; set; } = Array.Empty<ModifyHeader>();
	}

	public class ProxyBody
	{
		public ReplaceString[] Replace { get; set; } = Array.Empty<ReplaceString>();
	}

	public class ModifyHeader
	{
		public string Name { get; set; } = string.Empty;
		public string[] Value { get; set; } = Array.Empty<string>();
		public bool AllowMultiple { get; set; } = false;
	}

	public class ReplaceString
	{
		public string OldValue { get; set; } = string.Empty;
		public string NewValue { get; set; } = string.Empty;
	}
}