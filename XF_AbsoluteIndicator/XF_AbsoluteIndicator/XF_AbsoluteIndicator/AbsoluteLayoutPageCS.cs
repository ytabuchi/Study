using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_AbsoluteIndicator
{
    public class AbsoluteLayoutPageCS : ContentPage
    {

        BaseView baseView;
        IndicatorView indicatorView;

        public AbsoluteLayoutPageCS()
        {
            var abs = new AbsoluteLayout();



            indicatorView = new IndicatorView();

            //AbsoluteLayout.SetLayoutFlags(indicator,
            //    AbsoluteLayoutFlags.PositionProportional);
            //AbsoluteLayout.SetLayoutBounds(indicator,
            //    new Rectangle(0.5d, 0.5d,
            //    AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            //indicator.Width * 4, indicator.Height * 3

            baseView = new BaseView();



            abs.Children.Add(baseView);
            abs.Children.Add(indicatorView);

            Title = "AbsoluteIndicator";
            Content = abs;

            SizeChanged += AbsoluteLayoutPageCS_SizeChanged;
        }

        private void AbsoluteLayoutPageCS_SizeChanged(object sender, EventArgs e)
        {

#if DEBUG
            System.Diagnostics.Debug.WriteLine($"Width: {this.Width}");
            System.Diagnostics.Debug.WriteLine($"Height: {this.Height}");
#endif
            AbsoluteLayout.SetLayoutFlags(indicatorView,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(indicatorView,
                new Rectangle(0.5d, 0.5d,
                AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            //this.Width / 2, this.Height / 2
            AbsoluteLayout.SetLayoutFlags(baseView,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(baseView,
                new Rectangle(0d, 0d,
                this.Width, this.Height));
        }
    }
}
