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
        public async Task<byte[]> CaptureAsync()
        {
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