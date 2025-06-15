using MauiStoreApp.ViewModels;

namespace MauiStoreApp.Views;

public partial class CategoryPage : ContentPage
{
	public CategoryPage(CategoryPageViewModel categoryPageViewModel)
	{
		InitializeComponent();
		BindingContext = categoryPageViewModel;
	}
}