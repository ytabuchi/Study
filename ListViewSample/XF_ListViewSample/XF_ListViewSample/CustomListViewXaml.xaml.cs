using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ListViewSample;
using Xamarin.Forms;

namespace XF_ListViewSample
{
    public partial class CustomListViewXaml : ContentPage
    {
        List<Person> persons = new List<Person>();
        public CustomListViewXaml()
        {
            InitializeComponent();

            #region //persons に追加
            persons.Add(new Person { Name = "Alen", ImageResourceFileName = "A.png" });
            persons.Add(new Person { Name = "Brawn", ImageResourceFileName = "B.png" });
            persons.Add(new Person { Name = "Charie", ImageResourceFileName = "C.png" });
            persons.Add(new Person { Name = "Danny", ImageResourceFileName = "D.png" });
            persons.Add(new Person { Name = "Eric", ImageResourceFileName = "E.png" });
            persons.Add(new Person { Name = "Alen2", ImageResourceFileName = "A.png" });
            persons.Add(new Person { Name = "Brawn2", ImageResourceFileName = "B.png" });
            persons.Add(new Person { Name = "Charie2", ImageResourceFileName = "C.png" });
            persons.Add(new Person { Name = "Danny2", ImageResourceFileName = "D.png" });
            persons.Add(new Person { Name = "Eric2", ImageResourceFileName = "E.png" });
            persons.Add(new Person { Name = "Alen3", ImageResourceFileName = "A.png" });
            persons.Add(new Person { Name = "Brawn3", ImageResourceFileName = "B.png" });
            persons.Add(new Person { Name = "Charie3", ImageResourceFileName = "C.png" });
            persons.Add(new Person { Name = "Danny3", ImageResourceFileName = "D.png" });
            persons.Add(new Person { Name = "Eric3", ImageResourceFileName = "E.png" });
            persons.Add(new Person { Name = "Alen4", ImageResourceFileName = "A.png" });
            persons.Add(new Person { Name = "Brawn4", ImageResourceFileName = "B.png" });
            persons.Add(new Person { Name = "Charie4", ImageResourceFileName = "C.png" });
            persons.Add(new Person { Name = "Danny4", ImageResourceFileName = "D.png" });
            persons.Add(new Person { Name = "Eric4", ImageResourceFileName = "E.png" });
            persons.Add(new Person { Name = "Alen5", ImageResourceFileName = "A.png" });
            persons.Add(new Person { Name = "Brawn5", ImageResourceFileName = "B.png" });
            persons.Add(new Person { Name = "Charie5", ImageResourceFileName = "C.png" });
            persons.Add(new Person { Name = "Danny5", ImageResourceFileName = "D.png" });
            persons.Add(new Person { Name = "Eric5", ImageResourceFileName = "E.png" });
            #endregion

            this.BindingContext = persons;
        }
    }
}
