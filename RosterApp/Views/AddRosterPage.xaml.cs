using RosterApp.Models;

namespace RosterApp.Views;

public partial class AddRosterPage : ContentPage
{
	string _username;
	DateTime RosterEntryDate;
	TimeSpan RosterEntryStartTime;
	TimeSpan RosterEntryEndTime;
	DatabaseService _databaseService;
	DBFunctions _dbFunctions;

	public AddRosterPage(string username)
	{
		InitializeComponent();
		_username = username;
		_databaseService = new DatabaseService();
		_dbFunctions = new DBFunctions(_databaseService);
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		GetFormDetails();
		if(_dbFunctions.AddToRoster(_username, RosterEntryDate, RosterEntryStartTime, RosterEntryEndTime))
		{
			await DisplayAlert("Alert", "Roster Entry Added", "OK");
		}
	}

	private async void OnResetClicked(object sender, EventArgs e)
	{

	}

	private void GetFormDetails()
	{
		RosterEntryDate = DatePicker.Date;
		RosterEntryStartTime = StartTimePicker.Time;
		RosterEntryEndTime = EndTimePicker.Time;
	}
}