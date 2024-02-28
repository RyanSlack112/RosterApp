using Microsoft.Data.SqlClient;
using RosterApp.Models;

namespace RosterApp.Views;

public partial class LoginPage : ContentPage
{
	DatabaseService databaseService;
    DBFunctions dbFunctions;

	public LoginPage()
	{
		InitializeComponent();
        databaseService = new DatabaseService();
        dbFunctions = new DBFunctions(databaseService);
	}

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text;
        string password = PasswordEntry.Text;

        if(dbFunctions.Login(username, password))
        {
            await DisplayAlert("Alert", "You have successfully Logged In!", "OK");
            await Navigation.PushAsync(new HomePage());
        }
        else
        {
            await DisplayAlert("Error", "The Credentials you entered are incorrect", "OK");
            PasswordEntry.Text = "";
        }

		/*using(SqlConnection connection = databaseService.ConnectToDB())
		{
            if (databaseService.CheckDatabaseConnection(connection))
            {
                await DisplayAlert("Alert", "The Connection Was Successful", "OK");
            }
            else
            {
                await DisplayAlert("Alert", "The Connection Was Unsuccessful", "OK");
            }
        }*/
    }
}