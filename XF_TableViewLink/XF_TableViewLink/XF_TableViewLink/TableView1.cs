using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_TableViewLink
{
    public class TableView1 : ContentPage
    {
        public TableView1()
        {

            var tv = new TableView
            {
                Intent = TableIntent.Settings,
                Root = new TableRoot
                {
                    new TableSection("TableView")
                    {
                        new TextCell {
                            Text = "2-1",
                            //TextColor = Color.Red,
                            Command = new Command(async () => await Navigation.PushAsync(new Page21())),
                        },
                        new TextCell {
                            Text = "2-2",
                            //TextColor = Color.Blue,
                            Detail = "PushModal to Page 2-2",
                            //DetailColor = Color.Yellow,
                            Command = new Command(async ()=> await Navigation.PushModalAsync(new Page22())),
                        },
                    },
                }
            };

            Title = "TableView1";
            Content = tv;
        }
    }
}
