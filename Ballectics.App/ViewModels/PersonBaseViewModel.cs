using Ballectics.App.Models;
using Ballectics.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Ballectics.App.ViewModels;

public partial class PersonBaseViewModel : BasePageViewModel
{
    [ObservableProperty]
    private PersonModel _personInfo;


    private readonly PersonService personService;

    public PersonBaseViewModel(PersonService personService)
    {
        this.personService = personService;
    }

    [RelayCommand]
    public async Task<string> GetQrCodeTextAsync()
    {
        var encryptedText = $"{PersonInfo.Id}"; //Helper.Encriptor.Encrypt($"{PersonInfo.Id}");
        return encryptedText;
    }

    [RelayCommand]
    public async Task ShareQRCodeAsync()
    {
        var filePath = await SaveBarcodeToFileAsync();

        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = "https://balletics.com",
            File = new ShareFile(filePath)
        });
    }

    private async Task<byte[]> CreateQRCodeAsync()
    {
        var encryptedText = await GetQrCodeTextAsync();
        return Helper.Utility.GenerateQrCodeByte(encryptedText);

    }

    private async Task<string> SaveBarcodeToFileAsync()
    {
        var imageBytes = await CreateQRCodeAsync();
        var fileName = Path.Combine(FileSystem.CacheDirectory, $"barcode{PersonInfo.FullName}.png");
        File.WriteAllBytes(fileName, imageBytes);
        return fileName;
    }

    public async Task GetPersonDataAsync()
    {

        try
        {
            IsBusy = true;

            var person = await personService.GetPersonAsync();

            if (person.IsSuccess)
            {
                PersonInfo = person.Value;
            }
            else
            {
                await Shell.Current.DisplayAlert("Fehler", person.Error, "OK");
            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Fehler", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }



}
