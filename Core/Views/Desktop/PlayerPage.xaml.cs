using Core.ViewModels;

namespace Core.Views.Desktop;

public partial class PlayerPage : ContentPage
{
    public PlayerPage(PlayerViewModel playerVM)
    {
        InitializeComponent();

        BindingContext = playerVM;

        playerVM.AudioManager.RegisterPlayer(this.MediaPlayer);
    }
}