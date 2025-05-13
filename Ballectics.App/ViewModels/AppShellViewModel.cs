using Ballectics.App.Pages;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class AppShellViewModel : BasePageViewModel
{

    [RelayCommand]
    async Task LogOut()
    {
        await RunOnUiAsync(() =>
        {
            Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        });

    }
}