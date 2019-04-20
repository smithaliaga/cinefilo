namespace cinefilo.Helpers
{
    using cinefilo.Interfaces;
    using cinefilo.Resources;
    using Xamarin.Forms;

    public static class Languages {
        static Languages() {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        public static string Alert => Resource.Alert;
        public static string Accept => Resource.Accept;
        public static string Cancel => Resource.Cancel;
        public static string ErrorInternetConnection => Resource.ErrorInternetConnection;
        public static string ErrorNoConnection => Resource.ErrorNoConnection;
        public static string Default_Fill_Data => Resource.Default_Fill_Data;

        #region MENU_DRAWER
        public static string Menu_Drawer_Principal => Resource.Menu_Drawer_Principal;
        public static string Menu_Drawer_Collect => Resource.Menu_Drawer_Collect;
        public static string Menu_Drawer_BuyTicket => Resource.Menu_Drawer_BuyTicket;
        public static string Menu_Drawer_Trivia => Resource.Menu_Drawer_Trivia;
        public static string Menu_Drawer_Logout => Resource.Menu_Drawer_Logout;
        #endregion

        #region EDITOR_RETURN_TYPE
        public static string Editor_ReturnType_Go => Resource.Editor_ReturnType_Go;
        public static string Editor_ReturnType_Next => Resource.Editor_ReturnType_Next;
        public static string Editor_ReturnType_Send => Resource.Editor_ReturnType_Send;
        public static string Editor_ReturnType_Search => Resource.Editor_ReturnType_Search;
        public static string Editor_ReturnType_Done => Resource.Editor_ReturnType_Done;
        #endregion

        #region LOGIN_PAGE
        public static string Login_User => Resource.Login_User;
        public static string Login_Password => Resource.Login_Password;
        public static string Login_Forgot_Your_Password => Resource.Login_Forgot_Your_Password;
        public static string Login_No_Registered => Resource.Login_No_Registered;
        public static string Login_Button_Login => Resource.Login_Button_Login;
        public static string Login_Footer => Resource.Login_Footer;
        #endregion

        #region BUTTONS
        public static string Button_Add => Resource.Button_Add;
        public static string Button_Skip => Resource.Button_Skip;
        #endregion

        #region LABELS
        public static string Welcome_Label => Resource.Welcome;
        public static string Welcome_Summary => Resource.Welcome_Summary;
        #endregion

        #region SYSTEM
        public static string System_Process_Error => Resource.System_Process_Error;
        public static string System_From => Resource.System_From;
        public static string System_To => Resource.System_To;
        public static string System_Request_Success => Resource.System_Request_Success;
        public static string System_Request_Cancel => Resource.System_Request_Cancel;
        public static string Fingerprint_Authenticate_To_Continue => Resource.Fingerprint_Authenticate_To_Continue;
        public static string Fingerprint_Authenticate_Error => Resource.Fingerprint_Authenticate_Error;
        public static string System_Date_Format_Default => Resource.System_Date_Format_Default;
        public static string System_DateTime_Format_Default => Resource.System_DateTime_Format_Default;
        public static string System_Current_Password => Resource.System_Current_Password;
        public static string System_New_Password => Resource.System_New_Password;
        #endregion

        #region COLLECT
        public static string Collect_Title => Resource.Collect_Title;
        public static string Collect_Message_Bottom => Resource.Collect_Message_Bottom;
        #endregion

        #region DETAILMOVIE
        public static string DetailMoview_BuyTicket => Resource.DetailMoview_BuyTicket;
        #endregion

        #region BUYTICKET
        public static string BuyTicket_CardNumber => Resource.BuyTicket_CardNumber;
        public static string BuyTicket_CardHolder => Resource.BuyTicket_CardHolder;
        public static string BuyTicket_CardExpiration => Resource.BuyTicket_CardExpiration;
        public static string BuyTicket_CardCVV => Resource.BuyTicket_CardCVV;
        public static string BuyTicket_Month_Format => Resource.BuyTicket_Month_Format;
        public static string BuyTicket_Year_Format => Resource.BuyTicket_Year_Format;
        #endregion
    }
}
