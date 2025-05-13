namespace Ballectics.App.Helper;

public class MainAlert
{
    public static async Task DisplayAlert(string title, string message, string cancel)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Shell.Current.DisplayAlert(title, message, cancel);
        });
    }
    public static async Task DisplayAlert(string title, string message, string cancel, string accept)
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            Shell.Current.DisplayAlert(title, message, cancel, accept);
        });
    }


}
