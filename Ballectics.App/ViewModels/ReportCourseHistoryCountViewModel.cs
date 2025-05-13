using System.Collections.ObjectModel;
using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class ReportCourseHistoryCountViewModel : BasePageViewModel
{
    protected readonly PersonService personService;

    [ObservableProperty]
    private int _selectedCount;
    public ObservableCollection<PersonModel> DataList { get; set; }

    public ReportCourseHistoryCountViewModel(PersonService personService)
    {
        this.personService = personService;
        DataList = new ObservableCollection<PersonModel>();
        IsBusy = false;
    }

    public async void OnAppearing()
    {
        IsBusy = true;
        LoadDataAsync();
    }

    [RelayCommand]
    public async Task LoadDataAsync()
    {
        try
        {
            IsBusy = true;
            DataList.Clear();
            var data = await personService.GetAllAsync(count: SelectedCount);

            if (data.IsSuccess && data.Value is not null)
            {
                foreach (var item in data.Value.OrderBy(p => p.Rebase).ToList())
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
}