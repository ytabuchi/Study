using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF_DataSaveSample.ViewModels;


namespace XF_DataSaveSample.Views
{
    public partial class SaveByVMXaml : ContentPage
    {
        private AllPagesViewModel vm = new AllPagesViewModel();

        public SaveByVMXaml()
        {
            InitializeComponent();
            this.BindingContext = vm;
        }

        async void clearButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.Properties.Clear();

            if (!Application.Current.Properties.ContainsKey("name"))
            {
                resultLabel.Text = string.Empty;
                await DisplayAlert("Done", "Data is cleared", "OK");
            }
        }

        async void loadButton_Clicked(object sender, EventArgs e)
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
                await DisplayAlert("Error", "Data is not saved", "OK");
            }
        }

        async void saveButton_Clicked(object sender, EventArgs e)
        {
            if (entryName.Text != null)
            {
                Application.Current.Properties["name"] = vm.Name;
                Application.Current.Properties["birth"] = vm.Birthday;
                Application.Current.Properties["like"] = vm.Like;

                await DisplayAlert("DataSaved", string.Format("Name: {0}\nBirthday: {1:yyyy/MM/dd}\nLike?: {2}", vm.Name, vm.Birthday, vm.Like), "OK");
                resultLabel.Text = string.Empty;
            }
            else
            {
                await DisplayAlert("Error", "Please input your name.", "OK");
            }
        }
    }
}
