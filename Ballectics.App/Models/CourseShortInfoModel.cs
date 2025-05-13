using Ballectics.App.Helper;

namespace Ballectics.App.Models;

public class CourseShortInfoModel
{
    public long PersonId { get; set; }
    public string Fullname { get; set; }
    public int Rebase { get; set; }

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

    public CourseShortInfoModel()
    {
        PersonId = 0;
        Fullname = "Unbekannte";
        Rebase = 0;
        Image = null;
    }
}


