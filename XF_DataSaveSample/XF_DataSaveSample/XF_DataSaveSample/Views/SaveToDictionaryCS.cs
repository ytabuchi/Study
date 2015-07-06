using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XF_DataSaveSample.ViewModels;

namespace XF_DataSaveSample.Views
{
    public class SaveToDictionaryCS : ContentPage
    {
        private Entry entryName;
        private DatePicker pickerBirthday;
        private Switch switchLike;
        private Label resultLabel;

        public SaveToDictionaryCS()
        {
            var labelStyle = new Style(typeof(Label))
            {
                Setters = {
                new Setter { Property = Label.XAlignProperty, Value = TextAlignment.End },
                new Setter { Property = Label.YAlignProperty, Value = TextAlignment.Center },
                new Setter { Property = Label.WidthRequestProperty, Value = 150 }
                }
            };

            var labelName = new Label { Text = "Name:", Style = labelStyle };
            entryName = new Entry { Placeholder = "Input your name", HorizontalOptions = LayoutOptions.FillAndExpand };
            var labelBirthday = new Label { Text = "Birthday:", Style = labelStyle };
            pickerBirthday = new DatePicker { Date = new DateTime(1990, 1, 1) };
            var labelLike = new Label { Text = "Like Xamarin?", Style = labelStyle };
            switchLike = new Switch { };
            var saveButton = new Button { Text = "Save", HorizontalOptions = LayoutOptions.FillAndExpand };
            saveButton.Clicked += saveButton_Clicked;
            var loadButton = new Button { Text = "Load", HorizontalOptions = LayoutOptions.FillAndExpand };
            loadButton.Clicked += loadButton_Clicked;
            var clearButton = new Button { Text = "Clear", HorizontalOptions = LayoutOptions.FillAndExpand };
            clearButton.Clicked += clearButton_Clicked;
            resultLabel = new Label { Text = "", FontSize = 30 };

            Title = "Save to dictionary (C#)";
            Content = new StackLayout
            {
                Padding = 10,
                Spacing = 10,
                Children = {
					new Label { Text = "DataSave Sample", FontSize = 40, HorizontalOptions = LayoutOptions.Center },
                    new StackLayout { 
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            labelName, entryName
                        }
                    },
                    new StackLayout { 
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            labelBirthday, pickerBirthday
                        }
                    },
                    new StackLayout { 
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            labelLike, switchLike
                        }
                    },
                    new StackLayout { 
                        Orientation = StackOrientation.Horizontal,
                        Children = {
                            saveButton, loadButton, clearButton
                        }
                    },
                    resultLabel
				}
            };
        }

        void clearButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();

            if (!Application.Current.Properties.ContainsKey("id"))
            {
                resultLabel.Text = string.Empty;
                DisplayAlert("Done", "Data is cleared", "OK");
            }
        }

        void loadButton_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("name"))
            {
                var name = (string)Application.Current.Properties["name"];
                var birth = (DateTime)Application.Current.Properties["birth"];
                var like = (bool)Application.Current.Properties["like"];

                entryName.Text = name;
                pickerBirthday.Date = birth;
                switchLike.IsToggled = like;
                resultLabel.Text = string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", name, birth, like);
            }
            else
            {
                DisplayAlert("Error", "Data is not saved", "OK");
            }
        }

        void saveButton_Clicked(object sender, EventArgs e)
        {
            if (entryName.Text != null)
            {
                var name = entryName.Text;
                var birth = pickerBirthday.Date;
                var like = switchLike.IsToggled;
                Application.Current.Properties["name"] = name;
                Application.Current.Properties["birth"] = birth;
                Application.Current.Properties["like"] = like;

                DisplayAlert("DataSaved", string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", name, birth, like), "OK");
                resultLabel.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Please input your name.", "OK");
            }
        }
    }
}
