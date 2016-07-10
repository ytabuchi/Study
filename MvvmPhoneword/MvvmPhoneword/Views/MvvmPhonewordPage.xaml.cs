using Xamarin.Forms;

namespace MvvmPhoneword.Views
{
    public partial class MvvmPhonewordPage : ContentPage
    {
        public MvvmPhonewordPage()
        {
            InitializeComponent();


            MessagingCenter.Subscribe<ViewModels.MvvmPhonewordPageViewModel>(
                this,
                "ShowCallHistoryPage",
                async (sender) =>
                {
                    await Navigation.PushAsync(new CallHistoryPage());
                }
            );
        }
    }
}

