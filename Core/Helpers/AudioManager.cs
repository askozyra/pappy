using CommunityToolkit.Maui.Views;
using Core.Models.Music;
using System.ComponentModel;

namespace Core.Helpers
{
    public class AudioManager : INotifyPropertyChanged
    {
        private Track _currentTrack;

        public MediaElement MediaPlayer { get; set; }
        public List<Track> PlaybackQueue { get; set; }

        public Track CurrentTrack
        {
            get => _currentTrack;
            set
            {
                if (_currentTrack != value)
                {
                    _currentTrack = value;
                    OnPropertyChanged(nameof(CurrentTrack));
                }
            }
        }

        public AudioManager()
        {
            MediaPlayer = new MediaElement();
        }

        public void RegisterPlayer(MediaElement player)
        {
            MediaPlayer = player;
        }

        public void SetCurrentTrack(Track track)
        {
            CurrentTrack = track;
        }

        public void PlayPause()
        {
            if (MediaPlayer.CurrentState == CommunityToolkit.Maui.Core.MediaElementState.Playing)
                MediaPlayer.Pause();
            else
                MediaPlayer.Play();
        }

        public void Stop()
        {
            MediaPlayer.Stop();
        }

        private void OnPropertyChanged(string name)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
