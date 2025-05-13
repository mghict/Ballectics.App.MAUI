using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class LoginPageViewModel : BasePageViewModel
{
    private readonly SecurityService securityService;
    private readonly ConnectivityService connectivityService;
    private readonly StorageService storageService;

    public LoginPageViewModel(SecurityService securityService, ConnectivityService connectivityService, StorageService storageService)
    {
        this.securityService = securityService;
        this.connectivityService = connectivityService;
        this.storageService = storageService;
        PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(Password)) ValidatePassword();
            if (e.PropertyName == nameof(UserName)) ValidateUserName();
        };

        IsBusy = false;

        UserName = Password = UserNameError = PasswordError = ServiceError = string.Empty;
    }


    [ObservableProperty]
    private string _userName;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private string _userNameError;

    [ObservableProperty]
    private string _passwordError;

    [ObservableProperty]
    private string _serviceError;

    [RelayCommand]
    public async Task LoginUserAsync()
    {
        try
        {
            IsBusy = true;
            ServiceError = string.Empty;

            await Validate();

            if (await HasErrors())
            {
                return;
            }

            var result = await securityService.Login(UserName, Password);

            if (result is null || !result.IsSuccess || result.Value is null)
            {
                Console.Error.WriteLine(result!.Error);
                ServiceError = result.Error;

                return;
            }

            UserName = Password = string.Empty;

            Application.Current.MainPage = new AppShell();
        }
        catch (Exception ex)
        {
            ServiceError = ex.Message;
            Console.Error.WriteLine(ex.Message);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task ValidateUserName()
    {
        await Task.Run(() =>
        {
            UserNameError = string.IsNullOrWhiteSpace(UserName) ? "Bitte geben Sie den Benutzernamen ein" : string.Empty;
        });


    }

    private async Task ValidatePassword()
    {
        await Task.Run(() =>
        {
            PasswordError = string.IsNullOrWhiteSpace(UserName) ? "Bitte geben Sie das Passwort ein" : string.Empty;
        });
    }

    private async Task Validate()
    {
        await ValidateUserName();
        await ValidatePassword();
    }

    private async Task<bool> HasErrors()
    {
        return await Task.Run(() =>
        {
            return string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password);
        });
    }
}
