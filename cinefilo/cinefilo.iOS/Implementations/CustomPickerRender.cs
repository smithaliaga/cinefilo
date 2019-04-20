using cinefilo.iOS.Implementations;
using Xamarin.Forms;
using cinefilo.Util;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRender))]
namespace cinefilo.iOS.Implementations {
    public class CustomPickerRender : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e) {
            base.OnElementChanged(e);

            var picker = Element as Picker;

            if (Control != null && picker != null)
            { }
        }
    }
}