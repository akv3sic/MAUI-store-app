using CommunityToolkit.Maui;
using MauiStoreApp.Services;
using MauiStoreApp.ViewModels;
using MauiStoreApp.Views;
using Microsoft.Extensions.Logging;

namespace MauiStoreApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("astore-eczar-semi-bold.ttf", "astore-eczar-semi-bold");
                }).UseMauiCommunityToolkit();

            builder.Services.AddSingleton<BaseService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<CategoryService>();
            builder.Services.AddSingleton<CartService>();
            builder.Services.AddSingleton<UserService>();
            builder.Services.AddTransient<HomePageViewModel>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<ProductDetailsViewModel>();
            builder.Services.AddTransient<ProductDetailsPage>();
            builder.Services.AddTransient<CategoryPageViewModel>();
            builder.Services.AddTransient<CategoryPage>();
            builder.Services.AddTransient<RecentlyViewedPageViewModel>();
            builder.Services.AddTransient<RecentlyViewedPage>();
            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<CartPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
