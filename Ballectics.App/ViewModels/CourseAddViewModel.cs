using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class CourseAddViewModel : BasePageViewModel
{
    [ObservableProperty]
    private CourseAddModel _courseInfo;

    private readonly CourseService courseService;

    public CourseAddViewModel(CourseService courseService)
    {
        _courseInfo = new CourseAddModel();
        this.courseService = courseService;
    }

    [RelayCommand]
    public async Task IncreaseAsync()
    {
        CourseInfo.IncreaseCount++;
        OnPropertyChanged(nameof(CourseInfo));
    }

    [RelayCommand]
    public async Task DecreaseAsync()
    {
        CourseInfo.IncreaseCount--;
        if (CourseInfo.IncreaseCount <= 0)
            CourseInfo.IncreaseCount = 1;
        OnPropertyChanged(nameof(CourseInfo));
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        try
        {
            IsBusy = true;
            if (this.CourseInfo is null)
                return;

            var result = new ResultModel();

            result = await courseService.IncreaseCourseAsync(this.CourseInfo);

            if (!result.IsSuccess)
                await Application.Current.MainPage.DisplayAlert("Fehler", result!.Error, "OK");
            else
            {
                await Application.Current.MainPage.DisplayAlert("Nachricht", "Vorgang erfolgreich abgeschlossen.", "OK");
            }

            OnPropertyChanged(nameof(CourseInfo));

            await CancelAsync();
        }
        finally
        {
            IsBusy = false;
        }
        //return;
    }

    [RelayCommand]
    public async Task CancelAsync()
    {
        await Application.Current.MainPage.Navigation.PopModalAsync();
    }
}