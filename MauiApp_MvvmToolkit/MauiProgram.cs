using CommunityToolkit.Mvvm.Messaging;
using MauiApp_MvvmToolkit.Entities.Cart;
using MauiApp_MvvmToolkit.Views;
using Microsoft.Extensions.Logging;

namespace MauiApp_MvvmToolkit
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
                });

#if DEBUG
		builder.Logging.AddDebug();



#endif
            builder.Services.AddSingleton<IMessenger>(StrongReferenceMessenger.Default);
            builder.Services.AddSingleton<CartStore>();


            builder.Services.AddTransient<ProductViewModel>();
            builder.Services.AddTransient<ProductView>((s) => new ProductView()
            {
                BindingContext = s.GetRequiredService<ProductViewModel>()
            });

           

            builder.Services.AddTransient<CartViewModel>();
            builder.Services.AddTransient<CartView>((s) => new CartView()
            {
                BindingContext = s.GetRequiredService<CartViewModel>()
            });


            return builder.Build();
        }
    }
}