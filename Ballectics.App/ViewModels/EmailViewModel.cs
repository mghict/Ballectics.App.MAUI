using System.ComponentModel;
using System.Text.RegularExpressions;
using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class EmailViewModel : BasePageViewModel
{

    // Constructor
    public EmailViewModel(EmailService emailService)
    {

        ClearModelAsync();
        this.emailService = emailService;
    }

    public EmailViewModel()
    {
    }


    //Properties

    #region Properties
    private readonly EmailService emailService;

    [ObservableProperty]
    private EmailModel _emailInfo;


    [ObservableProperty]
    private bool _isAcceptRules;

    [ObservableProperty]
    private string _emailError;

    [ObservableProperty]
    private string _fullNameError;

    [ObservableProperty]
    private string _messageError;


    #endregion

    // Command

    [RelayCommand]
    public async Task SendEmailAsync()
    {
        Console.WriteLine("SendEmail Called");
        ValidateAll();

        if (HasErrors())
            return;

        var result = await emailService.SendEmailAsync(EmailInfo, CancellationToken.None);
        if (result.IsSuccess)
        {
            await Shell.Current.DisplayAlert("Nachricht", "E-Mail erfolgreich gesendet. ✅", "OK");
            ClearModelAsync();
        }
        else
            await Shell.Current.DisplayAlert("Fehler", result.Error, "OK");

    }

    [RelayCommand]
    public async Task ChangeIsAcceptableRulesAsync(bool input)
    {
        await Task.Run(() =>
        {
            IsAcceptRules = input;
        });
    }

    [RelayCommand]
    public async Task ClearModelAsync()
    {
        EmailInfo = new EmailModel();
        FullNameError = MessageError = EmailError = string.Empty;
        IsAcceptRules = false;
    }

    // Validation

    #region Validation
    private void ValidateEmail()
    {
        if (string.IsNullOrWhiteSpace(EmailInfo.Email))
            EmailError = "E-Mail eingeben.";
        else if (!Regex.IsMatch(EmailInfo.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            EmailError = "E-Mail ist ungültig.";
        else
            EmailError = "";
    }

    private void ValidateFullName()
    {
        FullNameError = string.IsNullOrWhiteSpace(EmailInfo.FullName) ? "Geben Sie den Vorname ein." : "";
    }

    private void ValidateMessage()
    {
        MessageError = string.IsNullOrWhiteSpace(EmailInfo.Message) ? "Geben Sie den E-Mail-Text ein." : "";
    }

    private void ValidateAll()
    {
        ValidateFullName();
        ValidateEmail();
        ValidateMessage();
    }

    private bool HasErrors()
    {
        return string.IsNullOrEmpty(EmailInfo.FullName) || string.IsNullOrEmpty(EmailInfo.Email) || string.IsNullOrEmpty(EmailInfo.Message);
    }

    #endregion 

    partial void OnEmailInfoChanged(EmailModel oldValue, EmailModel newValue)
    {
        if (oldValue != null)
            oldValue.PropertyChanged -= EmailInfo_PropertyChanged;

        if (newValue != null)
            newValue.PropertyChanged += EmailInfo_PropertyChanged;
    }

    private void EmailInfo_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(EmailModel.Email):
                ValidateEmail();
                break;
            case nameof(EmailModel.FullName):
                ValidateFullName();
                break;
            case nameof(EmailModel.Message):
                ValidateMessage();
                break;
        }
    }
}