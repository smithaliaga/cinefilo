using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Views.InputMethods;
using Android.Widget;
using cinefilo.Droid.Implementations;
using cinefilo.Helpers;
using cinefilo.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace cinefilo.Droid.Implementations
{
    public class CustomEntryRenderer : EntryRenderer
    {
        CustomEntry element;

        public CustomEntryRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            element = (CustomEntry)this.Element;

            /*******************IMAGE*******************/
            var editText = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case EnumImageAlignment.Left:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(element.Image), null, null, null);
                        break;
                    case EnumImageAlignment.Right:
                        editText.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(element.Image), null);
                        break;
                }
            }
            editText.CompoundDrawablePadding = 25;
            Control.Background.SetColorFilter(element.LineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);

            /*******************RETURN TYPE*******************/
            SetReturnType(element);
            // Editor Action is called when the return button is pressed
            Control.EditorAction += (object sender, TextView.EditorActionEventArgs args) =>
            {
                if (element?.ReturnType != EnumReturnType.Next)
                    element?.Unfocus();

                // Call all the methods attached to base_entry event handler Completed
                element?.InvokeCompleted();
            };
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, element.ImageWidth * 2, element.ImageHeight * 2, true));
        }

        void SetReturnType(CustomEntry entry)
        {
            var type = entry.ReturnType;

            switch (type)
            {
                case EnumReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel(Languages.Editor_ReturnType_Go, ImeAction.Go);
                    break;
                case EnumReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel(Languages.Editor_ReturnType_Next, ImeAction.Next);
                    break;
                case EnumReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel(Languages.Editor_ReturnType_Send, ImeAction.Send);
                    break;
                case EnumReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel(Languages.Editor_ReturnType_Search, ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel(Languages.Editor_ReturnType_Done, ImeAction.Done);
                    break;
            }
        }
    }
}