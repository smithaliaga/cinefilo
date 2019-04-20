namespace cinefilo.Util
{
    using Xamarin.Forms;

    public class Session
    {

        #region Singleton
        private static Session instance = null;

        public static Session GetInstace()
        {
            if (instance == null)
            {
                return new Session();
            }
            return instance;
        }
        #endregion

        #region Properties
        public string Token { get; set; }
        public ImageSource UserImage { get; set; }
        #endregion
    }
}
