using Core.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        public AudioManager AudioManager { get; set; }

        public PlayerViewModel(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
