using BrownianMotionMauiApp.Configurations;

using Microsoft.Extensions.Logging;

using SkiaSharp.Views.Maui.Controls.Hosting;

namespace BrownianMotionMauiApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .ConfigureApp();

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
