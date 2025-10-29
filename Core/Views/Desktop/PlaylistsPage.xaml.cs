using Core.ViewModels;

namespace Core.Views.Desktop
{
    public partial class PlaylistsPage : ContentPage
    {
        public PlaylistsPage(PlaylistsViewModel context)
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}