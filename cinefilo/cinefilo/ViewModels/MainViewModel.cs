namespace cinefilo.ViewModels
{
    using System.Collections.ObjectModel;
    using Helpers;
    using cinefilo.Interfaces;
    using Util;
    using Xamarin.Forms;

    public class MainViewModel
    {
        #region Properties
        public ObservableCollection<MenuItemViewModel> Menus { get; set; }
        public Session Session = Session.GetInstace();
        public MenuItemViewModel CurrentMenuItemViewModel { get; set; }
        public INavigation Navigation { get; set; }
        #endregion

        #region ViewModels
        public LoginViewModel Login { get; set; }
        public PrincipalViewModel PrincipalViewModel{ get; set; }
        public CollectViewModel CollectViewModel { get; set; }
        public DetailMovieViewModel DetailMovieViewModel { get; set; }
        public BuyTicketViewModel BuyTicketViewModel { get; set; }
        public TriviaViewModel TriviaViewModel { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.LoadMenu();
            
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstace() {
            return instance == null ? new MainViewModel() : instance;
        }
        #endregion

        #region Methods
        private void LoadMenu()
        {
            this.Menus = new ObservableCollection<MenuItemViewModel>();
            MenuItemViewModel menuPrincipal = new MenuItemViewModel
            {
                Icon = "house_outline.png",
                PageName = Globales.MENU_DRAWER_PRINCIPAL_PAGE,
                Title = Languages.Menu_Drawer_Principal
            };

            /*ESTABLECEMOS EL MENU PRINCIPAL COMO VISTA DE INICIO*/
            CurrentMenuItemViewModel = menuPrincipal;
            this.Menus.Add(menuPrincipal);
            /**/

            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "house_outline.png",
                PageName = Globales.MENU_DRAWER_COLLECT_PAGE,
                Title = Languages.Menu_Drawer_Collect
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "house_outline.png",
                PageName = Globales.MENU_DRAWER_TRIVIA_PAGE,
                Title = Languages.Menu_Drawer_Trivia
            });
            this.Menus.Add(new MenuItemViewModel
            {
                Icon = "logout.png",
                PageName = Globales.MENU_DRAWER_LOGIN_PAGE,
                Title = Languages.Menu_Drawer_Logout
            });
        }
        #endregion
    }
}
