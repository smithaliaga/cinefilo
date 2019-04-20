namespace cinefilo.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Xamarin.Essentials;
    using Rg.Plugins.Popup.Services;
    using cinefilo.Views;
    using cinefilo.Util;
    using cinefilo.Helpers;
    using cinefilo.Services;
    using cinefilo.Models.ws;

    public class CollectViewModel : BaseViewModel
    {
        public CollectViewModel()
        {
            this.dialogLoading = new DialogLoading();
        }

        private DialogLoading dialogLoading;
        private string token = "";

        public async Task ProcessTransaction(string idPendingTransaction)
        {
            try
            {
                await ShowDialog();
                /*
                var mainViewModel = MainViewModel.GetInstace();

                var processQR = await ApiService.WS_ProcessQR<ProcessQR>
                        (idPendingTransaction, Util.GetDeviceLanguage());
                if (processQR != null)
                {
                    if (processQR.ErrorCode == 0)
                    {
                        Vibration.Vibrate();
                        await Util.ShowMessage(Languages.Alert, Languages.System_Request_Success, null);
                    }
                }
                else
                {
                    return;
                }
                */
            }
            catch (Exception ex)
            {
                ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, null);
            }
            finally
            {
                await CloseDialog();
            }
        }

        public async Task ShowDialog()
        {
            try { await PopupNavigation.Instance.PushAsync(dialogLoading, true); }
            catch (Exception ex)
            {
                ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
            }
        }

        public async Task CloseDialog()
        {
            try { await PopupNavigation.Instance.RemovePageAsync(dialogLoading, true); }
            catch (Exception ex)
            {
                ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
            }
        }
    }
}
