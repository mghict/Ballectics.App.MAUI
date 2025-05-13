using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class PersonAddViewModel : PersonBaseViewModel
{
    private readonly PersonService personService;



    public PersonAddViewModel(PersonService personService) : base(personService)
    {
        PersonInfo = new PersonModel();
        this.personService = personService;

    }

    [RelayCommand]
    public async Task GoBackAsync()
    {
        await BackToPersonPage();
    }

    [RelayCommand]
    public async Task SavePersonAsync()
    {
        try
        {
            IsBusy = true;

            if (this.PersonInfo is null)
                return;

            var result = new ResultModel();

            if (this.PersonInfo.Id == 0)
                result = await personService.SaveNewAsync(this.PersonInfo);
            else
                result = await personService.SaveEditAsync(this.PersonInfo);

            if (!result.IsSuccess)
                await Application.Current.MainPage.DisplayAlert("Fehler", result!.Error, "OK");
            else
            {
                await Application.Current.MainPage.DisplayAlert("Nachricht", "Person erfolgreich hinzugefügt.", "OK");
                await BackToPersonPage();
            }

            return;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    public async Task CancelPersonAsync()
    {
        await BackToPersonPage();
    }
    private async Task BackToPersonPage()
    {
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }

}