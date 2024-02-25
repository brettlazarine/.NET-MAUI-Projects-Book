using Microsoft.Extensions.Logging;
using News.MVVM.ViewModels;
using News.MVVM.Views;
using News.Services;

namespace News
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
                    fonts.AddFont("FontAwesome.otf", "FontAwesome");
                })
                .RegisterAppTypes();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterAppTypes(this MauiAppBuilder mauiAppBuilder)
        {
            // Services
            mauiAppBuilder.Services.AddSingleton<INewsService>((serviceProvider) => new Services.NewsService());
            mauiAppBuilder.Services.AddSingleton<INavigate>((serviceProvider) => new Navigator());

            // ViewModels
            mauiAppBuilder.Services.AddTransient<HeadlinesViewModel>();

            //Views
            mauiAppBuilder.Services.AddTransient<AboutView>();
            mauiAppBuilder.Services.AddTransient<ArticleView>();
            mauiAppBuilder.Services.AddTransient<HeadlinesView>();

            return mauiAppBuilder;
        }
    }
}
