namespace KKLauncher.Web.Client.Pages.Main
{
    public partial class LoginRedirect
    {
        protected override void OnInitialized()
        {
            _navigationManager.NavigateTo("/login");
        }
    }
}
