using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XF_MasterDetailSample
{
    public class App : Application
    {
        public App()
        {
            MainPage = new RootPage();
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

    /// <summary>
    /// MasterDetailPage の定義です。Master(左側) と Detail(各子ページ) を指定します。
    /// </summary>
    public class RootPage : MasterDetailPage
    {
        public RootPage()
        {
            var menuPage = new MenuPage();
            // Menu ページの ListView Menu を選択した時に NavigateTo メソッドに SelectedItem を渡します。
            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            Master = menuPage;

            // Detail は NavigationPage で呼び出した方が良いです。(左からのスワイプで Master を出せるが操作しづらい)
            // バーの色を変えています。
            var detail = new NavigationPage(new ContractsPage());
            detail.BarBackgroundColor = Color.FromHex("3498DB");
            detail.BarTextColor = Color.White;
            Detail = detail;

        }

        /// <summary>
        /// ページ遷移のメソッドです。
        /// </summary>
        /// <param name="menu">MenuItem</param>
        void NavigateTo(MenuItem menu)
        {
            // menuPage の List<MenuItem> の選択値を MenuItem で受け取っているので
            // 予め定義されたページに移動できるってことは分かるんですが、凄いですね。
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            // 同じく各ページに移動する時にもバーの色を再設定 (このやり方では必須)
            var detail = new NavigationPage(displayPage);
            detail.BarBackgroundColor = Color.FromHex("3498DB");
            detail.BarTextColor = Color.White;
            Detail = detail;

            IsPresented = false;
        }
    }

    /// <summary>
    /// 左側のメニューページクラスです。
    /// </summary>
    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "settings.png"; // Icon を設定すると左側が文字でなくアイコンで置き換わります。
            Title = "Menu"; // Icon を指定しても Title プロパティは必須項目です。
            BackgroundColor = Color.FromHex("dce8ef");

            // ListView 設定
            Menu = new MenuListView();

            var menuLabel = new ContentView
            {
                Padding = new Thickness(10, 36, 0, 5),
                Content = new Label
                {
                    TextColor = Color.FromHex("333"),
                    Text = "MENU",
                    FontSize = 18,
                }
            };

            // タイトルの menuLabel と ListView を並べています。
            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            layout.Children.Add(menuLabel);
            layout.Children.Add(Menu);

            Content = layout;
        }
    }

    /// <summary>
    /// ListView を継承したクラスです。ItemsSource で List＜MenuItem＞、ItemTemplate で ImageCell を使用しています。
    /// </summary>
    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData(); // インスタンス化して、
            ItemsSource = data; // ItemsSource として指定します。
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            // ItemTemplate で使用しているのが ImageCell なので Android では Text が水色になってしまいます。
            // 嫌な場合は ImageCell ではなく ViewCell で ItemTemplate を作りましょう。
            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");

            ItemTemplate = cell;
        }
    }

    /// <summary>
    /// ListView のデータ用のクラスです。TargetType に遷移先ページを指定します。
    /// </summary>
    public class MenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }
        // この Type で移動先のページクラスを指定しています。
        public Type TargetType { get; set; }
    }

    /// <summary>
    /// ListView のデータクラスです。
    /// </summary>
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = "Contracts",
                IconSource = "contacts.png",
                TargetType = typeof(ContractsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Leads",
                IconSource = "leads.png",
                TargetType = typeof(LeadsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Accounts",
                IconSource = "accounts.png",
                TargetType = typeof(AccountsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Opportunities",
                IconSource = "opportunities.png",
                TargetType = typeof(OpportunitiesPage)
            });
        }
    }

    public class ContractsPage : ContentPage
    {
        public ContractsPage()
        {
            Title = "ContractsPage";
            BackgroundColor = Color.White;
            Content = new Label
            {
                Text = this.Title,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
    public class LeadsPage : ContentPage
    {
        public LeadsPage()
        {
            Title = "LeadsPage";
            BackgroundColor = Color.FromHex("ABC7D8");
            Content = new Label
            {
                Text = this.Title,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
    public class AccountsPage : ContentPage
    {
        public AccountsPage()
        {
            Title = "AccountsPage";
            BackgroundColor = Color.FromHex("A2C19B");
            Content = new Label
            {
                Text = this.Title,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
    public class OpportunitiesPage : ContentPage
    {
        public OpportunitiesPage()
        {
            Title = "OpportunitiesPage";
            BackgroundColor = Color.FromHex("B692B7");
            Content = new Label
            {
                Text = this.Title,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
        }
    }
}
