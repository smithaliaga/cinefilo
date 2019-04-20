namespace cinefilo.Views
{
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Threading.Tasks;
    using cinefilo.Interfaces;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GetChangePasswordPop : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Attributes
        private IEnterChangePasswordListener enterChangePasswordListener;
        #endregion

        public GetChangePasswordPop(IEnterChangePasswordListener enterChangePasswordListener)
        {
            InitializeComponent();
            this.enterChangePasswordListener = enterChangePasswordListener;
            CloseWhenBackgroundIsClicked = false;
        }

        public async void onAccept(object sender, EventArgs e)
        {
            try
            {
                enterChangePasswordListener.OnEnterChangePasswordClick(currentPassword.Text, newPassword.Text, false);

                await PopupNavigation.Instance.RemovePageAsync(this, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public async void onCancel(object sender, EventArgs e)
        {
            try
            {
                enterChangePasswordListener.OnEnterChangePasswordClick("", "", true);

                await PopupNavigation.Instance.RemovePageAsync(this, true);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            return base.OnBackgroundClicked();
        }
    }
}