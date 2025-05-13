namespace Ballectics.App.Views;

public partial class HeaderIcon : ContentView
{
	public HeaderIcon()
	{
		InitializeComponent();
	}

    private async void btnYouTube_Clicked(object sender, EventArgs e)
    {
        var url = "https://www.youtube.com/@balleticsbynina67";
        await Launcher.Default.OpenAsync(url);
    }

    private async void btnFacebook_Clicked(object sender, EventArgs e)
    {
        var url = "https://www.facebook.com/nina.lettow";
        await Launcher.Default.OpenAsync(url);
    }

    private async void btnInstagram_Clicked(object sender, EventArgs e)
    {
        var url = "https://www.instagram.com/balletics.by.nina/?hl=de";
        await Launcher.Default.OpenAsync(url);
    }
}