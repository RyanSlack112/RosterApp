using RosterApp.Models;

namespace RosterApp.Views;

public partial class ViewRosterPage : ContentPage
{
	DatabaseService _databaseService;
    DBFunctions _dbFunctions;
	string _username;

	public ViewRosterPage(string username)
	{
		InitializeComponent();
        _databaseService = new DatabaseService();
        _dbFunctions = new DBFunctions(_databaseService);
        _username = username;
	}

	private async void OnPopulateClicked(object sender, EventArgs e)
	{
        List<RosterData> rosterDataList = _dbFunctions.GetRosterEntries(_username, datePicker.Date);
		rosterCollectionView.ItemsSource = rosterDataList;
	}
}