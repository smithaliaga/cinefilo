using System.Drawing;
using CoreAnimation;
using CoreGraphics;
using UIKit;
using cinefilo.iOS.Implementations;
using cinefilo.Util;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace cinefilo.iOS.Implementations
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;

            var element = (CustomEntry)this.Element;

            /*************************IMAGE**********************/

            var textField = this.Control;
            if (!string.IsNullOrEmpty(element.Image))
            {
                switch (element.ImageAlignment)
                {
                    case EnumImageAlignment.Left:
                        textField.LeftViewMode = UITextFieldViewMode.Always;
                        textField.LeftView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                    case EnumImageAlignment.Right:
                        textField.RightViewMode = UITextFieldViewMode.Always;
                        textField.RightView = GetImageView(element.Image, element.ImageHeight, element.ImageWidth);
                        break;
                }
            }

            textField.BorderStyle = UITextBorderStyle.None;
            CALayer bottomBorder = new CALayer
            {
                Frame = new CGRect(0.0f, element.HeightRequest - 1, this.Frame.Width, 1.0f),
                BorderWidth = 2.0f,
                BorderColor = element.LineColor.ToCGColor()
            };

            textField.Layer.AddSublayer(bottomBorder);
            textField.Layer.MasksToBounds = true;

            /*************************RETURN TYPE**********************/
            SetReturnType(element);
            // Editor Action is called when the return button is pressed
            Control.ShouldReturn += (UITextField tf) => {
                element?.InvokeCompleted();
                return true;
            };
        }

        private UIView GetImageView(string imagePath, int height, int width)
        {
            var uiImageView = new UIImageView(UIImage.FromBundle(imagePath))
            {
                Frame = new RectangleF(0, 0, width, height)
            };
            UIView objLeftView = new UIView(new System.Drawing.Rectangle(0, 0, width + 10, height));
            objLeftView.AddSubview(uiImageView);

            return objLeftView;
        }

        void SetReturnType(CustomEntry entry)
        {
            var type = entry.ReturnType;

            switch (type)
            {
                case EnumReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case EnumReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case EnumReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case EnumReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case EnumReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
    }
}