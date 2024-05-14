namespace KKLauncher.Web.Client.Forms.ImageSelectForms
{
    public partial class ImageSelectForm
    {
        public Dictionary<int, string> ImagesWithKeys { get; set; }

        private int _selectedImageIndex = 0;
        private bool _sixImagesReady = false;

        protected override void OnInitialized()
        {
            _sixImagesReady = ImagesWithKeys?.Count == 6;
        }

        private void ImageSelectionChanged(int newIndex)
        {
            if (newIndex == _selectedImageIndex)
            {
                return;
            }

            _selectedImageIndex = newIndex;

            StateHasChanged();
        }

        private async Task BrowseImage()
        {

        }

        public async Task<byte[]> GetImage()
        {
            var imagePath = Path.Combine("wwwroot", "icons", "app", ImagesWithKeys[_selectedImageIndex]);

            return await _httpClient.GetByteArrayAsync(imagePath);
        }
    }
}
