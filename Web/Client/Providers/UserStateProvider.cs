using KratosBlazorApp.Client.Managers;
using KratosBlazorApp.Shared.Models;
using KratosBlazorApp.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Ory.Kratos.Client.Model;
using System.Security.Claims;

namespace KratosBlazorApp.Shared.Providers
{
    public class UserStateProvider : AuthenticationStateProvider
    {
        private readonly IUserManager api;
        private CurrentUser _currentUser;

        public bool IsAuthenticated { 
            get
            {
                return this._currentUser?.IsAuthenticated ?? false;
            } 
        }
        public UserStateProvider(IUserManager api)
        {
            this.api = api;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            try
            {
                var userInfo = await GetCurrentUser();
                if (userInfo.IsAuthenticated)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, _currentUser.UserName) }.Concat(_currentUser.Claims.Select(c => new Claim(c.Key, c.Value)));
                    identity = new ClaimsIdentity(claims, "Server authentication");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }
            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
        private async Task<CurrentUser> GetCurrentUser()
        {
            try
            {
                if (_currentUser != null && _currentUser.IsAuthenticated) return _currentUser;
                _currentUser = await api.CurrentUserInfo();
                return _currentUser;
            } catch
            {
                return _currentUser = new ()
                {
                    IsAuthenticated = false
                };
            }
        }
        public void Logout()
        {
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Login()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
