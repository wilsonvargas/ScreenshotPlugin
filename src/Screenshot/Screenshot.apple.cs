using Foundation;
using System;
using System.IO;
using System.Threading.Tasks;
using UIKit;

namespace Plugin.Screenshot
{
    /// <summary>
    /// Interface for Screenshot
    /// </summary>
    public class ScreenshotImplementation : IScreenshot
    {
        public async Task<string> CaptureAndSaveAsync()
        {
            byte[] bytes = await CaptureAsync();
            string documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            string imageFilename = System.IO.Path.Combine(documentsDirectory + "/AppPhoto", "Screnshot-" + date + ".jpg");
            string result = string.Empty;
            UIImage imageData = new UIImage(NSData.FromArray(bytes));
            imageData.SaveToPhotosAlbum((uiImage, nsError) =>
            {
                if (nsError != null) {
                    result = nsError.LocalizedDescription;
                }
                else {
                    result = imageFilename;
                }
            });

            return result;
        }

        public async Task<byte[]> CaptureAsync()
        {
            await Task.Delay(1000);
            var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;
            UIGraphics.BeginImageContext(view.Frame.Size);
            view.DrawViewHierarchy(view.Frame, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            using (var imageData = image.AsPNG())
            {
                var bytes = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
                return bytes;
            }
        }
    }
}
