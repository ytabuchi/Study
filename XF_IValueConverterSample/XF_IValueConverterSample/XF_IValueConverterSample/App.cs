using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_IValueConverterSample
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new StartPage());
            nav.BarBackgroundColor = Color.FromHex ("3498db");
            nav.BarTextColor = Color.White;
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    class StartPage : ContentPage
    {
        public StartPage()
        {
            Title = "IValueConverter Sample";
            Padding = 15;
            Content = new StackLayout
            {
                Children = {
                    new Button {
                        Text = "w/ Editor binding",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.EditorConverterPage()))
                    },
                    new Button {
                        Text = "w/ ListView binding",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.ListViewConverterPage()))
                    }
                }
            };
        }
    }
}
