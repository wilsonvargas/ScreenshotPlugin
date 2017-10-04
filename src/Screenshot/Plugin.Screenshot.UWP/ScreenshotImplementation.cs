using Plugin.Screenshot.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace Plugin.Screenshot
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class ScreenshotImplementation : IScreenshot
    {
        public async Task<byte[]> CaptureAsync()
        {
            var rtb = new RenderTargetBitmap();
            await rtb.RenderAsync(Window.Current.Content);

            var pixelBuffer = await rtb.GetPixelsAsync();
            byte[] pixels;
            CryptographicBuffer.CopyToByteArray(pixelBuffer, out pixels);

            // Useful for rendering in the correct DPI
            var displayInformation = DisplayInformation.GetForCurrentView();

            var stream = new InMemoryRandomAccessStream();
            var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
            encoder.SetPixelData(BitmapPixelFormat.Bgra8,
            BitmapAlphaMode.Premultiplied,
            (uint)rtb.PixelWidth,
            (uint)rtb.PixelHeight,
            displayInformation.RawDpiX,
            displayInformation.RawDpiY,
            pixels);

            await encoder.FlushAsync();
            stream.Seek(0);

            var readStram = stream.AsStreamForRead();
            var bytes = new byte[readStram.Length];
            readStram.Read(bytes, 0, bytes.Length);

            return bytes;
        }
    }
}