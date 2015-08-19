using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AbsoluteIndicator
{
    public class StartPageCS : ContentPage
    {
        ActivityIndicator indicator;
        BaseView baseView;

        public StartPageCS()
        {
            var abs = new AbsoluteLayout
            {
                //BackgroundColor = Color.Black,
                //Opacity = 0.3d,
            };

            indicator = new ActivityIndicator
            {
                BackgroundColor = Color.Gray,
                IsRunning = true,
                Color = Color.Red,
                Scale = 1.5d,
                //Opacity = 1.0d,
            };

            var w = this.Width;
            var h = this.Height;

            AbsoluteLayout.SetLayoutFlags(indicator,
                AbsoluteLayoutFlags.PositionProportional);
            //AbsoluteLayout.SetLayoutBounds(indicator,
            //    new Rectangle(0.5d, 0.5d,
            //    AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            //indicator.Width * 4, indicator.Height * 3

            baseView = new BaseView();
            AbsoluteLayout.SetLayoutFlags(baseView,
                AbsoluteLayoutFlags.PositionProportional);


            abs.Children.Add(baseView);
            abs.Children.Add(indicator);

            Content = abs;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            AbsoluteLayout.SetLayoutBounds(indicator,
                new Rectangle(0.5d, 0.5d,
                indicator.Width * 4, indicator.Height * 3));
            AbsoluteLayout.SetLayoutBounds(baseView,
                new Rectangle(0d, 0d,
                this.Width, this.Height));
        }
    }
}
