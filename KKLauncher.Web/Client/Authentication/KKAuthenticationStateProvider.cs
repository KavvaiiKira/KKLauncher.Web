using Blazored.LocalStorage;
using KKLauncher.Web.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace KKLauncher.Web.Client.Authentication
{
    public class KKAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;

        public KKAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;   
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (token == null || !token.LoggedIn)
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonympusPrincipal = new ClaimsPrincipal(anonymousIdentity);

                return new AuthenticationState(anonympusPrincipal);
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Country, "Russia"),
                new Claim(ClaimTypes.NameIdentifier, token.LoginIp),
                new Claim(ClaimTypes.Role, "User"),
                new Claim("KKLauncherClient", "Client")
            };

            var identity = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }
    }
}
