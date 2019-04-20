namespace cinefilo.ViewModels
{
    using System;
    using GalaSoft.MvvmLight.Command;
    using System.Windows.Input;
    using Helpers;
    using Views;
    using Util;
    using Services;
    using Models;
    using System.Diagnostics;

    public class LoginViewModel : BaseViewModel
    {

        #region Attributes
        private string user;
        private string password;
        private DialogLoading dialogLoading;
        #endregion

        #region Properties
        public String User
        {
            get
            {
                return this.user;
            }
            set
            {
                SetValue(ref this.user, value);
            }
        }

        public String Password
        {
            get
            {
                return this.password;
            }
            set
            {
                SetValue(ref this.password, value);
            }
        }
        #endregion

        #region Constructors
        public LoginViewModel()
        {
            this.dialogLoading = new DialogLoading();
            this.user = Shared.GetUser();
        }
        #endregion

        #region Commands
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.User) || string.IsNullOrEmpty(this.Password))
            {
                await Util.ShowMessage(Languages.Alert, Languages.Default_Fill_Data, Languages.Accept, null);
                return;
            }

            bool stateEvent = false;
            try
            {
                ShowDialogLoading(dialogLoading);

                
                var userAuthenticate = await ApiService.WS_UserAuthenticate<UserAuthenticate>
                (this.User, this.Password, Util.GetDeviceLanguage(), Util.GetDeviceInfo());
                if (userAuthenticate != null)
                {
                    if (userAuthenticate.ErrorCode == 0)
                    {
                        Shared.SetUser(this.user);

                        var mainViewModel = MainViewModel.GetInstace();
                        mainViewModel.Session.Token = userAuthenticate.Token;
                        mainViewModel.Session.UserImage = Util.GetImageSourceFromBase64(userAuthenticate.UserImage);

                        mainViewModel.PrincipalViewModel = new PrincipalViewModel();
                        stateEvent = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                //await ApiService.WS_SendException<EntityWSBase>(ex.ToString(),"");
            }
            finally
            {
                this.Password = string.Empty;
                CloseDialogLoading(dialogLoading);
            }

            if (stateEvent)
            {
                Util.ShowMasterPage();
            }
        }

        #endregion
    }
}
