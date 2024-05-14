using KKLauncher.Web.Contracts.Collections;
using Microsoft.AspNetCore.Components;

namespace KKLauncher.Web.Client.Pages.CollectionComponents
{
    public partial class CollectionPageComponent
    {
        [Parameter]
        public CollectionViewDto? Collection { get; set; }
    }
}
