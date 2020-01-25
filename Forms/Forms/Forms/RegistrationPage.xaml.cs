using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forms.Api;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        public RegistrationPage()
        {
            InitializeComponent();

            Country.Items.Add("RU");
            Country.Items.Add("DE");
            Country.Items.Add("JP");

            Sex.Items.Add("Male");
            Sex.Items.Add("Female");
        }

        private async void cancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void CreateButton_OnClicked(object sender, EventArgs e)
        {
            if (Password.Text != PasswordConfirm.Text)
            {
                await DisplayAlert("Wrong password", "Пароли не совпадают", "Try again");
                Password.Text = "";
                PasswordConfirm.Text = "";
                return;
            }
            var nvc = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Login", Login.Text),
                new KeyValuePair<string, string>("Password", Password.Text),
                new KeyValuePair<string, string>("Mail", Mail.Text),
                new KeyValuePair<string, string>("Sex", Sex.SelectedItem.ToString()),
                new KeyValuePair<string, string>("Country", Country.SelectedItem.ToString()),
            };
            var result = await RestAPI.CreateAccountAsync(nvc);
            if (result.ErrorLevel == 0)
            {
                await DisplayAlert("Registration", result.Message, "Got it!");
                Application.Current.Properties["Token"] = result.Data.Token;
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error in registration", result.Message, "Okay :(");
            }
        }
    }
}