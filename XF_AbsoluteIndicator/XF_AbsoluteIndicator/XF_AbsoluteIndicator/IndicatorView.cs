using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AbsoluteIndicator
{
    public class IndicatorView : ContentView
    {
        ActivityIndicator indicator;

        public IndicatorView()
        {
            indicator = new ActivityIndicator
            {
                IsRunning = true,
                Color = Color.White,
            };

            this.BackgroundColor = Color.Black;
            this.Opacity = 0.4d;

            Content = new StackLayout
            {
                Opacity = 1d,
                Padding = 20,
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    indicator,
                    new Label
                    {
                        Text = "Data Loading...",
                        TextColor = Color.White,
                    },
                }
            };
        }
    }
}
