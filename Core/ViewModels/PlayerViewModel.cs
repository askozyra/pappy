using Core.Helpers;
using Core.Models.Music;
using System.ComponentModel;

namespace Core.ViewModels
{
    public class PlayerViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        public AudioManager AudioManager { get; set; }

        public PlayerViewModel(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            query.TryGetValue("Track", out var obj);
            if (obj is not null && obj is Track track)
                AudioManager.SetCurrentTrack(track);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
