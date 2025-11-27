using Core.Models.Music;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Core.Helpers
{
    public static class PickerHelper
    {
        private static string[] _supportedExtensions = new[]
        {
                ".mp3", ".wav", ".mp4"
        };

        public static async Task<Track?> GetTrack(string source)
        {
            MusicProperties meta = await GetMetadata(source);

            return new Track()
            {
                Artist = meta.Artist,
                Album = meta.Album,
                Title = meta.Title == String.Empty
                    ? Path.GetFileNameWithoutExtension(source)
                    : meta.Title,
                Extension = Path.GetExtension(source),
                Duration = meta.Duration,
                Source = source
            };
        }

        public static async Task<List<Track>> GetTracks(string source)
        {
            var files = Directory.GetFiles(source);

            var tracks = new List<Track>();

            foreach (var file in files)
            {
                var track = await GetTrack(file);

                if (track is null) continue;

                tracks.Add(track);
            }

            return tracks;
        }

        public static async Task<string?> GetTrackFileSource()
        {
            PickOptions options = new PickOptions
            {
                FileTypes = new FilePickerFileType(
                    new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                        { DevicePlatform.WinUI, _supportedExtensions }
                })
            };

            FileResult result = await FilePicker.Default.PickAsync(options);
            return result?.FullPath;
        }

        public static async Task<string?> GetTracksFolderSource()
        {
            var folderPicker = new FolderPicker();

            foreach (var ext in _supportedExtensions)
                folderPicker.FileTypeFilter.Add(ext);

            var mauiWindow = App.Current!.Windows.FirstOrDefault();
            var nativeWindow = (mauiWindow?.Handler?.PlatformView as Microsoft.UI.Xaml.Window);
            var hWnd = WindowNative.GetWindowHandle(nativeWindow);
            InitializeWithWindow.Initialize(folderPicker, hWnd);

            var folder = await folderPicker.PickSingleFolderAsync();
            return folder?.Path;
        }

        public static async Task<MusicProperties> GetMetadata(string path)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);

            MusicProperties props = await file.Properties.GetMusicPropertiesAsync();

            return props;
        }

        public static async Task<ImageSource> GetAlbumArt(string path)
        {
            StorageFile file = await StorageFile.GetFileFromPathAsync(path);

            StorageItemThumbnail thumb =
                await file.GetThumbnailAsync(ThumbnailMode.MusicView, 300);

            return ImageSource.FromStream(() => thumb.AsStreamForRead());
        }
    }
}
