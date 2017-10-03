using Plugin.Screenshot.Abstractions;
using Android.App;
using Android.Graphics;
using System.IO;
using System;
using Plugin.CurrentActivity;


namespace Plugin.Screenshot
{
  /// <summary>
  /// Implementation for Feature
  /// </summary>
  public class ScreenshotImplementation : IScreenshot
  {
        Activity Context =>
        CrossCurrentActivity.Current.Activity ?? throw new NullReferenceException("Current Context/Activity is null, ensure that the MainApplication.cs file is setting the CurrentActivity in your source code so the Screenshot can use it.");

        public async System.Threading.Tasks.Task<byte[]> CaptureAsync()
        {
            if (Context == null)
            {
                throw new Exception("You have to set Screenshot.Activity in your Android project");
            }
            var view = Context.Window.DecorView;
            view.DrawingCacheEnabled = true;

            Bitmap bitmap = view.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return bitmapData;
        }
    }
}