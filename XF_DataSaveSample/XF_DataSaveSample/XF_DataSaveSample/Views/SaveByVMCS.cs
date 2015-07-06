using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XF_DataSaveSample.ViewModels;

namespace XF_DataSaveSample.Views
{
    public class SaveByVMCS : ContentPage
    {
        private Entry entryName;
        private Label resultLabel;
        private AllPagesViewModel vm = new AllPagesViewModel();

        public SaveByVMCS()
        {
            BindingContext = vm;

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
            entryName.SetBinding(Entry.TextProperty, "Name", mode: BindingMode.TwoWay);

            var labelBirthday = new Label { Text = "Birthday:", Style = labelStyle };
            var pickerBirthday = new DatePicker { };
            pickerBirthday.SetBinding(DatePicker.DateProperty, "Birthday", mode: BindingMode.TwoWay);

            var labelLike = new Label { Text = "Like Xamarin?", Style = labelStyle };
            var switchLike = new Switch { };
            switchLike.SetBinding(Switch.IsToggledProperty, "Like", mode: BindingMode.TwoWay);

            var saveButton = new Button { Text = "Save", HorizontalOptions = LayoutOptions.FillAndExpand };
            saveButton.Clicked += saveButton_Clicked;
            var loadButton = new Button { Text = "Load", HorizontalOptions = LayoutOptions.FillAndExpand };
            loadButton.Clicked += loadButton_Clicked;
            var clearButton = new Button { Text = "Clear", HorizontalOptions = LayoutOptions.FillAndExpand };
            clearButton.Clicked += clearButton_Clicked;
            resultLabel = new Label { Text = "", FontSize = 30 };

            Title = "Save to dic by vm (C#)";
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

            if (!Application.Current.Properties.ContainsKey("name"))
            {
                resultLabel.Text = string.Empty;
                DisplayAlert("Done", "Data is cleared", "OK");
            }
        }

        void loadButton_Clicked(object sender, EventArgs e)
        {
            if (Application.Current.Properties.ContainsKey("name"))
            {
                vm.Name = (string)Application.Current.Properties["name"];
                vm.Birthday = (DateTime)Application.Current.Properties["birth"];
                vm.Like = (bool)Application.Current.Properties["like"];

                resultLabel.Text = string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", vm.Name, vm.Birthday, vm.Like);
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
                Application.Current.Properties["name"] = vm.Name;
                Application.Current.Properties["birth"] = vm.Birthday;
                Application.Current.Properties["like"] = vm.Like;

                DisplayAlert("DataSaved", string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", vm.Name, vm.Birthday, vm.Like), "OK");
                resultLabel.Text = string.Empty;
            }
            else
            {
                DisplayAlert("Error", "Please input your name.", "OK");
            }
        }
    }
}
