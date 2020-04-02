using Android;
using Android.App;
using Android.Graphics;
using Android.OS;
using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;
using PermissionStatus = Xamarin.Essentials.PermissionStatus;

namespace Plugin.Screenshot
{
    /// <summary>
    /// Interface for Screenshot
    /// </summary>
    public class ScreenshotImplementation : IScreenshot
    {
        private Activity Context => Platform.CurrentActivity;

        public async Task<string> CaptureAndSaveAsync()
        {
            if (!(await RequestStoragePermission()))
            {
                var missingPermissions = string.Join(", ", nameof(StoragePermission));
                throw new Exception($"{missingPermissions} permission(s) are required.");
            }
            var bytes = await CaptureAsync();
            Java.IO.File picturesFolder = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDcim);
            string date = DateTime.Now.ToString().Replace("/", "-").Replace(":", "-");
            try
            {
                string filePath = System.IO.Path.Combine(picturesFolder.AbsolutePath + "/Camera", "Screnshot-" + date + ".png");
                using (System.IO.FileStream SourceStream = System.IO.File.Open(filePath, System.IO.FileMode.OpenOrCreate))
                {
                    SourceStream.Seek(0, System.IO.SeekOrigin.End);
                    await SourceStream.WriteAsync(bytes, 0, bytes.Length);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<byte[]> CaptureAsync()
        {
            await Task.Delay(1000);
            if (Context == null)
            {
                throw new Exception("You have to set Screenshot. Activity in your Android project");
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
            view.DrawingCacheEnabled = false;
            return bitmapData;
        }

        async Task<bool> RequestStoragePermission()
        {
            //We always have permission on anything lower than marshmallow.
            if ((int)Build.VERSION.SdkInt < 23)
                return true;

            var status = await CheckStatusAsync<StoragePermission>();
            if (status != PermissionStatus.Granted)
            {
                Console.WriteLine("Does not have storage permission granted, requesting.");
                var result = await RequestAsync<StoragePermission>();
                if (result != PermissionStatus.Granted)
                {
                    Console.WriteLine("Storage permission Denied.");
                    return false;
                }
            }
            return true;
        }
    }
}
