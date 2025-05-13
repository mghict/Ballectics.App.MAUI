using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class ReportCourseByDateTimePage : ContentPage
{
    public ReportCourseByDateTimePage(ReportCourseHistortDateTimeViewModel model)
    {
        InitializeComponent();
        model.SelectedDateTime = DateTime.Now;
        this.BindingContext = model;
    }

    private void MyDateTimeSelector_DateTimeSelected(object sender, DateTime e)
    {
        if (BindingContext is ReportCourseHistortDateTimeViewModel model)
        {
            model.SelectedDateTime = e;
            model.OnAppearing();
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((ReportCourseHistortDateTimeViewModel)this.BindingContext).OnAppearing();

    }
}
