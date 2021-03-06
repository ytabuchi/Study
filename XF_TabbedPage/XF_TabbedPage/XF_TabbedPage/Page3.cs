﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_TabbedPage
{
    public class Page3 : ContentPage
    {
        public Page3()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var label = new Label
            {
                Text = "Hello TabbedPage 3!",
                TextColor = Color.FromHex("#666666"),
                HorizontalTextAlignment = TextAlignment.Center
            };
            var button1 = new Button
            {
                Text = "GoTo Page3",
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Page3());
                })
            };
            var button2 = new Button
            {
                Text = "GoTo Page1",
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new Page1());
                })
            };

            Title = "TabPage 3";
            BackgroundColor = Color.FromHex("#eeeeff");

            Content = new StackLayout
            {
                Padding = 8,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children =
                    {
                        label,
                        button1,
                        button2
                    }
            };
        }
    }
}
