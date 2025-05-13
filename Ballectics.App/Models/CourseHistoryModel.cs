namespace Ballectics.App.Models;

public class CourseHistoryModel
{
    public long Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string Description { get; set; }
    public HistoryType Type { get; set; }

    public Color TypeColor
    {
        get
        {
            return Type switch
            {
                HistoryType.Increase => Colors.SeaGreen,
                HistoryType.Decrease => Colors.IndianRed,
                HistoryType.Change => Colors.Orange,
                _ => Colors.Gray
            };
        }
    }

    public Color TypeTextColor
    {
        get
        {
            return Type switch
            {
                HistoryType.Increase => Colors.White,
                HistoryType.Decrease => Colors.White,
                HistoryType.Change => Colors.White,
                _ => Colors.Gray
            };
        }
    }
}


