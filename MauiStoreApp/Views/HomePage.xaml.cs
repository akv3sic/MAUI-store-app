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
        SearchEntry.IsVisible = e.ScrollY < 50;
		if (e.ScrollY < 50)
		{
            SearchBarLayout.Padding = new Thickness(20, 5, 20, 5);
            Logo.HeightRequest = 40;
            Logo.Margin = new Thickness(-31, 0, 0, 0);
            SearchButton.HeightRequest = 22;
            SearchButton.WidthRequest = 22;
        }
        else
        {
            SearchBarLayout.Padding = new Thickness(20, 2, 20, 2);
            Logo.HeightRequest = 35;
            Logo.Margin = new Thickness(-22, 0, 0, 0);
            SearchButton.HeightRequest = 20;
            SearchButton.WidthRequest = 20;
        }
    }
}