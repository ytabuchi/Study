using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XF_MvvmSample.View
{
    public partial class XamlPage : ContentPage
    {
        public XamlPage()
        {
            InitializeComponent();

            // コードビハインドで BindingContext を指定する場合は初期値を入れられるみたいです。
            // this.BindingContext = new XamlPageViewModel() { Name = "My Name" };

            this.switcher.PropertyChanged += (sender, e) =>
            {
                this.letterlabel.SetBinding(Label.TextProperty,
                                        new Binding("Message",
                                                    converter: new Converters.StringCaseConverter(),
                                                    converterParameter: switcher.IsToggled));
            };
        }
    }
}
