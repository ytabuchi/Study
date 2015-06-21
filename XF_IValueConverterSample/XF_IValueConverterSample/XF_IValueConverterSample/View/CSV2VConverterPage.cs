using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_IValueConverterSample.View
{
    public class CSV2VConverterPage : ContentPage
    {
        public CSV2VConverterPage()
        {
            var editor = new Editor
            {
                Text = "test",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            var label = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            label.BindingContext = editor;
            label.SetBinding(Label.TextProperty, "Text" );
            var sclabel = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            sclabel.BindingContext = editor;
            sclabel.SetBinding(Label.TextProperty, 
                new Binding("Text", 
                    converter: new Converters.StringCaseConverter(),
                    converterParameter: "True"));
            var sllabel = new Label
            {
                Text = "",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            sllabel.BindingContext = editor;
            sllabel.SetBinding(Label.TextProperty,
                new Binding("Text",
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
            Title = "ViewToView bindig (C#)";
            Content = stack;
        }


    }
}
