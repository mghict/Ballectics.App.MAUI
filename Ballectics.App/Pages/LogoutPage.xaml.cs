using Ballectics.App.Services;

namespace Ballectics.App.Pages;

public partial class LogoutPage : ContentPage
{
    private readonly IServiceProvider serviceProvider;
    private readonly StorageService storageService;
    public LogoutPage(IServiceProvider serviceProvider)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;
        this.storageService = serviceProvider.GetRequiredService<StorageService>();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Promt();
    }

    private async void GoToLoginPage()
    {
        storageService.DeleteUser();
        storageService.DeleteToken();
        var login = serviceProvider.GetRequiredService<LoginPage>();

        Application.Current.MainPage = login;

    }

    private async void GoToHome()
    {
        await Shell.Current.GoToAsync($"{nameof(HomePage)}");;
    }

    private async void Promt()
    {
        Loaded += async (_, _) =>
        {
            bool result = await Shell.Current.DisplayAlert("ausloggen", "Möchten Sie sich wirklich abmelden?", "Ja, ich bin sicher.", "Nein");

            if (result)
            {

                GoToLoginPage();
            }
            else
            {
                GoToHome();

            }
        };
    }
}