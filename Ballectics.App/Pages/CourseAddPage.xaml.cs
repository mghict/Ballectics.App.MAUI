using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class CourseAddPage : ContentPage
{
    public CourseAddPage(CourseAddViewModel courseAddViewModel)
    {
        InitializeComponent();
        this.BindingContext = courseAddViewModel;
    }

    public void SetBindingContext(long personId)
    {
        ((CourseAddViewModel)this.BindingContext).CourseInfo.PersonId = personId;
    }
}