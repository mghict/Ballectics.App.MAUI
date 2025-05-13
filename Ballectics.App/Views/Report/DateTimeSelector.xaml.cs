namespace Ballectics.App.Views.Report;

public partial class DateTimeSelector : ContentView
{
    public event EventHandler<DateTime> DateTimeSelected;
    public DateTimeSelector()
    {
        InitializeComponent();
        InitHourPicker();
    }

    private void InitHourPicker()
    {
        for (int hour = 8; hour < 24; hour++)
        {
            HourPicker.Items.Add($"{hour:00}:00");
        }

        var currentTime = $"{DateTime.Now.Hour:00}:00";

        var index = HourPicker.Items.IndexOf(currentTime);
        HourPicker.SelectedIndex = index >= 0 ? index : 0;
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        var selectedDate = DatePicker.Date;
        var selectedHour = HourPicker.SelectedItem as string;

        var finalDateTime = selectedDate.Date + new TimeSpan(Convert.ToInt32(selectedHour!.Split(':')[0]), 0, 0);
        //PreviewLabel.Text = $"Ausgewähltes Datum: {finalDateTime:yyyy-MM-dd HH}:00";

        DateTimeSelected?.Invoke(this, finalDateTime);
    }
}