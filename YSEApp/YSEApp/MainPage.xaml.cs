using System.ComponentModel;
using Xamarin.Forms;
using ZXing;

namespace YSEApp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
           // ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            InitializeComponent();
        }
        public void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await DisplayAlert("Scanned result", result.Text, "OK");
            });
        }
    }
}
