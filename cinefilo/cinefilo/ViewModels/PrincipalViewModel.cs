namespace cinefilo.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Windows.Input;
    using cinefilo.Views;
    using Util;
    using cinefilo.Helpers;
    using System.Collections.ObjectModel;
    using static cinefilo.Models.ws.GetListMovie;
    using System.Collections.Generic;
    using cinefilo.Services;
    using cinefilo.Models.ws;

    public class PrincipalViewModel : BaseViewModel
    {
        public PrincipalViewModel()
        {
            this.dialogLoading = new DialogLoading();
            OnLoad();
        }

        private DialogLoading dialogLoading;

        private ObservableCollection<Movie> listMovie;
        public ObservableCollection<Movie> ListMovie { get { return listMovie; } set { SetValue(ref listMovie, value); } }

        private async void OnLoad()
        {
            ListMovie = new ObservableCollection<Movie>();
            try
            {
                ShowDialog();
                
                var getListMovie = await ApiService.WS_GetListMovie<GetListMovie>(MainViewModel.GetInstace().Session.Token);
                if (getListMovie != null)
                {
                    if (getListMovie.ErrorCode == 0)
                    {
                        ListMovie = new ObservableCollection<Movie>(getListMovie.ListMovie);
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
                CloseDialog();
            }
        }

        private async void ShowDialog()
        {
            try { await PopupNavigation.Instance.PushAsync(dialogLoading, true); } catch (Exception) { }
        }

        private async void CloseDialog()
        {
            try { await PopupNavigation.Instance.RemovePageAsync(dialogLoading, true); } catch (Exception) { }
        }

    }
}
