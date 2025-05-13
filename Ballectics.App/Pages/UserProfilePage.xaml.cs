using Ballectics.App.Services;
using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class UserProfilePage : ContentPage
{
    private readonly StorageService storageService;

    private PersonProfileViewModel personProfile { get; set; }
    public UserProfilePage(PersonProfileViewModel personProfileViewModel, StorageService storageService)
    {
        InitializeComponent();
        personProfile = personProfileViewModel;
        this.storageService = storageService;
        personProfile.IsBusy = true;
        SetBindingContext();
    }
    public async void SetBindingContext()
    {
        var user = await storageService.GetUserAsync();
        if (user is null)
        {
            personProfile.PersonInfo = new Models.PersonModel
            {
                Id = 0,
                FirstName = "Unbekannt",
                LastName = "Unbekannt",
            };
            return;
        }

        await personProfile.GetPersonDataAsync();
        personProfile.OnAppearing();

        this.BindingContext = personProfile;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        SetBindingContext();
    }
}