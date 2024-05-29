using KKLauncher.Web.Contracts.ResponseDtos;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace KKLauncher.Web.Client.Services
{
    public class AppStartService : IAppStartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public AppStartService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = $"{navigationManager.BaseUri}api/kk/v1/appstarter/";
        }

        public async Task<AppStartResultDto> StartAppAsync(Guid appId)
        {
            var result = await _httpClient.PutAsJsonAsync(_baseUri, appId);

            return await result.Content.ReadFromJsonAsync<AppStartResultDto>() ?? new AppStartResultDto("The server did not respond.");
        }
    }
}
