using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XF_IValueConverterSample.View
{
    public partial class ListViewConverterPage : ContentPage
    {
        public ListViewConverterPage()
        {
            InitializeComponent();

            var listdata = new List<Words> {
                new Words { word = "the" },
                new Words { word = "quick" },
                new Words { word = "brown" },
                new Words { word = "fox" },
                new Words { word = "jumps" },
                new Words { word = "over" },
                new Words { word = "the" },
                new Words { word = "lazy" },
                new Words { word = "dog" }
            };

            this.BindingContext = listdata;
        }
    }

    public class Words
    {
        public string word { get; set; }
    }
}
