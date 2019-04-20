namespace cinefilo.Views
{
    using Rg.Plugins.Popup.Pages;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using cinefilo.Helpers;
    using cinefilo.Interfaces;
    using cinefilo.Models.Local;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Util;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ModalPicker : PopupPage
    {

        private IModalPickerListener modalPickerListener;
        private List<EntityModalPicker> list;
        private int secuence;

        public ModalPicker (int secuence, IModalPickerListener modalPickerListener, List<EntityModalPicker> list, string title)
		{
            InitializeComponent ();
            this.secuence = secuence;
            this.modalPickerListener = modalPickerListener;
            this.list = new List<EntityModalPicker>(list);
            this.lst.ItemsSource = this.list;
            this.LabelTitle.Text = title;
            this.EntryFiler.TextChanged += OnEntryFilterTextChanged;
        }

        public async void lstItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await PopupNavigation.Instance.PopAsync();

                //ENVIA LOS DATOS DE LA FILA SELECCIONADA A LA VISTA ORIGEN
                var myListView = (ListView)sender;
                var myItem = myListView.SelectedItem;
                modalPickerListener.OnSelectedItem(secuence, myItem);
            }
            catch (Exception)
            {
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
            }
        }

        private async void OnEntryFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            entry.TextChanged -= OnEntryFilterTextChanged;
            try
            {
                this.lst.ItemsSource = new List<EntityModalPicker>(
                        this.list.FindAll(l => l.Name.ToUpper().Contains(entry.Text.ToUpper())));
            }
            catch (Exception)
            {
                await Util.ShowMessage(Languages.Alert, Languages.System_Process_Error, Languages.Accept, null);
            }
            finally
            {
                entry.TextChanged += OnEntryFilterTextChanged;
            }
        }

        private async void goBack(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}