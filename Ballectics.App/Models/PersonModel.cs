using Ballectics.App.Helper;

namespace Ballectics.App.Models;

public class PersonModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Mobile { get; set; }
    public string FullName { get; set; }
    public long Rebase { get; set; }
    public byte[]? Image { get; set; }

    public ImageSource ImageSource
    {
        get
        {
            if (Image is not null && Image.Length > 0)
            {
                return Image.GetImageSource();
            }
            return "avatar.png";
        }
    }

    public Color TypeColor
    {
        get
        {
            return Rebase switch
            {
                > 5 => Colors.SeaGreen,
                < 3 => Colors.IndianRed,
                _ => Colors.Orange
            };
        }
    }
}
