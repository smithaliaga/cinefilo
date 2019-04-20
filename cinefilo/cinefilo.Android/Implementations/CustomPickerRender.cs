using Android.Content;
using Android.Graphics.Drawables;
using cinefilo.Droid.Implementations;
using cinefilo.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRender))]
namespace cinefilo.Droid.Implementations {
    public class CustomPickerRender : PickerRenderer {
        public CustomPickerRender(Context context) : base(context) { }
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);
            if (e.OldElement == null) {
                // white background
                GradientDrawable MyBgColorDrawable = new GradientDrawable();
                MyBgColorDrawable.SetColor(Color.White.ToAndroid());

                // rounded corners
                GradientDrawable MyCornerRadiusDrawable = new GradientDrawable();
                MyCornerRadiusDrawable.SetCornerRadius(7);

                // combine all
                Drawable[] dArray = { MyBgColorDrawable, MyCornerRadiusDrawable };
                var layers = new LayerDrawable(dArray);

                // Set background
                Control.SetBackground(layers);
            }
        }
    }
}