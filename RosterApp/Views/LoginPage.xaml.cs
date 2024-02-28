using Microsoft.Data.SqlClient;
using RosterApp.Models;

namespace RosterApp.Views;

public partial class LoginPage : ContentPage
{
	DatabaseService databaseService;
	SqlConnection connection;

	public LoginPage()
	{
		InitializeComponent();
		databaseService = new DatabaseService();
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
		using(SqlConnection connection = databaseService.ConnectToDB())
		{
            if (databaseService.CheckDatabaseConnection(connection))
            {
                await DisplayAlert("Alert", "The Connection Was Successful", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "The Connection Was Unsuccessful", "OK");
            }
        }
    }
}