using Microsoft.Data.SqlClient;
using RosterApp.Models;

namespace RosterApp.Views;

public partial class LoginPage : ContentPage
{
	DatabaseService databaseService; //Database Service Object
    DBFunctions dbFunctions; //Database Functions Object

    //COnstructor
	public LoginPage()
	{
		InitializeComponent(); //GUI Initialization Function
        databaseService = new DatabaseService(); //Instantiate Database Service
        dbFunctions = new DBFunctions(databaseService); //Instantiate Database Functions

        CreateAccountLabel.GestureRecognizers.Add(new TapGestureRecognizer
        {
            Command = new Command(OnCreateAccountTapped)
        });
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        string username = UsernameEntry.Text; //Username string is the value of the Username textbox
        string password = PasswordEntry.Text; //Password string is the value of the Password textbox

        /*
         * Perform Login Check
         * If successful, display message and navigate to next page.
         * if unsuccessful, display error message.
         */
        if(dbFunctions.Login(username, password))
        {
            await DisplayAlert("Alert", "You have successfully Logged In!", "OK");
            await Navigation.PushAsync(new HomePage(username));
        }
        else
        {
            await DisplayAlert("Error", "The Credentials you entered are incorrect", "OK");
            PasswordEntry.Text = "";
        }

		/* Debug
		 * using(SqlConnection connection = databaseService.ConnectToDB())
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

    private async void OnCreateAccountTapped()
    {
        await Navigation.PushAsync(new RegisterPage());
    }
}