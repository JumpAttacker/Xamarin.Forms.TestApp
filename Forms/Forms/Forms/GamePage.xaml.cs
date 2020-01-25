using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        public GamePage()
        {
            InitializeComponent();
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Remove("Token");
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}