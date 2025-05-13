using Ballectics.App.Models;
using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class PersonShowQRCodePage : ContentPage
{
    public PersonShowQRCodePage(PersonBaseViewModel personBaseViewModel)
    {
        InitializeComponent();

        this.BindingContext = personBaseViewModel;

    }


    public void SetBindingContext(PersonModel person)
    {
        ((PersonBaseViewModel)this.BindingContext).PersonInfo = person;
    }

}