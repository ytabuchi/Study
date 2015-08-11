using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace XF_LVItemAppearingSample
{
    public class ListViewPageCS : ContentPage
    {
        //List<ListItem> listItems = new List<ListItem>();
        ObservableCollection<ListItem> listItems = new ObservableCollection<ListItem>();
        // 動的に List<ListItem> を追加するのに使ってるだけです。
        int n = 0;
        const int cellAmount = 25;

        public ListViewPageCS()
        {
            AddListItem(n);

            // ListView を定義します。
            var list = new ListView
            {
                ItemsSource = listItems,
                ItemTemplate = new DataTemplate(typeof(TextCell)),
            };
            list.SetBinding(TextCell.TextProperty, "TextItem");
            list.SetBinding(TextCell.DetailProperty, "DetailItem");

            // ListView の各 Item が表示された時にイベントが発生します。
            list.ItemAppearing += (object sender, ItemVisibilityEventArgs e) => {
#if DEBUG
                System.Diagnostics.Debug.WriteLine((e.Item as ListItem).TextItem);
                System.Diagnostics.Debug.WriteLine("LastData is " + listItems.Last().TextItem);
#endif
                // ObservableCollection の最後が ListView の Item と一致した時に ObservableCollection にデータを追加するなどの処理を行ってください。
                if (listItems.Last() == e.Item as ListItem)
                {
                    n++;
                    AddListItem(cellAmount * n);
                }
            };

            Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            Content = list;
        }

        /// <summary>
        /// cellAmount の数だけ listItems にデータを追加していきます。
        /// </summary>
        /// <param name="i">開始値</param>
        void AddListItem(int i)
        {
            foreach (var j in Enumerable.Range(i, cellAmount))
            {
                listItems.Add(new ListItem { TextItem = "TextData " + j, DetailItem = "DetailData " + j });
            }
        }
    }
}
