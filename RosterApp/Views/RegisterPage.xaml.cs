using RosterApp.Models;

namespace RosterApp.Views;

public partial class RegisterPage : ContentPage
{
	DatabaseService _databaseService;
	DBFunctions _dbFunctions;
	string username;
	string firstname;
	string lastname;
	string email;
	string confirmemail;
	string password;
	string confirmpassword;

	public RegisterPage()
	{
		InitializeComponent();
		_databaseService = new DatabaseService();
		_dbFunctions = new DBFunctions(_databaseService);
	}

	private async void OnRegisterClicked(object sender, EventArgs e)
	{
		GetFormDetails();
		if(email == confirmemail && password == confirmpassword)
		{
			if(_dbFunctions.Register(username, firstname, lastname, email, password))
			{
				await DisplayAlert("Alert", "You have successfully Registered", "OK");
			}
		}
	}

	private async void OnResetClicked(object sender, EventArgs e)
	{
		ResetFields();
	}

	private async void OnCancelClicked(object sender, EventArgs e)
	{

	}

	private void GetFormDetails()
	{
        username = UsernameEntry.Text;
        firstname = FirstNameEntry.Text;
        lastname = LastNameEntry.Text;
        email = EmailEntry.Text;
        confirmemail = ConfirmEmailEntry.Text;
        password = PasswordEntry.Text;
        confirmpassword = ConfirmPasswordEntry.Text;
    }

	private void ResetFields()
	{
		UsernameEntry.Text = "";
		FirstNameEntry.Text = "";
		LastNameEntry.Text = "";
		EmailEntry.Text = "";
		ConfirmEmailEntry.Text = "";
		PasswordEntry.Text = "";
		ConfirmPasswordEntry.Text = "";
	}
}