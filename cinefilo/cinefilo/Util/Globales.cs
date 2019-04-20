namespace cinefilo.Util
{
    public class Globales
    {

        #region HOST_CONNECTION
        //public static string WS_Url = "http://192.168.0.67:8080/api/";
        public static string WS_Url = "https://cineperu.herokuapp.com/api/";
        public static string HOSTNAME_TRUST = "cine.com";
        public static string ENCRYPTION_KEY = "ABCDEFGHIJKLMNOPQRSTUVWXYZ000001";
        public static string Logo_QR = "logo";
        #endregion

        #region DATE_FORMAT
        public static string DateFormatMets = "yyyy-MM-dd";
        #endregion

        #region MENU_DRAWER
        public static string MENU_DRAWER_PRINCIPAL_PAGE = "PrincipalPage";
        public static string MENU_DRAWER_COLLECT_PAGE = "CollectPage";
        public static string MENU_DRAWER_TRIVIA_PAGE = "TriviaPage";
        public static string MENU_DRAWER_LOGIN_PAGE = "LoginPage";
        #endregion

        #region METHODS_WEBSERVICE
        public static string WS_UserAuthenticate = "WS_UserAuthenticate";
        public static string WS_ProcessQR = "WS_ProcessQR";
        public static string WS_GetListMovie = "WS_GetListMovie";
        public static string WS_GetListTrivia = "WS_GetListTrivia";
        public static string WS_RegisterIntentTrivia = "WS_RegisterIntentTrivia";
        public static string WS_GetTriviaUser = "WS_GetTriviaUser";
        public static string WS_SendException = "WS_SendException";
        #endregion
    }
}
