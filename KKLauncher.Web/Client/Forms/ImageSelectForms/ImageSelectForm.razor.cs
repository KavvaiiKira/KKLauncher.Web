using KKLauncher.Web.Client.Constants;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace KKLauncher.Web.Client.Forms.ImageSelectForms
{
    public partial class ImageSelectForm
    {
        public Dictionary<int, string> ImagesWithKeys { get; set; }

        private int _selectedImageIndex = 0;
        private bool _sixImagesReady = false;
        private byte[] _uploadedImage = new byte[] { };
        private string _uploadedImageUrl = string.Empty;
        private IJSObjectReference _moduleTask;

        protected override void OnInitialized()
        {
            _sixImagesReady = ImagesWithKeys?.Count == 6;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            _moduleTask = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./js/FileImportUtils.js");
        }

        private void ImageSelectionChanged(int newIndex)
        {
            if (newIndex == _selectedImageIndex)
            {
                return;
            }

            _uploadedImage = new byte[] { };
            _uploadedImageUrl = string.Empty;

            _selectedImageIndex = newIndex;

            StateHasChanged();
        }

        private async Task BrowseImage()
        {
            await _moduleTask.InvokeVoidAsync("InvokeImport");
        }

        private async Task UploadedImageChanged(InputFileChangeEventArgs e)
        {
            var buffer = e.File.Size;
            if (buffer == 0)
            {
                return;
            }

            var fileName = e.File.Name;
            if (string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var fileExtension = fileName.Split('.').Last().ToLower();
            if (!AllowedFileExtensionConstants.AllowedAppImageExtensions.Contains(fileExtension))
            {
                //TODO: Toasts
                return;
            }

            byte[]? resultImageBytes = null;

            using (var ms = new MemoryStream())
            {
                await e.File.OpenReadStream(buffer).CopyToAsync(ms);
                resultImageBytes = ms.ToArray();
            }

            if (resultImageBytes == null)
            {
                return;
            }

            _uploadedImage = resultImageBytes;

            var imagesrc = Convert.ToBase64String(_uploadedImage);
            _uploadedImageUrl = string.Format("data:image/" + fileExtension + ";base64,{0}", imagesrc);

            _selectedImageIndex = -1;

            StateHasChanged();
        }

        public async Task<byte[]> GetImage()
        {
            if (_uploadedImage.Length != 0)
            {
                return _uploadedImage;
            }

            var imagePath = Path.Combine("icons", "app", ImagesWithKeys[_selectedImageIndex]);

            return await _httpClient.GetByteArrayAsync(imagePath);
        }
    }
}
