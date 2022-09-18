using KratosBlazorApp.Shared.Models.Identity;
using Newtonsoft.Json;
using Ory.Kratos;
using Ory.Kratos.Client.Api;

namespace KratosBlazorApp.Server.Middleware.Identity
{
    public class KratosService
    {
        private readonly string _kratosUrl;
        private readonly HttpClient _client;
        private readonly string _kratosLocalUrl = "https://localhost";
        public KratosService(string kratosUrl)
        {
            _client = new HttpClient();
            _kratosUrl = kratosUrl;
        }

        public async Task<string> GetUserIdByToken(string token, bool isLocalDev)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{(isLocalDev ? _kratosLocalUrl : _kratosUrl)}/sessions/whoami");
            request.Headers.Add("Authorization", token);
            return await SendWhoamiRequestAsync(request);
        }

        public async Task<string> GetUserIdByCookie(string cookieName, string cookieContent, bool isLocalDev)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{(isLocalDev ? _kratosLocalUrl : _kratosUrl)}/sessions/whoami");
            request.Headers.Add("Cookie", $"{cookieName}={cookieContent}");
            return await SendWhoamiRequestAsync(request);
        }

        private async Task<string> SendWhoamiRequestAsync(HttpRequestMessage request)
        {
            var res = await _client.SendAsync(request);
            res.EnsureSuccessStatusCode();
            var json = await res.Content.ReadAsStringAsync();
            var whoami = JsonConvert.DeserializeObject<KratosWhoami>(json);
            if (whoami == null && !whoami.Active)
                throw new InvalidOperationException("Session is not active.");

            return whoami.Identity.Id;
        }
    }
}
