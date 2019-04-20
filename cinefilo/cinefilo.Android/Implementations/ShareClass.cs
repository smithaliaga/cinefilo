using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using cinefilo.Droid.Implementations;
using cinefilo.Interfaces;
using cinefilo.Util;
using Xamarin.Forms;
using ZXing.Common;
using ZXing.Mobile;
using ZXing.QrCode.Internal;

[assembly: Dependency(typeof(ShareClass))]
namespace cinefilo.Droid.Implementations {
    public class ShareClass : Activity, IShare {
        public void ShareImage(string subject, string filename, byte[] data)
        {
            try
            {
                var intent = new Intent(Intent.ActionSend);
                intent.SetType("image/jpeg");
                var bitmap = CopyTo(data);

                Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.App.Application.Context.ApplicationContext.PackageName
                    + Java.IO.File.Separator + filename);
                if (!path.ParentFile.Exists())
                {
                    path.ParentFile.Mkdirs();
                }
                using (FileStream os = new FileStream(path.AbsolutePath, FileMode.Create))
                {
                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, os);
                }
                intent.AddFlags(ActivityFlags.GrantReadUriPermission);
                intent.AddFlags(ActivityFlags.GrantWriteUriPermission);
                intent.PutExtra(Intent.ExtraStream, Android.Net.Uri.FromFile(path));
                Android.App.Application.Context.StartActivity(Intent.CreateChooser(intent, subject).AddFlags(ActivityFlags.NewTask));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DeleteFiles()
        {
            try
            {
                System.Threading.Tasks.Task.Run(async () =>
                {
                    Java.IO.File path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.App.Application.Context.ApplicationContext.PackageName);
                    if (path.Exists())
                    {
                        Java.IO.File[] files = await path.ListFilesAsync();
                        for (int i = 0; i < files.Length; i++)
                        {
                            files[i].Delete();
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        byte[] IShare.ConvertImageStream(string content)
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

            Drawable icon = Android.App.Application.Context.GetDrawable(Android.App.Application.Context.Resources.GetIdentifier(Globales.Logo_QR, "drawable", Android.App.Application.Context.ApplicationContext.PackageName));
            Bitmap image1 = barcodeWriter.Write(content);
            Bitmap image2 = ((BitmapDrawable)icon).Bitmap;
            Bitmap bitmap = overlayBitmapToCenter(image1, image2);

            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
            stream.Position = 0;
            return ReadFully(stream);
        }

        public static Bitmap overlayBitmapToCenter(Bitmap bitmapQR, Bitmap bitmapLogo)
        {
            Bitmap overlayBitmap = Bitmap.CreateBitmap(bitmapQR.Width, bitmapQR.Height, bitmapQR.GetConfig());
            Canvas canvas = new Canvas(overlayBitmap);
            int canvasWidth = canvas.Width;
            int canvasHeight = canvas.Height;
            canvas.DrawBitmap(bitmapQR, new Matrix(), null);

            Bitmap resizeLogo = Bitmap.CreateScaledBitmap(bitmapLogo, 77, 77, true);
            int centreX = (canvasWidth - resizeLogo.Width) / 2;
            int centreY = (canvasHeight - resizeLogo.Height) / 2;
            canvas.DrawBitmap(resizeLogo, centreX, centreY, null);

            return overlayBitmap;
        }

        public Bitmap CopyTo(byte[] data)
        {
            return BitmapFactory.DecodeByteArray(data, 0, data.Length);
        }

        public byte[] ReadFully(Stream input)
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