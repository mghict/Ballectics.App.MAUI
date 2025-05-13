using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class PersonPage : ContentPage
{
    public PersonPage(PersonViewModel personViewModel)
    {
        InitializeComponent();
        this.BindingContext = personViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((PersonViewModel)this.BindingContext).OnAppearing();
    }

    private void searchBut_TextChanged(object sender, TextChangedEventArgs e)
    {
        ((PersonViewModel)this.BindingContext).OnAppearing();
    }
}