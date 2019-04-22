using GalaSoft.MvvmLight.Command;
using cinefilo.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static cinefilo.Models.ws.GetListMovie;

namespace cinefilo.ViewModels
{
    using cinefilo.Helpers;
    using cinefilo.Models;
    using cinefilo.Services;
    using Util;

    public class BuyTicketViewModel : BaseViewModel
    {
        private Movie movie;
        public Movie Movie { get { return movie; } set { SetValue(ref movie, value); } }
        private string cardNumber;
        public string CardNumber { get { return cardNumber; } set { SetValue(ref cardNumber, value); } }
        private string cardHolder;
        public string CardHolder { get { return cardHolder; } set { SetValue(ref cardHolder, value); } }
        private string cardMonthExpirationDate;
        public string CardMonthExpirationDate { get { return cardMonthExpirationDate; } set { SetValue(ref cardMonthExpirationDate, value); } }
        private string cardYearExpirationDate;
        public string CardYearExpirationDate { get { return cardYearExpirationDate; } set { SetValue(ref cardYearExpirationDate, value); } }
        private string cardCVV;
        public string CardCVV { get { return cardCVV; } set { SetValue(ref cardCVV, value); } }
        private DialogLoading dialogLoading;

        public BuyTicketViewModel(Movie movie)
        {
            dialogLoading = new DialogLoading();
            this.Movie = movie;
        }

        public ICommand ProcessTransactionCommand { get { return new RelayCommand(ProcessTransaction); } }
        private async void ProcessTransaction()
        {
            try
            {
                if (Movie == null || string.IsNullOrEmpty(CardNumber) || string.IsNullOrEmpty(CardHolder) ||
                    string.IsNullOrEmpty(CardMonthExpirationDate) || string.IsNullOrEmpty(CardYearExpirationDate) ||
                    string.IsNullOrEmpty(CardCVV))
                {
                    await Util.ShowMessage(Languages.Alert, Languages.Default_Fill_Data, Languages.Accept, null);
                    return;
                }

                await ShowDialog();

                var response = await ApiService.WS_SendTransactionBuyTicket<EntityWSBase>(MainViewModel.GetInstace().Session.Token,
                    Movie.Code, CardNumber, CardHolder, cardMonthExpirationDate + "/" + CardYearExpirationDate, CardCVV);
                if (response != null)
                {
                    if (response.ErrorCode == 0)
                    {
                        await Util.ShowMessage(Languages.Alert, Languages.System_Request_Success, Languages.Accept, null);
                        Util.HideAllModalsDefault();
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
            }
            finally
            {
                await CloseDialog();
            }
        }

        private async Task ShowDialog()
        {
            try { await PopupNavigation.Instance.PushAsync(dialogLoading, true); } catch (Exception) { }
        }

        private async Task CloseDialog()
        {
            try { await PopupNavigation.Instance.RemovePageAsync(dialogLoading, true); } catch (Exception) { }
        }
    }
}
