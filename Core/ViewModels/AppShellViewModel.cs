using Core.Helpers;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class AppShellViewModel
    {
        public AudioManager AudioManager { get; set; }

        public ICommand OpenMediaCommand { get; set; }
        public ICommand OpenFolderCommand { get; set; }

        public AppShellViewModel(AudioManager audioManager)
        {
            AudioManager = audioManager;

            OpenMediaCommand = new Command(OpenMedia);
            OpenFolderCommand = new Command(OpenFolder);
        }

        private async void OpenMedia()
        {
            var path = await PickerHelper.GetTrackFileSource();
            var track = await PickerHelper.GetTrack(path);

            if (track is null)
                return;

            await Shell.Current.GoToAsync("///Player", new Dictionary<string, object>()
            {
                { "Track", track }
            });
        }

        private async void OpenFolder()
        {
            var path = await PickerHelper.GetTracksFolderSource();
            var tracks = await PickerHelper.GetTracks(path);

            if (tracks is null)
                return;

            await Shell.Current.GoToAsync("///Library", new Dictionary<string, object>()
            {
                { "Tracks", tracks }
            });
        }
    }
}
