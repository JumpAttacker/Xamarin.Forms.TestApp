using System;
using System.ComponentModel;
using Forms.Api;
using Xamarin.Forms;

namespace Forms
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public bool IsLogged = false;

        public MainPage()
        {
            InitializeComponent();
            var connected = RestAPI.Init();
            Console.WriteLine($"Connected: {connected}");
            LoginButton.Clicked += async (sender, args) =>
            {
                var result = await RestAPI.LoginAsync(LoginEntry.Text, PasswordEntry.Text);
                LoginResultCheck(result);
            };
        }

        private async void LoginResultCheck(RegistrationObject result)
        {
            if (result.ErrorLevel == 0)
            {
                //await DisplayAlert("Login", result.message, "Got it!");
                Application.Current.Properties["Token"] = result.Data.Token;
                IsLogged = true;
                //Application.Current.MainPage = new NavigationPage(new GamePage(result));
                Application.Current.MainPage = new GamePage();
            }
            else
            {
                IsLogged = false;
                await DisplayAlert("Error in auth", result.Message, "Okay :(");
            }
        }

        private async void navButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage());
        }

        private async void regButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new RegistrationPage());
        }

        protected override async void OnAppearing()
        {
            IsLogged = false;
            if (Application.Current.Properties.TryGetValue("Token", out var token))
            {
                var result = await RestAPI.LoginAsyncWithToken(token);
                LoginResultCheck(result);
            }
            base.OnAppearing();
        }
    }
}
