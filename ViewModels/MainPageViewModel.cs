using BrownianMotionMauiApp.Views;

using CommunityToolkit.Mvvm.ComponentModel;

using System.Windows.Input;

namespace BrownianMotionMauiApp.ViewModels;
public class MainPageViewModel : ObservableObject
{
    #region Constructors

    public MainPageViewModel()
    {
        NavigateCommand = new Command(OnNavigateClicked);
    }

    #endregion

    #region Commands

    public ICommand NavigateCommand { get; }

    #endregion

    #region Private Methods

    private static async void OnNavigateClicked() => await Shell.Current.GoToAsync(nameof(BrownianMotionPage));
    
    #endregion
}
