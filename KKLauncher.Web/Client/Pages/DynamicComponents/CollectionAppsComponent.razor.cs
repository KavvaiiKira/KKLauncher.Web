using KKLauncher.Web.Contracts.Apps;
using Microsoft.AspNetCore.Components;

namespace KKLauncher.Web.Client.Pages.DynamicComponents
{
    public partial class CollectionAppsComponent
    {
        [Parameter]
        public Guid? CollectionId { get; set; }

        private List<AppViewDto> _collectionApps = new List<AppViewDto>();

        protected override async Task OnParametersSetAsync()
        {
            if (CollectionId == null)
            {
                return;
            }

            var collectionApps = await _collectionService.GetCollectionAppsAsync(CollectionId.Value);
            _collectionApps = collectionApps.ToList();
        }

        public void Dispose()
        {
        }
    }
}
