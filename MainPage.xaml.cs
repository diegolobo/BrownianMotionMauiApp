using BrownianMotionMauiApp.ViewModels;

namespace BrownianMotionMauiApp;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}

