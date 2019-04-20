namespace cinefilo.Views {

    using Rg.Plugins.Popup.Services;
    using System;
    using System.Threading.Tasks;
    using cinefilo.Interfaces;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetPasswordPop : Rg.Plugins.Popup.Pages.PopupPage {

        #region Attributes
        private IEnterPasswordListener enterPasswordListener;
        #endregion

        public GetPasswordPop(IEnterPasswordListener enterPasswordListener)  {
            InitializeComponent();
            this.enterPasswordListener = enterPasswordListener;
            CloseWhenBackgroundIsClicked = false;
        }

        public async void Send(object sender, EventArgs e) {
            try
            {
                if (string.IsNullOrEmpty(userCode.Text))
                {
                    //MANDAMOS ESTADO TRUE PARA OBLIGAR A QUE SE CANCELE EL METODO INICIADO
                    enterPasswordListener.OnEnterPasswordClick(userCode.Text, true);
                }
                else
                {
                    //MANDAMOS ESTADO FALSE PARA QUE CONTINUE CON EL PROCESO DEL METODO INICIADO
                    enterPasswordListener.OnEnterPasswordClick(userCode.Text, false);
                }
                await PopupNavigation.Instance.RemovePageAsync(this, true);
            }
            catch(Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            
        }

        protected override void OnAppearing()  {
            base.OnAppearing();
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
        }

        protected override void OnAppearingAnimationBegin() {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd() {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin() {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd() {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()  {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync() {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync() {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()  {
            return base.OnDisappearingAnimationEndAsync();
        }

        protected override bool OnBackButtonPressed() {
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked() {
            return base.OnBackgroundClicked();
        }
    }
}