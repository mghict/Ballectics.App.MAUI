using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel model)
	{
		InitializeComponent();
        this.BindingContext = model;
        Shell.SetNavBarIsVisible(this, false);
        Shell.SetTabBarIsVisible(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
    }
}