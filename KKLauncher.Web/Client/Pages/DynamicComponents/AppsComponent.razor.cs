using BlazorComponentBus;
using KKLauncher.Web.Client.Events;
using KKLauncher.Web.Client.Models;
using KKLauncher.Web.Contracts.Apps;

namespace KKLauncher.Web.Client.Pages.DynamicComponents
{
    public partial class AppsComponent
    {
        private List<AppViewDto> _apps = new List<AppViewDto>();

        protected override async Task OnInitializedAsync()
        {
            var token = await _localStorageService.GetItemAsync<LoginToken>(nameof(LoginToken));
            if (token == null)
            {
                return;
            }

            var apps = await _appService.GetAppsByPCLocalIpAsync(token.LoginIp);
            _apps = apps.ToList();
        }

        protected override void OnParametersSet()
        {
            _bus.Subscribe<AppAddedEvent>(AppAddedEventHandler);
            _bus.Subscribe<AppDeletedEvent>(AppDeletedEventHandler);
        }

        private async Task AppAddedEventHandler(MessageArgs message, CancellationToken cancellationToken)
        {
            var eventItem = message.GetMessage<AppAddedEvent>();
            if (eventItem == null)
            {
                return;
            }

            var newApp = await _appService.GetAppViewByIdAsync(eventItem.NewAppId);
            if (newApp == null)
            {
                //TODO: Toasts
                return;
            }

            if (_apps.Any(a => a.Id == newApp.Id))
            {
                return;
            }

            _apps.Add(newApp);

            StateHasChanged();
        }

        private async Task AppDeletedEventHandler(MessageArgs message, CancellationToken cancellationToken)
        {
            var eventItem = message.GetMessage<AppDeletedEvent>();
            if (eventItem == null)
            {
                return;
            }

            var deletingResul = await _appService.DeleteAppAsync(eventItem.DeletedAppId);
            if (!deletingResul)
            {
                //TODO: Toasts
                return;
            }

            _apps.RemoveAll(a => a.Id == eventItem.DeletedAppId);

            StateHasChanged();
        }

        public void Dispose()
        {
            _bus.UnSubscribe<AppAddedEvent>(AppAddedEventHandler);
            _bus.UnSubscribe<AppDeletedEvent>(AppDeletedEventHandler);
        }
    }
}
