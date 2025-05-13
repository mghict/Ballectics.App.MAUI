using Ballectics.App.Helper;

namespace Ballectics.App.Models;

public class CourseHistoryWithImageModel : CourseHistoryModel
{
    public string FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
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
}


