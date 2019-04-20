namespace cinefilo.Util {
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;
    using Plugin.DeviceInfo;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using System.IO;
    using cinefilo.Views;
    using cinefilo.Helpers;
    using cinefilo.ViewModels;
    using cinefilo.Models.Local;
    using Plugin.DeviceInfo.Abstractions;

    public class Util {
        public static string GetDeviceLanguage() {
            var lang = System.Globalization.CultureInfo.CurrentUICulture;
            var country = System.Globalization.RegionInfo.CurrentRegion;
            return lang.TwoLetterISOLanguageName + "-" + country.TwoLetterISORegionName;
        }

        public static async Task ShowMessage(string title,
            string message,
            string buttonText,
            Action afterHideCallback) {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonText);

            if (afterHideCallback != null)
            {
                afterHideCallback?.Invoke();
            }
        }

        public static async Task ShowMessage(string title,
            string message,
            Action afterHideCallback)
        {
            await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                Languages.Accept);

            if (afterHideCallback != null)
            {
                afterHideCallback?.Invoke();
            }
        }

        public static async Task<bool> QuestAlert(string title, string message) {
            var answer = await Application.Current.MainPage.DisplayAlert(title, message, Languages.Accept, Languages.Cancel);
            return answer;
        }

        public static async Task<bool> ShowConfirmDialog(string title,
            string message,
            string buttonYesText,
            string buttonNoText,
            Action afterHideCallback)
        {
            var result = await Application.Current.MainPage.DisplayAlert(
                title,
                message,
                buttonYesText,
                buttonNoText);

            if (afterHideCallback != null)
            {
                afterHideCallback?.Invoke();
            }

            if (result)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string GetDeviceInfo()
        {
            string returnJson = "";
            try
            {
                string ID = CrossDeviceInfo.Current.Id;
                string DeviceName = CrossDeviceInfo.Current.DeviceName;
                string Idiom = CrossDeviceInfo.Current.Idiom.ToString();
                string Model = CrossDeviceInfo.Current.Model;
                string Platform = CrossDeviceInfo.Current.Platform.ToString();
                string AppVersion = CrossDeviceInfo.Current.AppVersion;
                string AppBuild = CrossDeviceInfo.Current.AppBuild;
                string VersionSO = CrossDeviceInfo.Current.VersionNumber.ToString();
                string Manufacturer = CrossDeviceInfo.Current.Manufacturer;

                Dictionary<string, string> parameters = new Dictionary<string, string>
                {
                    { "ID", ID },
                    { "DeviceName", DeviceName },
                    { "Idiom", Idiom },
                    { "Model", Model },
                    { "Platform", Platform },
                    { "AppVersion", AppVersion },
                    { "AppBuild", AppBuild },
                    { "VersionSO", VersionSO },
                    { "Manufacturer", Manufacturer }
                };

                returnJson = JsonConvert.SerializeObject(parameters, Formatting.Indented);
            }
            catch (Exception)
            {
            }
            return returnJson;
        }

        public static ImageSource GetImageSourceFromBase64(string base64picture)
        {
            ImageSource imageSource = null;
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64picture);
                imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            catch(Exception)
            {
            }
            return imageSource;
        }

        public static void ShowMasterPage()
        {
            Application.Current.MainPage = new MasterPage();
        }

        public static void ShowNavigationPage(Page page) {
            try
            {
                Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page));
            }
            catch (Exception)
            {
            }
        }

        public static void HideNavigationPage()
        {
            Application.Current.MainPage.Navigation.PopModalAsync();
        }

        public async static void HideAllModals()
        {
            try
            {
                int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
                for (int currModal = 0; currModal < numModals; currModal++)
                {
                    await Application.Current.MainPage.Navigation.PopModalAsync();
                }
            }
            catch (Exception)
            {
            }
        }

        public static void ShowNavigationPageDefault(Page page)
        {
            try
            {
                MainViewModel.GetInstace().Navigation.PushAsync(page);
            } catch (Exception)
            {
            }
        }

        public async static void HideAllModalsDefault()
        {
            try
            {
                int numModals = MainViewModel.GetInstace().Navigation.NavigationStack.Count;

                for (int currModal = 0; currModal < numModals; currModal++)
                {
                    await MainViewModel.GetInstace().Navigation.PopAsync();
                }
            }
            catch (Exception)
            {
            }
        }

        public static int ProcessAllowCard(bool? allowed)
        {
            if (allowed == null)
            {
                return -1;
            }
            else if (allowed.Value)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static DateTime? ConvertStringToDateFormat(string date, string dateFormat)
        {
            DateTime? datetime = null;
            try
            {
                datetime = DateTime.ParseExact(date, dateFormat,
                                       System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
            }
            return datetime;
        }

        public static string ChangeStringToDateFormato(string date, string dateFormatOrigin, string dateFormatFinal)
        {
            string datetime = "";
            try
            {
                datetime = DateTime.ParseExact(date, dateFormatOrigin,
                                       System.Globalization.CultureInfo.InvariantCulture).ToString(dateFormatFinal);
            }
            catch (Exception)
            {
            }
            return datetime;
        }

        public static DateTime? ConvertUtcToLocalDate(DateTime dateTimeUTC)
        {
            DateTime? cstTime = null;
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                cstTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, cstZone);
            }
            catch (Exception)
            {
            }
            return cstTime;
        }

        public static string ConvertUtcToLocalDateFormat(DateTime dateTimeUTC, string dateFormat)
        {
            string formatDateTime = "";
            try
            {
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZoneInfo.Local.Id);
                DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(dateTimeUTC, cstZone);
                formatDateTime = cstTime.ToString(dateFormat);
            }
            catch (Exception)
            {
            }
            return formatDateTime;
        }

        public static List<EntityModalPicker> GetTimeZones()
        {
            List<EntityModalPicker> list = new List<EntityModalPicker>();
            try
            {
                foreach (var item in TimeZoneInfo.GetSystemTimeZones())
                {
                    list.Add(new EntityModalPicker(item.DisplayName, item.DisplayName));
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public static string GetDeviceId()
        {
            return CrossDeviceInfo.Current.Id;
        }

        public static int GetIconSizeDefault()
        {
            if (CrossDeviceInfo.Current.Platform == Platform.Android)
            {
                //return App.ScreenInches < 4.5 ? 15 : 30;
                return 40;
            }
            else
            {
                return 30;
            }
        }
    }
}
