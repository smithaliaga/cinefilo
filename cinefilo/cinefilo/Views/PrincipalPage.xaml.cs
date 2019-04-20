namespace cinefilo.Views
{
    using System;
    using cinefilo.Helpers;
    using cinefilo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using Util;
    using static cinefilo.Models.ws.GetListMovie;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PrincipalPage : ContentPage
	{
		public PrincipalPage ()
		{
			InitializeComponent ();
            MainViewModel.GetInstace().Navigation = Navigation;
        }

        private ViewCell lastCell;

        private void OnTappedViewCell(object sender, System.EventArgs e)
        {
            try
            {
                if (lastCell != null)
                    lastCell.View.BackgroundColor = Color.Transparent;
                var viewCell = (ViewCell)sender;
                if (viewCell.View != null)
                {
                    viewCell.View.BackgroundColor = (Color)Application.Current.Resources["ColorType17"];
                    lastCell = viewCell;
                }
            }
            catch (Exception ex)
            {
                ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
            }
        }

        private bool isEnabled = true;

        private async void lstItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                if (isEnabled)
                {
                    isEnabled = false;
                    var myListView = (ListView)sender;
                    var myItem = myListView.SelectedItem;

                    Movie movieSelected = (Movie) myItem;

                    MainViewModel.GetInstace().DetailMovieViewModel = new DetailMovieViewModel(movieSelected);
                    Util.ShowNavigationPageDefault(new DetailMoviePage(movieSelected));
                    isEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ServiceBackgroundWorker.GetInstance().SendExceptionWorker(ex.ToString());
                isEnabled = true;
            }
        }
    }
}