namespace cinefilo.Util
{
    using System;
    using Xamarin.Forms;

    public class Shared
    {

        private static string KEY_IS_FIRST_TIME = "IS_FIRST_TIME";
        private static string KEY_USER = "USER";

        public static bool GetIsFirstTime()
        {
            bool isFirstTime = false;
            try
            {
                if (Application.Current.Properties.ContainsKey(KEY_IS_FIRST_TIME))
                {
                    bool? nullableBool = Application.Current.Properties[KEY_IS_FIRST_TIME] as bool?;
                    isFirstTime = nullableBool.HasValue ? nullableBool.Value : false;
                }
                else
                {
                    Application.Current.Properties[KEY_IS_FIRST_TIME] = false;
                    isFirstTime = true;
                }
            }
            catch(Exception)
            {
            }
            return isFirstTime;
        }

        public static string GetUser()
        {
            string retorno = "";
            try
            {
                if (Application.Current.Properties.ContainsKey(KEY_USER))
                {
                    retorno = Application.Current.Properties[KEY_USER] as string;
                }
            }
            catch (Exception)
            {
            }
            return retorno;
        }

        public static void SetUser(string user)
        {
            try
            {
                Application.Current.Properties[KEY_USER] = user;
            }
            catch (Exception)
            {
            }
        }
    }
}
