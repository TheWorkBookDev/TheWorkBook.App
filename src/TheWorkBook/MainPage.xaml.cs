namespace TheWorkBook;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        GoBack.GoBackInTime().GetAwaiter().GetResult();
        return true;
    }
}
