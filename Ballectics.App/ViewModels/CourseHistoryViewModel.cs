using System.Collections.ObjectModel;
using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class CourseHistoryViewModel : BasePageViewModel
{
    private readonly CourseService courseService;
    public long PersonId { get; set; }

    public ObservableCollection<CourseHistoryModel> CourseHistoryItems { get; private set; }

    public CourseHistoryViewModel(CourseService courseService)
    {
        CourseHistoryItems = new ObservableCollection<CourseHistoryModel>();
        this.courseService = courseService;
    }

    public void OnAppearing()
    {
        IsBusy = true;
    }

    [RelayCommand]
    public async Task OnLoadDataAsync()
    {
        IsBusy = true;
        CourseHistoryItems.Clear();
        try
        {
            var result = await courseService.GetHistoryAsync(PersonId);
            if (result.IsSuccess && result.Value is not null)
            {
                foreach (var item in result.Value)
                {
                    CourseHistoryItems.Add(item);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}