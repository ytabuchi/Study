using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace XF_Map
{
    public class App : Application
    {
        public App()
        {
            var map = new Map(MapSpan.FromCenterAndRadius(new Position(35.686203, 139.752757), Distance.FromKilometers(5)));
            MainPage = new ContentPage
            {
                Content = map,
            };
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
}
