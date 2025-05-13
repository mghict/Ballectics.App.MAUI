using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class CourseDecreaseViewModel : BasePageViewModel
{
    private readonly CourseService courseService;
    private long personId { get; set; } = 0;

    [ObservableProperty]
    private CourseShortInfoModel _courseShortInfo;


    public CourseDecreaseViewModel(CourseService courseService)
    {
        this.courseService = courseService;
        SetModel();
    }

    [RelayCommand]
    public async Task DecreaseCourseAsync()
    {
        var result = await courseService.DecreaseCourseAsync(new CourseDecreaseModel() { PersonId = CourseShortInfo.PersonId });
        //var result=await courseService.DecreaseCourseAsync(new CourseDecreaseModel() { PersonId=6});

        if (!result.IsSuccess)
        {
            await Shell.Current.DisplayAlert("Fehler", result!.Error, "OK");
            return;
        }
        await GetCourseShortInfoAsync(CourseShortInfo.PersonId);
        await Shell.Current.DisplayAlert("✅ Nachrichten", "Der Vorgang war erfolgreich.", "OK");

        SetModel();

        //await Application.Current.MainPage.Navigation.PopModalAsync();
    }

    [RelayCommand]
    public async Task GetCourseShortInfoAsync(long id)
    {
        personId = id;

        var result = await courseService.GetCourseShortInfoAsync(personId);

        if (!result.IsSuccess)
        {
            await Application.Current.MainPage.DisplayAlert("Fehler", result!.Error, "OK");
            return;
        }

        CourseShortInfo = result!.Value!;
    }

    [RelayCommand]
    public async Task GoBackAsync()
    {
        await Shell.Current.Navigation.PopModalAsync();
    }

    public void SetModel()
    {
        CourseShortInfo = new CourseShortInfoModel()
        {
            PersonId = 0,
            Fullname = "Unbekannt",
            Rebase = 0
        };
    }

    public bool IsValid()
    {
        if (CourseShortInfo is null)
        {
            return false;
        }
        if (CourseShortInfo.PersonId <= 0)
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(CourseShortInfo.Fullname))
        {
            return false;
        }
        if (CourseShortInfo.Rebase <= 0)
        {
            return false;
        }

        return true;
    }
}