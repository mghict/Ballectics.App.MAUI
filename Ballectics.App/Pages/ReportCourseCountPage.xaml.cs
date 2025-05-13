using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class ReportCourseCountPage : ContentPage
{
    public ReportCourseCountPage(ReportCourseHistoryCountViewModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
    }

    private void countSelector_CountSelected(object sender, int e)
    {
        if (BindingContext is ReportCourseHistoryCountViewModel model)
        {
            model.SelectedCount = e;
            model.OnAppearing();
        }
    }
}