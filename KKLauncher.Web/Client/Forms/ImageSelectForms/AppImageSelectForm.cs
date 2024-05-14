namespace KKLauncher.Web.Client.Forms.ImageSelectForms
{
    public class AppImageSelectForm : ImageSelectForm
    {
        public AppImageSelectForm()
        {
            ImagesWithKeys = new Dictionary<int, string>()
            {
                { 0, "app-default-1.png" },
                { 1, "app-default-2.png" },
                { 2, "app-default-3.png" },
                { 3, "app-default-4.png" },
                { 4, "app-default-5.png" },
                { 5, "app-default-6.png" }
            };
        }
    }
}
