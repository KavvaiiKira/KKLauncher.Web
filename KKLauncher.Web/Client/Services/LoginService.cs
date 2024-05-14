using KKLauncher.Web.Contracts;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace KKLauncher.Web.Client.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public LoginService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = $"{navigationManager.BaseUri}api/kk/v1/pc/";
        }

        public async Task<PCLoginResponseDto> LoginPcAsync(PCDto pcDto)
        {
            var res = await _httpClient.PutAsJsonAsync($"{_baseUri}login", pcDto);
            if (!res.IsSuccessStatusCode)
            {
                return new PCLoginResponseDto("The server did not respond.");
            }

            return await res.Content.ReadFromJsonAsync<PCLoginResponseDto>() ?? new PCLoginResponseDto("Server Error.");
        }
    }
}
