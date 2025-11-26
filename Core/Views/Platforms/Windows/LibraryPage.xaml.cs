using Core.ViewModels;

namespace Core.Views.Platforms.Windows;

public partial class LibraryPage : ContentPage
{
    public LibraryPage(LibraryViewModel libraryVM)
    {
        InitializeComponent();

        BindingContext = libraryVM;
    }
}