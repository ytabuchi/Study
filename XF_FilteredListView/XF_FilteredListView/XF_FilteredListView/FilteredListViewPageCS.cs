using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Xamarin.Forms;

namespace XF_FilteredListView
{
    public class FilteredListViewPageCS : ContentPage
    {
        ObservableCollection<string> listData = new ObservableCollection<string>();
        ListView listView;
        HashSet<string> hash2, filter;
        List<string> list2;
        Stopwatch sw = new Stopwatch();
        Stopwatch swEach = new Stopwatch();

        public FilteredListViewPageCS()
        {
            list2 = new List<string>();
            AddItems(list2, 2);
            hash2 = new HashSet<string>();
            AddItems(hash2, 2);

            filter = new HashSet<string>();
            AddItems(filter, 3);

            var anyButton = new Button
            {
                Text = "Any",
            };
            anyButton.Clicked += AnyButton_Clicked;
            var joinButton = new Button
            {
                Text = "Join",
            };
            joinButton.Clicked += JoinButton_Clicked;
            var intersectButton = new Button
            {
                Text = "Intersect",
            };
            intersectButton.Clicked += IntersectButton_Clicked;
            var hashButton = new Button
            {
                Text = "Hash",
            };
            hashButton.Clicked += HashButton_Clicked;

            listView = new ListView
            {
                ItemsSource = listData,
            };

            Content = new StackLayout
            {
                Padding = new Thickness(5, Device.OnPlatform(25, 5, 5), 5, 0),
                Children = {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Children =
                        {
                            anyButton,
                            joinButton,
                            intersectButton,
                            hashButton,
                        },
                    },
                    listView,
                }
            };
        }

        #region ボタンクリックメソッド群
        /// <summary>
        /// LINQ の Any で積集合を生成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AnyButton_Clicked(object sender, EventArgs e)
        {
            long[] eachTime = new long[10];
            listData.Clear();
            sw.Restart();

            for (int i = 0; i < 10; i++)
            {
                swEach.Restart();
                listData.Clear();
                var res = from s2 in list2 where filter.Any(fs => s2.Equals(fs)) select s2;
                foreach (var item in res)
                {
                    listData.Add(item);
                }
                eachTime[i] = swEach.ElapsedMilliseconds;
            }

            sw.Stop();
            await DisplayAlert("Any: Time(ms)", $"Total: {sw.ElapsedMilliseconds} ms\nAverage: {sw.ElapsedMilliseconds / 10} ms\nEach:\n{eachTime[0]} ms\n{eachTime[1]} ms\n{eachTime[2]} ms\n{eachTime[3]} ms\n{eachTime[4]} ms\n{eachTime[5]} ms\n{eachTime[6]} ms\n{eachTime[7]} ms\n{eachTime[8]} ms\n{eachTime[9]} ms", "OK");
        }
        /// <summary>
        /// LINQ の join で積集合を生成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void JoinButton_Clicked(object sender, EventArgs e)
        {
            long[] eachTime = new long[10];
            listData.Clear();
            sw.Restart();

            for (int i = 0; i < 10; i++)
            {
                swEach.Restart();
                listData.Clear();
                var res = from s2 in list2 join fs in filter on s2 equals fs select s2;
                foreach (var item in res)
                {
                    listData.Add(item);
                }
                eachTime[i] = swEach.ElapsedMilliseconds;
            }

            sw.Stop();
            await DisplayAlert("Join: Time(ms)", $"Total: {sw.ElapsedMilliseconds} ms\nAverage: {sw.ElapsedMilliseconds / 10} ms\nEach:\n{eachTime[0]} ms\n{eachTime[1]} ms\n{eachTime[2]} ms\n{eachTime[3]} ms\n{eachTime[4]} ms\n{eachTime[5]} ms\n{eachTime[6]} ms\n{eachTime[7]} ms\n{eachTime[8]} ms\n{eachTime[9]} ms", "OK");
        }
        /// <summary>
        /// Intersect で積集合を生成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void IntersectButton_Clicked(object sender, EventArgs e)
        {
            long[] eachTime = new long[10];
            listData.Clear();
            sw.Restart();

            for (int i = 0; i < 10; i++)
            {
                swEach.Restart();
                listData.Clear();
                var res = list2.Intersect(filter);
                foreach (var item in res)
                {
                    listData.Add(item);
                }
                eachTime[i] = swEach.ElapsedMilliseconds;
            }

            sw.Stop();
            await DisplayAlert("Intersect: Time(ms)", $"Total: {sw.ElapsedMilliseconds} ms\nAverage: {sw.ElapsedMilliseconds / 10} ms\nEach:\n{eachTime[0]} ms\n{eachTime[1]} ms\n{eachTime[2]} ms\n{eachTime[3]} ms\n{eachTime[4]} ms\n{eachTime[5]} ms\n{eachTime[6]} ms\n{eachTime[7]} ms\n{eachTime[8]} ms\n{eachTime[9]} ms", "OK");
        }
        /// <summary>
        /// HashSet.IntersectWith で積集合を生成します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void HashButton_Clicked(object sender, EventArgs e)
        {
            long[] eachTime = new long[10];
            listData.Clear();
            sw.Restart();

            for (int i = 0; i < 10; i++)
            {
                swEach.Restart();
                listData.Clear();
                hash2.IntersectWith(filter);
                foreach (var item in hash2)
                {
                    listData.Add(item);
                }
                eachTime[i] = swEach.ElapsedMilliseconds;
            }

            sw.Stop();
            await DisplayAlert("HashSet: Time(ms)", $"Total: {sw.ElapsedMilliseconds} ms\nAverage: {sw.ElapsedMilliseconds / 10} ms\nEach:\n{eachTime[0]} ms\n{eachTime[1]} ms\n{eachTime[2]} ms\n{eachTime[3]} ms\n{eachTime[4]} ms\n{eachTime[5]} ms\n{eachTime[6]} ms\n{eachTime[7]} ms\n{eachTime[8]} ms\n{eachTime[9]} ms", "OK");
        }
        #endregion

        /// <summary>
        /// HashSet に i の倍数を 1,000 個追加します。
        /// </summary>
        /// <param name="set"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private HashSet<string> AddItems(HashSet<string> set, int i)
        {
            foreach (var j in Enumerable.Range(0, 1000))
            {
                set.Add($"Item {i * j}");
            }
            return set;
        }
        /// <summary>
        /// List に i の倍数を 1,000 個追加します。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private List<string> AddItems(List<string> list, int i)
        {
            foreach (var j in Enumerable.Range(0, 1000))
            {
                list.Add($"Item {i * j}");
            }
            return list;
        }
    }
}
