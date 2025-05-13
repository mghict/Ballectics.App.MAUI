using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class CourseHistoryPage : ContentPage
{
    public CourseHistoryPage(CourseHistoryViewModel courseHistoryViewModel)
    {
        InitializeComponent();
        this.BindingContext = courseHistoryViewModel;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((CourseHistoryViewModel)this.BindingContext).OnAppearing();
    }

    public void SetBindingContext(long personId)
    {
        ((CourseHistoryViewModel)this.BindingContext).PersonId = personId;
    }
}