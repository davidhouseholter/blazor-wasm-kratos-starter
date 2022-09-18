using KratosBlazorApp.Shared.Models;
using Ory.Kratos.Client.Client;
using System;
using System.Net.Http.Json;

namespace KratosBlazorApp.Shared.Services
{

    public interface IUserManager
    {
        Task<bool> InitializeAsync(bool state);
        Task<CurrentUser?> CurrentUserInfo();
    }
    public class UserManager : IUserManager
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly string baseAddress = "";
        public UserManager(HttpClient httpClient, IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            baseAddress = _configuration["BaseAddress"];
        }
        public async Task<CurrentUser?> CurrentUserInfo()
        {
            var result = await _httpClient.GetFromJsonAsync<CurrentUser?>($"{baseAddress}user/whoami");
            return result;
        }

        public Task<bool> InitializeAsync(bool state)
        {
            return Task.FromResult(true);
        }
    }
}
