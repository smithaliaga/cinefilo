namespace cinefilo.Views
{
    using cinefilo.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TriviaPage : ContentPage
	{
		public TriviaPage ()
		{
			InitializeComponent ();
            MainViewModel.GetInstace().Navigation = Navigation;
        }
	}
}