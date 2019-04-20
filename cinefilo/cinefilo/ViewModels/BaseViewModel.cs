namespace cinefilo.ViewModels
{
    using cinefilo.Views;
    using Rg.Plugins.Popup.Services;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
            {
                return;
            }
            backingField = value;
            OnPropertyChanged(propertyName);
        }

        public async void ShowDialogLoading(DialogLoading dialogLoading)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(dialogLoading, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public async void CloseDialogLoading(DialogLoading dialogLoading)
        {
            try
            {
                await PopupNavigation.Instance.RemovePageAsync(dialogLoading, true);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
