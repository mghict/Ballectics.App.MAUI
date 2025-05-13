using System.Collections.ObjectModel;
using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class ReportCourseHistortDateTimeViewModel : BasePageViewModel
{
    protected readonly ReportService reportService;

    [ObservableProperty]
    private DateTime _selectedDateTime;

    public ObservableCollection<CourseHistoryWithImageModel> DataList { get; set; }


    public ReportCourseHistortDateTimeViewModel(ReportService reportService)
    {
        this.reportService = reportService;
        DataList = new ObservableCollection<CourseHistoryWithImageModel>();
        IsBusy = false;
    }


    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            DataList.Clear();
            var data = await reportService.GetCourseHistoryWithImageAsync(SelectedDateTime);

            if (data.IsSuccess && data.Value is not null)
            {
                foreach (var item in data.Value)
                {
                    DataList.Add(item);
                }
            }
        }
        finally
        {
            IsBusy = false;
        }
    }

    public async void OnAppearing()
    {
        IsBusy = true;
        LoadDataAsync();
    }
}