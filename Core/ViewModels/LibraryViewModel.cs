using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Windows.Storage.Pickers;
using WinRT.Interop;

namespace Core.ViewModels
{
    public class Track
    {
        public string Title { get; set; }
        public string Source { get; set; }
    }

    public class LibraryViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> CurrentTracks { get; set; }


        public ICommand OpenFolderCommand { get; set; }
        public ICommand PlayMediaCommand { get; set; }

        private Track _selectedItem;
        public Track SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public LibraryViewModel()
        {
            CurrentTracks = new ObservableCollection<string>();

            OpenFolderCommand = new Command(OpenFolder);
            PlayMediaCommand = new Command<string>(PlayMedia);
            // add play btn on hover
            // add playback controls on bottom?
        }

        private void PlayMedia(string source)
        {

        }

        private async void OpenFolder()
        {
            string res = await OpenMedia();
            AddFiles(res);
        }

        private async Task<string> OpenMedia()
        {
            try
            {
#if WINDOWS
                var folderPicker = new FolderPicker();

                // MUST specify a file type filter (even if it's "*")
                folderPicker.FileTypeFilter.Add("*");

                // Get the MAUI window handle and attach it to the picker
                var mauiWindow = App.Current!.Windows.FirstOrDefault();
                var nativeWindow = (mauiWindow?.Handler?.PlatformView as Microsoft.UI.Xaml.Window);
                var hWnd = WindowNative.GetWindowHandle(nativeWindow);
                InitializeWithWindow.Initialize(folderPicker, hWnd);

                // Show dialog
                var folder = await folderPicker.PickSingleFolderAsync();
                return folder?.Path;
#else
    return null;
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error picking file: {ex.Message}");
            }

            return null;
        }

        private void AddFiles(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);

            foreach (var file in files)
            {
                CurrentTracks.Add(Path.GetFileName(file));
            }
        }



        private void OnPropertyChanged(string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
