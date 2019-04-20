namespace cinefilo.Views
{

    using Util;
    using ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : MasterDetailPage
    {

        public MasterPage () {
			InitializeComponent ();
            App.Navigator = Navigator;

            masterPage.listView.ItemSelected += OnItemSelected;
            if (Device.RuntimePlatform == Device.UWP) {
                MasterBehavior = MasterBehavior.Popover;
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e) {

            MenuItemViewModel item = e.SelectedItem as MenuItemViewModel;

            if (item == null)
            {
                return;
            }

            MainViewModel.GetInstace().CurrentMenuItemViewModel = item;

            if (item.PageName == Globales.MENU_DRAWER_PRINCIPAL_PAGE)
            {
                MainViewModel.GetInstace().PrincipalViewModel = new PrincipalViewModel();
                Detail = new NavigationPage(new PrincipalPage());
            }
            if (item.PageName == Globales.MENU_DRAWER_COLLECT_PAGE)
            {
                MainViewModel.GetInstace().CollectViewModel = new CollectViewModel();
                Detail = new NavigationPage(new CollectPage());
            }
            if (item.PageName == Globales.MENU_DRAWER_TRIVIA_PAGE)
            {
                MainViewModel.GetInstace().TriviaViewModel = new TriviaViewModel();
                Detail = new NavigationPage(new TriviaPage());
            }
            else if (item.PageName == Globales.MENU_DRAWER_LOGIN_PAGE)
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = Color.Black,
                };
                return;
            }

            masterPage.listView.SelectedItem = null;
            IsPresented = false;
        }
    }
}