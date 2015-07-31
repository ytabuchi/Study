using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_SearchBarSample
{
    public class App : Application
    {
        public App()
        {
            MainPage = new SearchBarPageCS();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class SearchBarPageCS : ContentPage
    {
        MyListView mylist;
        SearchBar searchbar;
        public SearchBarPageCS()
        {
            mylist = new MyListView();
            searchbar = new SearchBar
            {
                Placeholder = "Search",
            };
            searchbar.TextChanged += (sender, e) => mylist.SearchFilter(e.NewTextValue);
            searchbar.SearchButtonPressed += (sender, e) => mylist.SearchFilter(searchbar.Text);

            Content = new StackLayout
            {
                Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 5),
                Children = {
                    searchbar,
                    mylist,
                }
            };
        }

    }

    public class MyListView : ListView
    {
        List<string> listData;
        public MyListView()
        {
            string[] str = { "test1", "TEST2", "abc", "XYZ", "ａｂｃ", "ｘｙｚ", "あいうえお", "カキクケコ", "表示性能" };
            listData = new List<string>(str);

            this.ItemsSource = listData;
        }

        /// <summary>
        /// MyListView を引数でフィルターするメソッドです。
        /// </summary>
        /// <param name="filter">string</param>
        public void SearchFilter(string filter)
        {
            this.BeginRefresh();

            if (string.IsNullOrWhiteSpace(filter))
            {
                this.ItemsSource = listData;
            }
            else
            {
                // 英語の大文字小文字対策のみですが、日本語の全角半角対策も入れるべきだと思います。
                // 良いメソッドをご存知の方はご紹介ください＞＜
                this.ItemsSource = listData
                    .Where(x => x.ToLower().Contains(filter.ToLower()));
            }

            this.EndRefresh();
        }
    }
}
