using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_AnimationSample
{
    public partial class AnimationTest : ContentPage
    {
        Dictionary<string, Easing> animations = new Dictionary<string, Easing>
        {
            { "BounceIn", Easing.BounceIn },
            { "BounceOut", Easing.BounceOut },
            { "CubicIn", Easing.CubicIn },
            { "CubicInOut", Easing.CubicInOut },
            { "CubicOut", Easing.CubicOut },
            { "Linear", Easing.Linear },
            { "SinIn", Easing.SinIn },
            { "SinInOut", Easing.SinInOut },
            { "SinOut", Easing.SinOut },
            { "SpringIn", Easing.SpringIn },
            { "SpringOut", Easing.SpringOut }
        };

        Easing easing;

        public AnimationTest()
        {
            InitializeComponent();

            // See the more detailed sample in https://developer.xamarin.com/api/type/Xamarin.Forms.Picker/
            easingPicker.SelectedIndexChanged += (sender, e) =>
            {
                if (easingPicker.SelectedIndex == -1)
                {
                    easing = Easing.Linear;
                    button.IsEnabled = true;
                }
                else
                {
                    // Get the item name from `SelectedIndex`. Then map `Easing` enum using "animations" dictionary.
                    string easingName = easingPicker.Items[easingPicker.SelectedIndex];
                    easing = animations[easingName];
                    button.IsEnabled = true;
                }
            };

            button.Clicked += async (sender, e) =>
            {
                double d1, d2;
                int i;
                if (double.TryParse(x.Text, out d1) && double.TryParse(y.Text, out d2) && int.TryParse(length.Text, out i))
                {
                    // Animate BoxView.
                    await boxView.TranslateTo(d1, d2, (uint)i, easing);
                }
            };
        }

        /// <summary>
        /// Set this page as a root page.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            Navigation.RemovePage(Navigation.NavigationStack[0]);
        }
    }
}
