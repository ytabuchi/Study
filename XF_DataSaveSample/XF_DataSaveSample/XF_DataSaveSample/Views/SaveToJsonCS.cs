using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF_DataSaveSample.ViewModels;

namespace XF_DataSaveSample.Views
{
    public class SaveToJsonCS : ContentPage
    {
        private Entry entryName;
        private Label resultLabel;
        private AllPagesViewModel vm = new AllPagesViewModel();

        public SaveToJsonCS()
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

            Title = "Save to Json (C#)";
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

        async void clearButton_Clicked(object sender, EventArgs e)
        {
            if (DependencyService.Get<ISaveAndLoad>().ClearData("temp.json"))
            {
                resultLabel.Text = null;
                await DisplayAlert("Done", "Data is cleared", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Data cannot be cleared", "OK");
            }
        }

        async void loadButton_Clicked(object sender, EventArgs e)
        {
            var data = DependencyService.Get<ISaveAndLoad>().LoadData("temp.json");
            if (data != null)
            {
                this.vm = JsonConvert.DeserializeObject<AllPagesViewModel>(data);
                this.BindingContext = vm;
                resultLabel.Text = string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", vm.Name, vm.Birthday, vm.Like);
            }
            else
            {
                await DisplayAlert("Error", "Data is not saved", "OK");
            }
        }

        async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (vm.Name != null)
            {
                var json = JsonConvert.SerializeObject(vm);
                DependencyService.Get<ISaveAndLoad>().SaveData("temp.json", json);
                await DisplayAlert("Data Saved", string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", vm.Name, vm.Birthday, vm.Like), "OK");
                resultLabel.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Please input your name.", "OK");
            }
        }
    }

}
