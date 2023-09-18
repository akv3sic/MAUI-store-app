using MauiStoreApp.ViewModels;

namespace MauiStoreApp.Views;

public partial class RecentlyViewedPage : ContentPage
{
	public RecentlyViewedPage(RecentlyViewedPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}