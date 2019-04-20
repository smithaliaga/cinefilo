using Foundation;
using System;
using System.IO;
using UIKit;
using cinefilo.Interfaces;
using cinefilo.iOS.Implementations;
using Xamarin.Forms.Platform.iOS;
using ZXing.Common;
using ZXing;
using ZXing.QrCode.Internal;
using ZXing.Mobile;
using cinefilo.Util;

[assembly: Xamarin.Forms.Dependency(typeof(ShareClass))]
namespace cinefilo.iOS.Implementations {
    public class ShareClass : UIViewController, IShare
    {

        public void ShareImage(string subject, string filename, byte[] data)
        {
            var handler = new ImageLoaderSourceHandler();
            var uiImage = CopyTo(data);
            var img = FromObject(uiImage);
            var mess = FromObject(subject);
            try
            {
                var activityItems = new[] { img };
                ShareAction(activityItems);
            }
            catch (Exception)
            {
                var activityItems = new[] { img };
                ShareAction(activityItems);
            }

        }

        public void DeleteFiles()
        {

        }

        private void ShareAction(NSObject[] items)
        {
            var activityController = new UIActivityViewController(items, null);
            var viewController = GetCurrentView();
            NSString[] excludedActivityTypes = null;
            if (excludedActivityTypes != null && excludedActivityTypes.Length > 0)
                activityController.ExcludedActivityTypes = excludedActivityTypes;
            if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                if (activityController.PopoverPresentationController != null)
                    activityController.PopoverPresentationController.SourceView = viewController.View;
            viewController.PresentViewControllerAsync(activityController, true);
        }


        private UIViewController GetCurrentView(UIViewController controller = null)
        {
            controller = controller ?? UIApplication.SharedApplication.KeyWindow.RootViewController;
            if (controller.PresentedViewController == null)
                return controller;
            switch (controller.PresentedViewController)
            {
                case UINavigationController _:
                    return ((UINavigationController)controller.PresentedViewController).VisibleViewController;
                case UITabBarController _:
                    return ((UITabBarController)controller.PresentedViewController).SelectedViewController;
            }
            return GetCurrentView(controller.PresentedViewController);
        }

        public byte[] ConvertImageStream(string text)
        {
            int width = 600; int height = 600;
            var barcodeWriter = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 3,
                }
            };

            barcodeWriter.Options.Hints.Add(ZXing.EncodeHintType.ERROR_CORRECTION, ErrorCorrectionLevel.M);
            barcodeWriter.Renderer = new BitmapRenderer();

            var image2 = UIImage.FromFile(Globales.Logo_QR);
            var image1 = barcodeWriter.Write(text);

            UIGraphics.BeginImageContext(new CoreGraphics.CGSize(width, height));

            image1.Draw(new CoreGraphics.CGRect(
                0, 0, width, height));

            double iconFinalSizeFactor = 1.5;

            image2.Draw(new CoreGraphics.CGRect(
                width / 2 - image2.Size.Width * iconFinalSizeFactor / 2,
                height / 2 - image2.Size.Height * iconFinalSizeFactor / 2,
                image2.Size.Width * iconFinalSizeFactor,
                image2.Size.Height * iconFinalSizeFactor));

            var NewImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();


            var stream = NewImage.AsPNG().AsStream();
            stream.Position = 0;
            return ReadFully(stream);
        }

        private UIImage CopyTo(byte[] src)
        {
            return UIImage.LoadFromData(NSData.FromArray(src));
        }

        private byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }


    }
}