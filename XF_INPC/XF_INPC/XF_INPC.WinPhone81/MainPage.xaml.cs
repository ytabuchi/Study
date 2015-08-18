using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページのアイテム テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=391641 を参照してください

namespace XF_INPC.WinPhone81
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new XF_INPC.App());
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// このページがフレームに表示されるときに呼び出されます。
        /// </summary>
        /// <param name="e">このページにどのように到達したかを説明するイベント データ。
        /// このプロパティは、通常、ページを構成するために使用します。</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: ここに表示するページを準備します。

            // TODO: アプリケーションに複数のページが含まれている場合は、次のイベントの
            // 登録によりハードウェアの戻るボタンを処理していることを確認してください:
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed イベント。
            // 一部のテンプレートで指定された NavigationHelper を使用している場合は、
            // このイベントが自動的に処理されます。
        }
    }
}
