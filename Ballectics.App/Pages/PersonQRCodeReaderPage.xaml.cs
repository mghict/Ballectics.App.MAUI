using Ballectics.App.ViewModels;

namespace Ballectics.App.Pages;

public partial class PersonQRCodeReaderPage : ContentPage
{
    public PersonQRCodeReaderPage(CourseDecreaseViewModel courseDecreaseViewModel)
    {
        InitializeComponent();
        this.BindingContext = courseDecreaseViewModel;
    }

    protected async override void OnAppearing()
    {
        var status =  await Permissions.RequestAsync<Permissions.Camera>();
        if (status != PermissionStatus.Granted)
        {
            await Shell.Current.DisplayAlert("Fehler", "Die Kamera Berechtigung wurde nicht erteilt.", "OK");
            return;
        }

        base.OnAppearing();
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            cameraBarcodeReaderView.IsDetecting = true;
            FrameContent.IsVisible = false;
        });

    }
    protected override void OnSizeAllocated(double width, double height)
    {
        ProcessShow();

        base.OnSizeAllocated(width, height);
        if (width > height)
        {
            LayoutMain.Orientation = StackOrientation.Horizontal;
            FrameQrCode.VerticalOptions = FrameContent.VerticalOptions = LayoutOptions.Start;

        }
        else
        {
            LayoutMain.Orientation = StackOrientation.Vertical;
            FrameQrCode.VerticalOptions = FrameContent.VerticalOptions = LayoutOptions.Center;

        }

        ProcessHide();
    }

    private async void cameraBarcodeReaderView_BarcodesDetected(object sender, ZXing.Net.Maui.BarcodeDetectionEventArgs e)
    {
        ProcessShow();

        cameraBarcodeReaderView.IsDetecting = false;
        cameraBarcodeReaderView.IsEnabled = false;

        var scannedText = e.Results[0].Value;

        if (string.IsNullOrWhiteSpace(scannedText))
            return;

        long personId = 0;
        try
        {
            personId = long.Parse(scannedText);
        }
        catch (Exception ex)
        {

            System.Diagnostics.Debug.WriteLine("Decryption or Parsing failed: " + ex.Message);
            await ShowErrorAsync();
            return;

        }


        if (personId <= 0)
        {
            await ShowErrorAsync();
            return;
        }

        await ((CourseDecreaseViewModel)this.BindingContext).GetCourseShortInfoAsync(personId);
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            FrameContent.IsVisible = true;
        });

        ProcessHide();
    }

    private void NewPersonButton_Clicked(object sender, EventArgs e)
    {
        SetRenew();
    }

    private async void SetRenew()
    {
        ProcessHide();
        cameraBarcodeReaderView.IsDetecting = true;
        cameraBarcodeReaderView.IsEnabled = true;
        ((CourseDecreaseViewModel)this.BindingContext).SetModel();

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            FrameContent.IsVisible = false;
        });
    }

    private async Task ShowErrorAsync()
    {
        try
        {
            ProcessHide();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Shell.Current.DisplayAlert("Fehler", "Der QR-Code ist ungültig.", "OK");
            });
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error showing alert: " + ex.Message);
        }

        SetRenew();
    }

    private void btnNew_Clicked(object sender, EventArgs e)
    {
        SetRenew();
    }

    private async void ProcessShow()
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            LoaderOverlay.IsVisible = true;
        });
    }
    private async void ProcessHide()
    {
        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            LoaderOverlay.IsVisible = false;
        });
    }
}