using Core.Helpers;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class AppShellViewModel
    {
        public AudioManager AudioManager { get; set; }

        public ICommand OpenMediaCommand { get; set; }

        public AppShellViewModel(AudioManager audioManager)
        {
            AudioManager = audioManager;

            OpenMediaCommand = new Command(OpenMedia);
        }

        private async void OpenMedia()
        {
            var res = await PickerHelper.OpenMedia();
            if (res is not null)
            {
                AudioManager.SetCurrentTrack(res);
            }
        }
    }
}
