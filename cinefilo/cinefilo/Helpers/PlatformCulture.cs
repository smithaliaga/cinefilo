namespace cinefilo.Helpers
{
    using System;

    public class PlatformCulture
    {
        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier");
            }

            PlatformString = platformCultureString.Replace("_","-");
            var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = PlatformString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = PlatformString;
                LocaleCode = "";
            }
        }

        public string PlatformString
        {
            get;
            set;
        }

        public string LanguageCode
        {
            get;
            set;
        }

        public string LocaleCode
        {
            get;
            set;
        }

        public override string ToString()
        {
            return PlatformString;
        }
    }
}
