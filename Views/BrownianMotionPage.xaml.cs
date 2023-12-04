using BrownianMotionMauiApp.ViewModels;

namespace BrownianMotionMauiApp.Views;

public partial class BrownianMotionPage : ContentPage
{
    public BrownianMotionPage(BrownianMotionViewModel viewModel)
    {
		InitializeComponent();
        BindingContext = viewModel;
    }
}