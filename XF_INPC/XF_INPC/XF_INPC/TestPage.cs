using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_INPC
{
    public class TestPage : ContentPage
    {
        public TestPage()
        {
            this.BindingContext = new TestPageViewModel();

            var ed = new Editor { Text = "BindingTo TextValue" };
            ed.SetBinding(Editor.TextProperty, "TextValue", mode: BindingMode.OneWayToSource);
            var lb = new Label { Text = "" };
            lb.SetBinding(Label.TextProperty, "TextValue");

            var sw = new Switch();
            sw.SetBinding(Switch.IsToggledProperty, "BoolValue", mode: BindingMode.TwoWay);

            var sc = new SwitchCell { Text = "BindingTo BoolValue" };
            sc.SetBinding(SwitchCell.OnProperty, "BoolValue", mode: BindingMode.TwoWay);

            var tc = new TextCell { Text = "" };
            tc.SetBinding(TextCell.TextProperty, "BoolValue", stringFormat:"Value: {0}");

            var tv = new TableView
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot
                {
                    new TableSection("TableView")
                    {
                        sc,
                        tc,
                    },
                }
            };

            Content = new StackLayout
            {
                Padding = 20,
                Children = {
                    new Label {
                        Text = "INotifyPropertyChanged Test",
                        FontSize = 30,
                    },
                    ed,
                    lb,
                    sw,
                    tv,
                }
            };
        }
    }
}
