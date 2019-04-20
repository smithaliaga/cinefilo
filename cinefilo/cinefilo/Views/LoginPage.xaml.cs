namespace cinefilo.Views
{
    using cinefilo.Helpers;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage {
		public LoginPage () {
			InitializeComponent ();
            
            username.Completed += (object sender, EventArgs e) => {
                btnLogin.Focus();
                //password.Focus();
                ShowModalEntePassword();
            };
        }

        private void Password_Clicked(object sender, EventArgs e)
        {
            ShowModalEntePassword();
        }

        private bool isEnabled = true;

        private async void ShowModalEntePassword()
        {
            try
            {
                if (isEnabled)
                {
                    isEnabled = false;
                    await PopupNavigation.Instance.PushAsync(new ModalPasswordSecurity(password, password.Placeholder, Languages.Accept));
                    isEnabled = true;
                }
            }catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                isEnabled = true;
            }
        }
    }
}