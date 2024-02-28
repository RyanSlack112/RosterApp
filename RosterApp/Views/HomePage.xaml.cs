namespace RosterApp.Views;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
        InitializeComponent();
	}

    private async void OnAddRosterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddRosterPage());
	}

	private async void OnViewRosterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ViewRosterPage());
	}
}