namespace cinefilo
{
    using System.Collections.Generic;
    using cinefilo.Helpers;
    using cinefilo.Views;
    using Xam.Plugin.SimpleAppIntro;
    using Xamarin.Forms;

    public partial class MainPage : ContentPage {
		public MainPage() {
			InitializeComponent();
            Open_Static_Clicked();
        }

        private void Open_Static_Clicked()
        {
            var welcomePage = new SimpleAppIntro(new List<object>() {
                new Slide(new SlideConfig(Languages.Welcome_Label, Languages.Welcome_Summary, "logo.png",
                "#f68f1e", "#FFFFFF", "#FFFFFF",  FontAttributes.Bold, FontAttributes.None, 24, 16)),
            })
            {
                // Properties
                ShowPositionIndicator = true,
                ShowSkipButton = true,
                ShowNextButton = true,
                DoneText = Languages.Editor_ReturnType_Go,
                NextText = Languages.Editor_ReturnType_Next,
                SkipText = Languages.Button_Skip,

                // Theming
                BarColor = "#607D8B",
                SkipButtonBackgroundColor = "#100FFFFFF",
                DoneButtonBackgroundColor = "#100FFFFFF",
                NextButtonBackgroundColor = "#100FFFFFF",

                // Callbacks
                OnSkipButtonClicked = OnSkipButtonClicked,
                OnDoneButtonClicked = OnDoneButtonClicked,
                
            };

            Navigation.PushModalAsync(welcomePage);
        }

        private void OnSkipButtonClicked() {
            goLogin();
        }

        private void OnDoneButtonClicked() {
            goLogin();
        }

        private void goLogin() {
            Application.Current.SavePropertiesAsync();
            Application.Current.MainPage = new NavigationPage(new LoginPage()) {
                BarBackgroundColor = Color.Black,
            };
        }
    }
}
