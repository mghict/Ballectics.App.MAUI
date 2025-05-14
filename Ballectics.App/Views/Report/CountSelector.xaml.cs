namespace Ballectics.App.Views.Report;

public partial class CountSelector : ContentView
{
    public event EventHandler<int> CountSelected;
    public CountSelector()
    {
        InitializeComponent();
        InitCountPicker();
    }

    private void InitCountPicker()
    {
        for (int i = 0; i < 11; i++)
        {
            CountPicker.Items.Add($"{i:00}");
        }

        CountPicker.SelectedIndex = 2;
    }

    private void OnSubmitClicked(object sender, EventArgs e)
    {
        var finalDateTime = Convert.ToInt32(CountPicker.SelectedItem);
        CountSelected?.Invoke(this, finalDateTime);
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}