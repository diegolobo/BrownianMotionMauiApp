using BrownianMotionMauiApp.ViewModels;
using BrownianMotionMauiApp.Views;

namespace BrownianMotionMauiApp.Configurations;

public static class BuilderConfiguration
{
    public static MauiAppBuilder ConfigureApp(this MauiAppBuilder builder)
    {
        ConfigureViews(builder);
        ConfigureViewModels(builder);

        builder.ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
        });

        return builder;
    }

    private static void ConfigureViews(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<BrownianMotionPage>();
    }

    private static void ConfigureViewModels(MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddSingleton<BrownianMotionViewModel>();
    }
}
