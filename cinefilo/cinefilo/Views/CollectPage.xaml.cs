namespace cinefilo.Views
{
    using Plugin.Permissions.Abstractions;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using cinefilo.Helpers;
    using cinefilo.Util;
    using cinefilo.ViewModels;
    using Xam.Plugin.SimpleAppIntro;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using ZXing.Mobile;
    using ZXing.Net.Mobile.Forms;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CollectPage : ContentPage
	{
        private ZXingScannerView scanner;
        private bool isFirstTimeOnLoad = true;
        private bool isLoadLectorQR = false;

        public CollectPage()
        {
            InitializeComponent();
            MainViewModel.GetInstace().Navigation = Navigation;
            LoadLectorQR();
        }

        private async void LoadLectorQR()
        {
            if (isLoadLectorQR)
            {
                return;
            }

            isLoadLectorQR = true;

            await MainViewModel.GetInstace().CollectViewModel.ShowDialog();

            bool statePermission = await Permissions.GetPermissions(Permission.Camera);
            if (statePermission)
            {
                try
                {
                    await Task.Delay(1000);

                    scanner = new ZXingScannerView
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        Options = new MobileBarcodeScanningOptions()
                        {
                            PossibleFormats = new List<ZXing.BarcodeFormat>
                            {
                                ZXing.BarcodeFormat.QR_CODE,
                            },
                            DelayBetweenContinuousScans = 2000
                        },
                    };
                    scanner.OnScanResult += (result) =>
                    {
                        scanner.IsAnalyzing = false;
                        if (scanner.IsScanning)
                        {
                            scanner.AutoFocus();
                        }

                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await MainViewModel.GetInstace().CollectViewModel.ProcessTransaction(result.Text);
                            scanner.IsAnalyzing = true;
                        });
                    };

                    MainPageContainer.Children.Add(scanner);

                    scanner.IsEnabled = true;
                    scanner.IsAnalyzing = true;
                    scanner.IsScanning = true;
                }
                catch (Exception ex)
                {
                    ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
                }
            }
            isFirstTimeOnLoad = false;
            await MainViewModel.GetInstace().CollectViewModel.CloseDialog();
            isLoadLectorQR = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DisconnectScanner();
            if (!isFirstTimeOnLoad) LoadLectorQR();
        }

        protected override void OnDisappearing()
        {
            DisconnectScanner();
            base.OnDisappearing();
        }

        private void DisconnectScanner()
        {
            try
            {
                if (scanner != null) scanner.IsScanning = false; MainPageContainer.Children.Remove(scanner); scanner = null;
            }
            catch (Exception)
            {
            }
        }
    }
}