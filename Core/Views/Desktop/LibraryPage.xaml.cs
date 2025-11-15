using Core.ViewModels;

namespace Core.Views.Desktop;

public partial class LibraryPage : ContentPage
{
    // List<Track>

    public LibraryPage(LibraryViewModel libraryVM)
    {
        InitializeComponent();
        BindingContext = libraryVM;

    }

}