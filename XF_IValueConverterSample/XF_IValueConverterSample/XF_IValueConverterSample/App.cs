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
                    new Label { Text = "Use IValueConverter in view to view binding page (Xaml)" },
                    new Button {
                        Text = "ViewToView page (Xaml)",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.XamlV2VConverterPage()))
                    },
                    new Label { Text = "Use IValueConverter in ViewModel page (Xaml)" },
                    new Button {
                        Text = "ViewModel Page (Xaml)",
                        Command = new Command(async () =>
                            await Navigation.PushAsync(new View.XamlViewModelConverterPage()))
                    },
                    new Label { Text ="Use IValueConverter in view to view binding page (C#)"},
                    new Button {
                        Text = "ViewToView page (C#)",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.CSV2VConverterPage()))
                    },
                    new Label { Text ="Use IValueConverter in ViewModel page (C#)"},
                    new Button {
                        Text = "ViewModel Page (C#)",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.CSViewModelConverterPage()))
                    },

                    new Label { Text = "Use IValueConverter in ListView page (おまけ)"},
                    new Button {
                        Text = "ListView binding",
                        Command = new Command(async ()=> 
                            await Navigation.PushAsync(new View.ListViewConverterPage()))
                    }
                }
            };
        }
    }
}
