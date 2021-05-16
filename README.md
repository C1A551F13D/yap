# yap
YAP - yet another proxy. It's a localhost proxy for a target URL to test changes before they go live.

# Getting Started
```
git clone https://github.com/C1A551F13D/yap.git
cd yap
dotnet run --project web
```
Open a browser and navigate to https://localhost:5001

# appsettings.json
Out of the box, the proxy targets https://www.google.com. It demonstrates adding and removing headers request and response headers, and performing basic string replacements. All of this is controlled via the [appsettings.json](web/appsettings.json) file. Using these settings, you setup your target and other various settings. You can add or remove headers from the request or the response, you can modify the request or response body data. Play around, have fun. It's pretty flexible.

# TODO
- [ ] Token identifiers for replacements (e.g., so you don't have to specify www.google.com throughout appsettings.json)
- [ ] Event handlers/notifications for various injection points
- [ ] I'm sure we can think of more cool ideas

# Disclaimer
This is a powerful tool and should be used responsibly. It is possible to use this tool for alternative purposes (e.g., phishing as a MITM proxy); however, you should only do so if properly authorized.
