using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class PersonProfilePage : ContentPage
{
    public PersonProfileViewModel personProfile { get; set; }
    public PersonProfilePage(PersonProfileViewModel personProfileViewModel)
    {
        InitializeComponent();
        personProfile = personProfileViewModel;
    }

    public void SetBindingContext(PersonProfileViewModel personProfileViewModel)
    {
        personProfile = personProfileViewModel;
        this.BindingContext = personProfileViewModel;
    }

    public void SetBindingContext()
    {
        this.BindingContext = personProfile;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((PersonProfileViewModel)this.BindingContext).OnAppearing();
    }
}