using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_IValueConverterSample.View
{
    public class CSViewModelConverterPage : ContentPage
    {
        public CSViewModelConverterPage()
        {
            BindingContext = new ViewModel.CommonViewModel();

            var editor = new Editor
            {
                Text = "",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            editor.SetBinding(Editor.TextProperty, "Message");
            var label = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            label.SetBinding(Label.TextProperty, "Message");
            var sclabel = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            sclabel.SetBinding(Label.TextProperty,
                new Binding("Message",
                    converter: new Converters.StringCaseConverter(),
                    converterParameter: "True"));
            var sllabel = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            sllabel.SetBinding(Label.TextProperty,
                new Binding("Message",
                    converter: new Converters.StringToLengthConverter(),
                    stringFormat: "{0} letters"));

            var stack = new StackLayout
            {
                Padding = 20,
                Children = {
                    editor,
                    label,
                    sclabel,
                    sllabel,
                },
            };

            Padding = 15;
            Title = "ViewModel bindig (C#)";
            Content = stack;
        }
    }
}
