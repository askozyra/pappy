using Core.Models.Music;
using Windows.Storage;
using Windows.Storage.FileProperties;

namespace Core.Helpers
{
    public static class PickerHelper
    {
        private static string[] _supportedExtensions;

        static PickerHelper()
        {
            _supportedExtensions = new[]
            {
                ",mp3", ".wav", ".mp4"
            };
        }

        public static async Task<Track?> OpenMedia()
        {
            PickOptions options = new PickOptions
            {
                PickerTitle = "Select a file",
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                        { DevicePlatform.WinUI, _supportedExtensions }
                })
            };

            FileResult result = await FilePicker.Default.PickAsync(options);

            if (result is null)
                return null;

            var meta = await GetMetadata_Uwp(result.FullPath);

            return new Track()
            {
                Artist = meta.Artist,
                Album = meta.Album,
                Title = meta.Title,
                Duration = meta.Duration,
                Source = result.FullPath
            };
        }

        public static async Task<MusicProperties> GetMetadata_Uwp(string path)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);

            MusicProperties props = await file.Properties.GetMusicPropertiesAsync();

            return props;
        }

        public static async Task<ImageSource> GetAlbumArt_Uwp(string path)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);

            StorageItemThumbnail thumb =
                await file.GetThumbnailAsync(ThumbnailMode.MusicView, 300);

            if (thumb is null)
                return null;

            return ImageSource.FromStream(() => thumb.AsStreamForRead());
        }
    }
}
