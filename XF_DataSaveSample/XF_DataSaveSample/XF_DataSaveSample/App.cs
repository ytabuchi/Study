using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using XF_DataSaveSample.Views;

namespace XF_DataSaveSample
{
    public class App : Application
    {
        public App()
        {
            var nav = new NavigationPage(new StartPage());
            nav.BarBackgroundColor = Color.FromHex("3498DB");
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

    public class StartPage : ContentPage
    {
        public StartPage()
        {
            Title = "DataSave Sample";
            Padding = 16;
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 10,
                    Children = {
                        new Button { Text = "Save to dictionay (C#)", 
                            Command = new Command(async ()=> await Navigation.PushAsync(new SaveToDictionaryCS()))
                        },
                        new Button { Text = "Save to dic by ViewModel (C#)",
                            Command = new Command(async () => await Navigation.PushAsync(new SaveByVMCS()))
                        },
                        new Button { Text = "Save to dic by ViewModel (Xaml)",
                            Command = new Command(async () => await Navigation.PushAsync(new SaveByVMXaml()))
                        },
                        new Button { Text = "Save to json (C#)",
                            Command = new Command(async () => await Navigation.PushAsync(new SaveToJsonCS()))
                        },
                        new Button { Text = "Save to json (Xaml)",
                            Command = new Command(async () => await Navigation.PushAsync(new SaveToJsonXaml()))
                        },
                    }
                }
            };
        }
    }

}
