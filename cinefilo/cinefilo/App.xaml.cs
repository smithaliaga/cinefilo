using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace cinefilo
{
    using Views;
    using cinefilo.Util;
    using cinefilo.ViewModels;

    public partial class App : Application
    {
        #region Properties
        public static NavigationPage Navigator
        {
            get;
            internal set;
        }
        public static MasterPage Master { get; internal set; }
        public static bool IsOpen { get; set; } = false;
        #endregion

        #region Constructors
        public App()
        {
            InitializeComponent();
            if (Shared.GetIsFirstTime())
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }

        }
        #endregion

        #region Methods
        protected override void OnStart()
        {
            // Handle when your app starts
            IsOpen = true;
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            IsOpen = false;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            IsOpen = true;
        }
        #endregion
    }
}
