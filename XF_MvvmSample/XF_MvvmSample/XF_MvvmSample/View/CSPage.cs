using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_MvvmSample.View
{
    public class CSPage : ContentPage
    {
        Label sclabel;
        Switch switcher;

        public CSPage()
        {
            BindingContext = new ViewModel.PageViewModel();

            var editor = new Editor { Text = "", HorizontalOptions = LayoutOptions.FillAndExpand };
            editor.SetBinding(Editor.TextProperty, "Message");

            switcher = new Switch {  };
            //switcher.SetBinding(Switch.IsToggledProperty, "Toggled");
            switcher.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                sclabel.SetBinding(Label.TextProperty,
                new Binding("Message",
                    converter: new Converters.StringCaseConverter(),
                    converterParameter: switcher.IsToggled));
            };

            var label = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 30
            };
            label.SetBinding(Label.TextProperty, "Message");

            sclabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 30
            };
            

            var sllabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontSize = 30
            };
            sllabel.SetBinding(Label.TextProperty,
                new Binding("Message",
                    converter: new Converters.StringToLengthConverter(),
                    stringFormat: "{0} letters"));

            Title = "Mvvm w/ C#";
            Content = new StackLayout
            {
                Padding = new Thickness(20, 5),
                Children = {
					editor,
                    switcher,
                    label,
                    sclabel,
                    sllabel
				}
            };
        }
    }
}
