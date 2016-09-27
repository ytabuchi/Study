using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AnimationSample
{
    public class SplashPage : ContentPage
    {
        Image iconImage;

        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            var abs = new AbsoluteLayout();

            iconImage = new Image
            {
                Source = "Twitter.png",
                WidthRequest = 100,
                HeightRequest = 100
            };
            AbsoluteLayout.SetLayoutFlags(iconImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(iconImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            abs.Children.Add(iconImage);

            this.BackgroundColor = Color.FromHex("#429de3");
            this.Content = abs;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await iconImage.ScaleTo(1, 2000); //初期化などの時間のかかる処理
            await iconImage.ScaleTo(0.9, 1500, Easing.Linear);
            await iconImage.ScaleTo(150, 1200, Easing.Linear);
            await Navigation.PushAsync(new AnimationTest());
        }
    }
}
