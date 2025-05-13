using System.Collections.ObjectModel;
using Ballectics.App.Models;
using Ballectics.App.Pages;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class PersonViewModel : PersonBaseViewModel
{
    private readonly PersonService personService;
    private readonly IServiceProvider serviceProvider;
    private readonly PersonQRCodeReaderPage personQRCodeReaderPage;
    private readonly PersonProfilePage personProfilePage;
    private PersonAddPage personAddPage;
    private readonly ReportCourseByDateTimePage reportCourseByDateTimePage;
    private readonly ReportCourseCountPage reportCourseHistoryCount;

    public ObservableCollection<PersonModel> PersonList { get; private set; }

    [ObservableProperty]
    private string _searchText = string.Empty;

    public PersonViewModel(IServiceProvider serviceProvider,
                           PersonService personService,
                           PersonQRCodeReaderPage personQRCodeReaderPage,
                           PersonProfilePage personProfilePage,
                           PersonAddPage personAddPage,
                           ReportCourseByDateTimePage reportCourseByDateTimePage,
                           ReportCourseCountPage reportCourseHistoryCount)
        : base(personService)
    {
        this.personService = personService;
        this.serviceProvider = serviceProvider;
        this.personQRCodeReaderPage = personQRCodeReaderPage;
        this.personProfilePage = personProfilePage;
        this.personAddPage = personAddPage;
        this.reportCourseByDateTimePage = reportCourseByDateTimePage;
        this.reportCourseHistoryCount = reportCourseHistoryCount;

        PersonList = new ObservableCollection<PersonModel>();

    }

    [RelayCommand]
    public async Task AddPersonAsync()
    {
        personAddPage = serviceProvider.GetRequiredService<PersonAddPage>();
        await Application.Current.MainPage.Navigation.PushModalAsync(personAddPage);
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    public async Task OnLoadDataAsync()
    {

        IsBusy = true;

        PersonList.Clear();
        try
        {
            var result = await personService.GetAllAsync(SearchText);

            if (result.IsSuccess && result.Value is not null)
            {
                foreach (var item in result.Value)
                {
                    PersonList.Add(item);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", result.Error, "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }

    }

    [RelayCommand]
    public async Task PersonTappedDeleteAsync(PersonModel person)
    {
        if (person == null)
            return;
        var answer = await Shell.Current.DisplayAlert("Genehmigung", "Möchten Sie die Informationen löschen?", "Ja", "Nein");
        if (answer)
        {
            var result = await personService.DeleteAsync(person);
            if (result.IsSuccess)
            {
                OnAppearing();
            }
            else
            {
                await Shell.Current.DisplayAlert("Fehler", result.Error, "OK");
            }
        }
    }


    [RelayCommand]
    public async Task PersonTappedEditAsync(PersonModel person)
    {
        if (person == null) return;

        personAddPage.AddPersonModel(person);
        await Application.Current.MainPage.Navigation.PushModalAsync(personAddPage);

        //await OnLoadData();
        OnAppearing();
    }

    [RelayCommand]
    public async Task PersonTappedProfileAsync(PersonModel person)
    {
        if (person == null) return;

        personProfilePage.personProfile.PersonInfo = person;
        personProfilePage.SetBindingContext();
        await Application.Current.MainPage.Navigation.PushModalAsync(personProfilePage);

    }

    [RelayCommand]
    public async Task ScanQrCodeAsync()
    {
        await Application.Current.MainPage.Navigation.PushModalAsync(personQRCodeReaderPage);

    }

    [RelayCommand]
    public async Task SearchAsync()
    {
        IsBusy = true;
    }

    [RelayCommand]
    public async Task GotoReportHistoryAsync()
    {
        await Application.Current.MainPage.Navigation.PushModalAsync(reportCourseByDateTimePage);
    }

    [RelayCommand]
    public async Task GotoReportHistoryCountAsync()
    {
        await Application.Current.MainPage.Navigation.PushModalAsync(reportCourseHistoryCount);
    }
}
