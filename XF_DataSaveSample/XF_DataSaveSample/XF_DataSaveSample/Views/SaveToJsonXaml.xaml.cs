using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Xamarin.Forms;
using XF_DataSaveSample.ViewModels;

namespace XF_DataSaveSample.Views
{
    public partial class SaveToJsonXaml : ContentPage
    {
        private AllPagesViewModel vm = new AllPagesViewModel();

        public SaveToJsonXaml()
        {
            InitializeComponent();
            this.BindingContext = vm;
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
