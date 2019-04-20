namespace cinefilo.Views
{
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using System;
    using cinefilo.Helpers;
    using cinefilo.Interfaces;
    using cinefilo.Util;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModalDateFilter : PopupPage
    {

        private IModalDateFilterListener modalDateFilterListener;

        public ModalDateFilter (IModalDateFilterListener modalDateFilterListener, string title)
		{
            this.modalDateFilterListener = modalDateFilterListener;
            InitializeComponent ();
            this.LabelTitle.Text = title;
            fromDate.Format = Languages.System_Date_Format_Default;
            toDate.Format = Languages.System_Date_Format_Default;
		}

        private async void onAccept(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();

                modalDateFilterListener.OnConfirmFilter(true,
                    fromDate.Date.ToString(Globales.DateFormatMets),
                    toDate.Date.ToString(Globales.DateFormatMets));
            }
            catch(Exception)
            {
            }
            
        }

        private async void onCancel(object sender, EventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();

                modalDateFilterListener.OnConfirmFilter(false, "", "");
            }
            catch (Exception)
            {
            }
        }
    }
}