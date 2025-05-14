using Ballectics.App.Helper;
using Ballectics.App.Pages;

namespace Ballectics.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        BuildShell();
    }



    private async void BuildShell()
    {
        RegisterRout(nameof(LoginPage), typeof(LoginPage));
        RegisterRout(nameof(NoInternetPage), typeof(NoInternetPage));

        this.tabBar.Items.Clear();
        AddTab("Home", "home.png", typeof(HomePage), nameof(HomePage));
        AddTab("Kontakt", "contact.png", typeof(ContactPage), nameof(ContactPage));
        AddTab("About", "about.png", typeof(AboutPage), nameof(AboutPage));

        var user = await Storage.GetUserAsync();
        if (user is null)
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            return;
        }

        if (user.Role.ToLower().Equals("admin"))
        {

            AddTab("Kunden", "people.png", typeof(PersonPage), nameof(PersonPage));

            RegisterRout(nameof(PersonAddPage), typeof(PersonAddPage));

        }
        else if (user.Role.ToLower().Equals("user"))
        {
            AddTab("Profil", "profile.png", typeof(UserProfilePage), nameof(UserProfilePage));
        }

        AddTab("Ausloggen", "power_off.png", typeof(LogoutPage), nameof(LogoutPage));
    }

    private void AddTab(string title, string icon, Type pageType, string routeName)
    {
        //Routing.RegisterRoute(nameof(pageType).ToLowerInvariant(),pageType);

        var tabItem = new ShellContent
        {
            Title = title,
            Icon = icon,
            Route = routeName,
            ContentTemplate = new DataTemplate(pageType)
            //ContentTemplate = new DataTemplate(() => new NavigationPage((Page)Activator.CreateInstance(pageType)))
        };

        tabBar.Items.Add(tabItem);

        //RegisterRout(routeName, pageType);
    }

    private void RegisterRout(string routeName, Type pageType)
    {
        Routing.RegisterRoute(routeName, pageType);
    }

}
