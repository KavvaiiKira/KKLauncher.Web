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

        public async Task<IEnumerable<AppViewDto>> GetAppsByPCLocalIpAsync(string pcLocalIp)
        {
            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppViewDto>>($"{_baseUri}pcapps/{pcLocalIp}");
            return res ?? Enumerable.Empty<AppViewDto>();
        }

        public async Task<IEnumerable<AppViewDto>> SearchAppsAsync(string localIp, string appNameContainsKey)
        {
            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppViewDto>>($"{_baseUri}appsearch/{localIp}/{appNameContainsKey}");
            return res ?? Enumerable.Empty<AppViewDto>();
        }

        public async Task<AppViewDto?> GetAppViewByIdAsync(Guid appId)
        {
            var res = await _httpClient.GetFromJsonAsync<AppViewDto>($"{_baseUri}appview/{appId}");
            return res;
        }

        public async Task<bool> DeleteAppAsync(Guid appId)
        {
            var res = await _httpClient.DeleteAsync($"{_baseUri}{appId}");

            return res.IsSuccessStatusCode ?
                await res.Content.ReadFromJsonAsync<bool>() :
                false;
        }
    }
}
