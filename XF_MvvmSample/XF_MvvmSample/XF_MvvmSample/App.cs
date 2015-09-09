using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_MvvmSample
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            MainPage = new NavigationPage(new StartPage());
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
            Title = "Mvvm Sample";
            Padding = 15;
            Content = new StackLayout
            {
                Children = {
                    new Button {
                        Text = "Xaml",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.XamlPage()))
                    },
                    new Button {
                        Text = "C#",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.CSPage()))
                    },
                }
            };
        }
    }
}
