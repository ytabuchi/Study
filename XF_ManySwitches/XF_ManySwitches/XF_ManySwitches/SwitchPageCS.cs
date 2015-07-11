using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_ManySwitches
{
    public class SwitchPageCS : ContentPage
    {
        public SwitchPageCS()
        {
            SwitchPageViewModel vm = new SwitchPageViewModel();
            BindingContext = vm;

            var sw0 = new SwitchCell { Text = "Toggle sw1 & sw2" };
            sw0.SetBinding(SwitchCell.OnProperty, "SwAllValue", BindingMode.OneWayToSource);
            var sw1 = new SwitchCell { Text = "Toggle sw1" };
            sw1.SetBinding(SwitchCell.OnProperty, "Sw1Value", BindingMode.TwoWay);
            var sw2 = new SwitchCell { Text = "Toggle sw2" };
            sw2.SetBinding(SwitchCell.OnProperty, "Sw2Value", BindingMode.TwoWay);
            var tc1 = new TextCell { Text = "sw1 value" };
            tc1.SetBinding(TextCell.DetailProperty, "Sw1Value");
            var tc2 = new TextCell { Text = "sw2 value" };
            tc2.SetBinding(TextCell.DetailProperty, "Sw2Value");

            var tv = new TableView
            {
                Intent = TableIntent.Menu,
                Root = new TableRoot
                {
                    new TableSection("Toggle all")
                    {
                        sw0,
                    },

                    new TableSection("Toggle each")
                    {
                        sw1,
                        sw2,
                    },
                    new TableSection("Values of ViewModel")
                    {
                        tc1,
                        tc2,
                    }
                }
            };

            Content = new StackLayout
            {
                Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0),
                Children =
                {
                    tv,
                }
            };
        }
    }
}
