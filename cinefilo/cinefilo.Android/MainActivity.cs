using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using Plugin.MediaManager.Forms.Android;
using Plugin.Permissions;
using System;

namespace cinefilo.Droid
{
    [Activity(Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity {

        protected override void OnCreate(Bundle bundle)
        {

            CrossFingerprint.SetCurrentActivityResolver(()=> CrossCurrentActivity.Current.Activity);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            base.OnCreate(bundle);

            //plugin para inicializar libreria de popup
            Rg.Plugins.Popup.Popup.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();

            VideoViewRenderer.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults) {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

