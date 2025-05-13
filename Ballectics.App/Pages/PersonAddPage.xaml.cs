using Ballectics.App.Models;
using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class PersonAddPage : ContentPage
{
    public PersonAddPage(PersonAddViewModel personAddViewModel)
    {
        InitializeComponent();
        BindingContext = personAddViewModel;
    }

    public void AddPersonModel(PersonModel person)
    {
        if (person is not null)
        {
            ((PersonAddViewModel)BindingContext).PersonInfo = person;
            txtTitle.Text = "Bearbeiten Kunde";
        }
    }
}