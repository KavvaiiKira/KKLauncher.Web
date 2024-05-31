using KKLauncher.Web.Contracts.Apps;
using KKLauncher.Web.Contracts.Collections;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace KKLauncher.Web.Client.Services
{
    public class CollectionService : ICollectionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUri;

        public CollectionService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _baseUri = $"{navigationManager.BaseUri}api/kk/v1/collection/";
        }

        public async Task<bool> AddCollectionAsync(CollectionDto collectionDto)
        {
            var res = await _httpClient.PutAsJsonAsync($"{_baseUri}", collectionDto);
            return res.IsSuccessStatusCode ?
                await res.Content.ReadFromJsonAsync<bool>() :
                false;
        }

        public async Task<IEnumerable<CollectionViewDto>> GetCollectionsByPCLocalIpAsync(string pcLocalIp)
        {
            var res = await _httpClient.GetFromJsonAsync<IEnumerable<CollectionViewDto>>($"{_baseUri}pccollections/{pcLocalIp}");
            return res ?? Enumerable.Empty<CollectionViewDto>();
        }

        public async Task<CollectionViewDto?> GetCollectionViewByIdAsync(Guid collectionId)
        {
            var res = await _httpClient.GetFromJsonAsync<CollectionViewDto>($"{_baseUri}collectionview/{collectionId}");
            return res;
        }

        public async Task<IEnumerable<AppViewDto>> GetCollectionAppsAsync(Guid collectionId)
        {
            var res = await _httpClient.GetFromJsonAsync<IEnumerable<AppViewDto>>($"{_baseUri}apps/{collectionId}");
            return res ?? Enumerable.Empty<AppViewDto>();
        }
    }
}
