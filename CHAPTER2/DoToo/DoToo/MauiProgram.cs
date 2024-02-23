using DoToo.Repositories;
using DoToo.ViewModel;
using DoToo.Views;
using Microsoft.Extensions.Logging;

namespace DoToo
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
                })
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IToDoItemRepository, ToDoItemRepository>();

            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<ItemViewModel>();

            return builder;
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainView>();
            builder.Services.AddTransient<ItemView>();

            return builder;
        }
    }
}
