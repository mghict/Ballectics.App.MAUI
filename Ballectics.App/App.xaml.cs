using Ballectics.App.Pages;
using Ballectics.App.ViewModels;

namespace Ballectics.App
{
    public partial class App : Application
    {
        public App(LoginPageViewModel loginPageViewModel)
        {
            InitializeComponent();

            MainPage = new LoginPage(loginPageViewModel);
        }
    }
}
