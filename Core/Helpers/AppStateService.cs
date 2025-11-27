namespace Core.Helpers
{
    public class AppStateService
    {
        public AudioManager AudioManager { get; set; }

        public AppStateService(AudioManager audioManager)
        {
            AudioManager = audioManager;
        }
    }
}
