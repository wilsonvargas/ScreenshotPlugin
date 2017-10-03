using Plugin.Screenshot.Abstractions;
using System;
using System.Threading.Tasks;

namespace Plugin.Screenshot
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class ScreenshotImplementation : IScreenshot
    {
        public Task<byte[]> CaptureAsync()
        {
            throw new NotImplementedException();
        }
    }
}