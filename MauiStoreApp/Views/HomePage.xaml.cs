using MauiStoreApp.ViewModels;

namespace MauiStoreApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void OnScrolled(object sender, ScrolledEventArgs e)
    {
		searchBar.IsVisible = e.ScrollY < 90;
    }
}