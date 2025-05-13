using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class ContactPage : ContentPage
{
    public ContactPage(EmailViewModel model)
    {
        InitializeComponent();
        this.BindingContext = model;
    }

    private async void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        await ((EmailViewModel)this.BindingContext).ChangeIsAcceptableRulesAsync(e.Value);
    }
}