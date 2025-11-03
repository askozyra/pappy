using Core.ViewModels;

namespace Core.Views.Desktop
{
    public partial class PlaylistsPage : ContentPage
    {
        public PlaylistsPage(AppViewModel appVM, PlaylistsViewModel playlistsVM)
        {
            InitializeComponent();
            BindingContext = new
            {
                App = appVM,
                Playlists = playlistsVM
            };
        }
    }
}