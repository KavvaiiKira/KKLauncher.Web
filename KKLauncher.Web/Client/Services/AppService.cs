using KKLauncher.Web.Contracts.Apps;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace KKLauncher.Web.Client.Services
{
    public class AppService : IAppService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public AppService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = $"{navigationManager.BaseUri}api/kk/v1/app/";
        }

        public async Task<bool> AddAppAsync(AppDto appDto)
        {
            var res = await _httpClient.PutAsJsonAsync($"{_baseUri}", appDto);
            return res.IsSuccessStatusCode ?
                await res.Content.ReadFromJsonAsync<bool>() :
                false;
        }
    }
}
