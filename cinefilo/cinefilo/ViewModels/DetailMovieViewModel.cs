namespace cinefilo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using static cinefilo.Models.ws.GetListMovie;
    using Util;
    using cinefilo.Views;
    using cinefilo.Helpers;

    public class DetailMovieViewModel : BaseViewModel
    {

        private Movie movie;
        public Movie Movie { get { return movie; } set { SetValue(ref movie, value); } }
        private DialogLoading dialogLoading;

        public DetailMovieViewModel(Movie movie)
        {
            dialogLoading = new DialogLoading();
            this.Movie = movie;
        }

        public ICommand BuyTicketCommand { get { return new RelayCommand(BuyTicket); } }
        private async void BuyTicket()
        {
            try
            {
                await ShowDialog();

                MainViewModel.GetInstace().BuyTicketViewModel = new BuyTicketViewModel(movie);
                Util.ShowNavigationPageDefault(new BuyTicketPage());
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
