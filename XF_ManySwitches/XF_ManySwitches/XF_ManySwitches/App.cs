using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_ManySwitches
{
    public class App : Application
    {
        public App()
        {
            List<ContentPage> pgs = new List<ContentPage>();
            pgs.Add(new SwitchPageCS());
            pgs.Add(new SwitchPageXaml());
            var pg = new CarouselPage
            {
                Title = "CS,Xaml",
                Children = {
                    pgs[0],pgs[1],
                }
            };
            MainPage = pg;
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
