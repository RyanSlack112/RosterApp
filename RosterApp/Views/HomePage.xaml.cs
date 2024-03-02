namespace RosterApp.Views;

public partial class HomePage : ContentPage
{
	string _username;

	public HomePage(string username)
	{
        InitializeComponent();
		_username = username;
	}

	/*
	 * Button Click Event for AddRosterButton
	 * On Click, Navigate to the Add Roster Page
	 */
    private async void OnAddRosterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddRosterPage(_username));
	}

	/*
	 * Button Click Event for ViewRosterButton
	 * On Click, Navigate to the View Roster Page.
	 */
	private async void OnViewRosterClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new ViewRosterPage());
	}
}