using KratosBlazorApp.Shared.Models;
using KratosBlazorApp.Shared.Models.Identity;
using KratosBlazorApp.Shared.Providers;
using KratosBlazorApp.Shared.Wrapper;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Newtonsoft.Json;
using Ory.Kratos.Client.Model;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace KratosBlazorApp.Client.Managers
{
    public interface IOryKratosIdentityManager
    {
        Task<KratosSelfServiceLoginFlow> StartKratosSelfServiceLoginFlow();
        Task<KratosSelfServiceLoginFlow> GetKratosSelfServiceLoginFlow(string flowId);
        Task<OryKratosLoginResponse> CompleteKratosSelfServiceLoginFlow(LoginRequest loginParameters, string action);
        Task<KratosSelfServiceRegistrationFlow> GetKratosSelfServiceRegistrationFlow();
        Task<OryKratosRegistrationResponse> CompleteKratosSelfServiceRegistrationFlow(RegisterRequest registerRequest, string action);
        Task<KratosSelfServiceLogoutUrl> Logout();
    }

    public class OryKratosIdentityManager : IOryKratosIdentityManager
    {
        private readonly HttpClient _httpClient;
        private readonly UserStateProvider _userStateProvider;
        private readonly IConfiguration _configuration;
        private readonly string baseAddress = "";
        private readonly string baseIdentityAddress = "";

        public OryKratosIdentityManager(HttpClient httpClient, UserStateProvider userStateProvider, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _userStateProvider = userStateProvider;
            baseAddress = _configuration["BaseAddress"];
            baseIdentityAddress = _configuration["IdentityBaseAddress"];
        }
      
        public async Task<OryKratosLoginResponse> CompleteKratosSelfServiceLoginFlow(LoginRequest loginParameters, string action)
        {
            
            var result = await _httpClient.PostAsJsonAsync($"{action}", loginParameters);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OryKratosLoginResponse>(s);
            _userStateProvider.Login();

            return data;
        }

        public async Task<KratosSelfServiceLoginFlow> GetKratosSelfServiceLoginFlow(string flowId)
        {

            var result = await _httpClient.GetAsync($"{baseIdentityAddress}self-service/login/browser?flow={flowId}");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<KratosSelfServiceLoginFlow>(s);
            return data;
        }

        public async Task<KratosSelfServiceLoginFlow> StartKratosSelfServiceLoginFlow()
        {
            var result = await _httpClient.GetAsync($"{baseIdentityAddress}self-service/login/browser");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<KratosSelfServiceLoginFlow>(s);
            return data;
        }

        public async Task<KratosSelfServiceRegistrationFlow> GetKratosSelfServiceRegistrationFlow()
        {
            var result = await _httpClient.GetAsync($"{baseIdentityAddress}self-service/Registration/browser");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<KratosSelfServiceRegistrationFlow>(s);
            return data;
        }
        public async Task<OryKratosRegistrationResponse> CompleteKratosSelfServiceRegistrationFlow(RegisterRequest registerRequest, string action)
        {

            var result = await _httpClient.PostAsJsonAsync($"{action}", registerRequest);
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<OryKratosRegistrationResponse>(s);
            registerRequest.IdentityId = data.Identity.Id;
            var newUser = await _httpClient.PostAsJsonAsync($"{baseAddress}user/register", registerRequest);
            _userStateProvider.Login();

            return data;
        }

        public async Task<KratosSelfServiceLogoutUrl> Logout()
        {
            var result = await _httpClient.GetAsync($"{baseIdentityAddress}self-service/logout/browser");
            if (result.StatusCode == System.Net.HttpStatusCode.BadRequest) throw new Exception(await result.Content.ReadAsStringAsync());
            result.EnsureSuccessStatusCode();
            var s = await result.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<KratosSelfServiceLogoutUrl>(s);
            var logout = await _httpClient.GetAsync($"{data.LogoutUrl}");

            _userStateProvider.Logout();

            return data;
        }
    }
}
