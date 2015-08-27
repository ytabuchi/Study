using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


using Xamarin.Forms;

namespace XF_PopupIndicator
{
    public class AbsolutePageCS : ContentPage
    {
        ObservableCollection<ListItem> listItems = new ObservableCollection<ListItem>();
        Frame frameLayer;
        ListView listLayer;
        ContentView bgLayer;

        // 以下は動的に List<ListItem> を追加するのに使ってるだけです。
        int n = 0;
        const int cellAmount = 25;

        public AbsolutePageCS()
        {
            AddListItem(n);

            var cell = new DataTemplate(typeof(TextCell));
            cell.SetBinding(TextCell.TextProperty, "TextItem");
            cell.SetBinding(TextCell.DetailProperty, "DetailItem");

            listLayer = new ListView
            {
                ItemsSource = listItems,
                ItemTemplate = cell,
            };
            listLayer.ItemAppearing += ListLayer_ItemAppearing;

            frameLayer = new Frame
            {
                //BackgroundColor = Color.Black.MultiplyAlpha(0.7d),
                BackgroundColor = Color.White,
                IsVisible = false,
                Content = new StackLayout
                {
                    Children =
                    {
                        new ActivityIndicator {
                            IsRunning = true,
                            Color = Device.OnPlatform(Color.White, Color.Default, Color.Accent),
                        },
                        new Label {
                            Text = "Data loading...",
                            TextColor = Color.Black,
                            XAlign = TextAlignment.Center,
                        },
                    }
                },
            };


            bgLayer = new ContentView
            {
                BackgroundColor = Color.Black.MultiplyAlpha(0.4d),
                IsVisible = false,
            };

            var abs = new AbsoluteLayout();

            abs.Children.Add(listLayer);
            abs.Children.Add(bgLayer);
            abs.Children.Add(frameLayer);

            this.Title = "AbsoluteIndicator";
            this.Padding = new Thickness(0, Device.OnPlatform(20, 0, 0), 0, 0);
            this.Content = abs;

            SizeChanged += AbsolutePageCS_SizeChanged;
        }

        /// <summary>
        /// ListView の各 Item が表示された時に発生するイベントです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ListLayer_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine((e.Item as ListItem).TextItem);
            System.Diagnostics.Debug.WriteLine("LastData is " + listItems.Last().TextItem);
#endif
            // ObservableCollection の最後が ListView の Item と一致した時に ObservableCollection にデータを追加するなどの処理を行ってください。
            if (listItems.Last() == e.Item as ListItem)
            {
                bgLayer.IsVisible = true;
                frameLayer.IsVisible = true;
                await Task.Delay(2000);

                n++;
                AddListItem(cellAmount * n);
                frameLayer.IsVisible = false;
                bgLayer.IsVisible = false;
            }
        }

        /// <summary>
        /// cellAmount の数だけ listItems にデータを追加していきます。
        /// </summary>
        /// <param name="i">開始値</param>
        private void AddListItem(int i)
        {

            foreach (var j in Enumerable.Range(i, cellAmount))
            {
                listItems.Add(new ListItem { TextItem = "TextData " + j, DetailItem = "DetailData " + j });
            }
        }

        /// <summary>
        /// サイズが決まった後で呼び出されます。AbsoluteLayout はここで位置を決めるのが良いみたいです。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AbsolutePageCS_SizeChanged(object sender, EventArgs e)
        {
            AbsoluteLayout.SetLayoutFlags(frameLayer,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(frameLayer,
                new Rectangle(0.5d, 0.5d,
                Device.OnPlatform(AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize, this.Width), AbsoluteLayout.AutoSize)); // View の中央に AutoSize で配置

            AbsoluteLayout.SetLayoutFlags(bgLayer,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(bgLayer,
                new Rectangle(0d, 0d,
                this.Width, this.Height)); // View の左上から View のサイズ一杯で配置

            AbsoluteLayout.SetLayoutFlags(listLayer,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(listLayer,
                new Rectangle(0d, 0d,
                this.Width, this.Height)); // View の左上から View のサイズ一杯で配置
        }


    }
}
