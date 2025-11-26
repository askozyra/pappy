using Core.Models.Music;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Core.ViewModels
{
    public class LibraryViewModel : INotifyPropertyChanged, IQueryAttributable
    {
        private ObservableCollection<Track> _currentTracks;
        private Track _selectedItem;

        public ObservableCollection<Track> CurrentTracks
        {
            get { return _currentTracks; }
            set
            {
                _currentTracks = value;
                OnPropertyChanged();
            }
        }

        public Track SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayMediaCommand { get; set; }

        public LibraryViewModel()
        {
            CurrentTracks = new ObservableCollection<Track>();

            PlayMediaCommand = new Command<Track>(PlayMedia);
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            query.TryGetValue("Tracks", out var obj);
            if (obj is not null && obj is IList<Track> tracks)
                CurrentTracks = new ObservableCollection<Track>(tracks);
        }

        private async void PlayMedia(Track track)
        {
            await Shell.Current.GoToAsync("///Player", new Dictionary<string, object>()
            {
                { "Track", track }
            });
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
