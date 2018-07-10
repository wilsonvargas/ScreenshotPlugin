using Foundation;
using Plugin.Screenshot.Abstractions;
using System;
using System.Threading.Tasks;
using UIKit;

namespace Plugin.Screenshot
{
    /// <summary>
    /// Implementation for Screenshot
    /// </summary>
    public class ScreenshotImplementation : IScreenshot
    {
        public async Task<string> CaptureAndSaveAsync()
        {
            var bytes = await CaptureAsync();
            var documentsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            try
            {
                string localPath = System.IO.Path.Combine(documentsDirectory, "Screnshot-" + date + ".png");

                var chartImage = new UIImage(NSData.FromArray(bytes));
                chartImage.SaveToPhotosAlbum((image, error) =>
                {
                    //you can retrieve the saved UI Image as well if needed using
                    if (error != null)
                    {
                        Console.WriteLine(error.ToString());
                    }
                });
                return localPath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
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
