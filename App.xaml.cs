using BrownianMotionMauiApp.Views;

namespace BrownianMotionMauiApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        Routing.RegisterRoute(nameof(BrownianMotionPage), typeof(BrownianMotionPage));
    }
}
