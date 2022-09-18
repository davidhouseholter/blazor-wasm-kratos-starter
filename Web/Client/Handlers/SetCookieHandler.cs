using Microsoft.AspNetCore.Components.WebAssembly.Http;

namespace KratosBlazorApp.Client.Handlers
{
    public class SetCookieHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
