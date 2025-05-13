using Ballectics.App.Helper;
using Ballectics.App.Models;
using Ballectics.App.Pages;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class PersonProfileViewModel : PersonBaseViewModel
{
    [ObservableProperty]
    private CourseModel _courseInfo;




    private readonly CourseService courseService;
    private readonly PersonService personService;
    private readonly StorageService storageService;
    private readonly CourseAddPage courseAddPage;
    private readonly CourseHistoryPage courseHistoryPage;
    private readonly PersonShowQRCodePage personShowQRCodePage;

    public PersonProfileViewModel(CourseService courseService,
                                  PersonService personService,
                                  StorageService storageService,
                                  CourseAddPage courseAddPage,
                                  CourseHistoryPage courseHistoryPage,
                                  PersonShowQRCodePage personShowQRCodePage)
        : base(personService)
    {
        this.courseService = courseService;
        this.personService = personService;
        this.storageService = storageService;
        this.courseAddPage = courseAddPage;
        this.courseHistoryPage = courseHistoryPage;
        this.personShowQRCodePage = personShowQRCodePage;
        IsBusy = false;
    }


    [RelayCommand]
    public async Task SelectImageAsync()
    {
        var status = await Permissions.RequestAsync<Permissions.Photos>();
        if (status != PermissionStatus.Granted)
        {
            return;
        }

        status = await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            return;
        }

        var user = await storageService.GetUserAsync();
        if (user is null || !user.Role.ToLower().Equals("admin"))
        {
            return;
        }

        var result = await Shell.Current.DisplayActionSheet("Foto auswählen", "Stornierung", null, "Kamera", "Galerie");
        FileResult photo = null;

        try
        {
            if (result == "Galerie")
            {
                photo = await MediaPicker.PickPhotoAsync();
            }
            else if (result == "Kamera")
            {
                photo = await MediaPicker.CapturePhotoAsync();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler", ex.Message, "Ok");
            return;
        }

        if (photo == null)
            return;

        var stream = await photo.OpenReadAsync();
        PersonInfo.Image = await stream.ConvertStreamToByteArray();

        try
        {
            IsBusy = true;
            await personService.UploadImage(PersonInfo.Image, PersonInfo.Id);
        }
        finally
        {
            IsBusy = false;
        }

        OnPropertyChanged(nameof(PersonInfo));

    }

    [RelayCommand]
    public async Task AddCourseAsync()
    {
        courseAddPage.SetBindingContext(PersonInfo.Id);
        await Application.Current.MainPage.Navigation.PushModalAsync(courseAddPage);
    }

    [RelayCommand]
    public async Task<bool> VisibleAddCourseAsync()
    {
        var user = await storageService.GetUserAsync();
        if (user is null || !user.Role.ToLower().Equals("admin"))
        {
            return false;
        }

        return true;
    }

    [RelayCommand]
    public async Task ShowHistoryAsync()
    {
        var user = await storageService.GetUserAsync();
        if (user is null)
        {
            return;
        }

        courseHistoryPage.SetBindingContext(PersonInfo.Id);
        await Application.Current.MainPage.Navigation.PushModalAsync(courseHistoryPage);
    }

    [RelayCommand]
    public async Task OnLoadCourseDataAsync()
    {
        var user = await storageService.GetUserAsync();
        if (user is null)
        {
            return;
        }

        await FetchCourse();
    }

    [RelayCommand]
    public async Task ShowQRCodeAsync()
    {
        if (PersonInfo == null)
            return;

        personShowQRCodePage.SetBindingContext(PersonInfo);
        await Application.Current.MainPage.Navigation.PushModalAsync(personShowQRCodePage);
    }

    public async void OnAppearing()
    {
        IsBusy = true;
        await OnLoadCourseDataAsync();
    }

    private async Task FetchCourse()
    {
        try
        {
            IsBusy = true;
            if (PersonInfo == null)
                return;

            var result = await courseService.GetCourseAsync(PersonInfo.Id);
            if (result.IsSuccess && result.Value is not null)
            {
                CourseInfo = result.Value;
            }
            else
            {
                CourseInfo = new CourseModel();
            }

            OnPropertyChanged(nameof(CourseInfo));
        }
        finally
        {
            IsBusy = false;
        }

    }


}