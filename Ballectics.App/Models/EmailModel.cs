using CommunityToolkit.Mvvm.ComponentModel;

namespace Ballectics.App.Models
{
    public partial class EmailModel : ObservableObject
    {
        [ObservableProperty]
        private string _fullName;

        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _message;
    }
}
