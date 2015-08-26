using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AbsoluteIndicator
{
    public class BaseView : ContentView
    {
        public BaseView()
        {
            Content = new StackLayout
            {
                Padding = 20,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = {
                    new Label
                    {
                        Text = "Hello ContentPage",
                        TextColor = Color.Black,
                        XAlign = TextAlignment.Center,
                    },
                    new Button
                    {
                        Text = "Next Page",
                        Command = new Command(async () => await Navigation.PushAsync(new AbsoluteLayoutPageCS())),
                    }
                }
            };
        }
    }
}
