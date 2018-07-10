using System.Threading.Tasks;

namespace Plugin.Screenshot.Abstractions
{
    /// <summary>
    /// Interface for Screenshot
    /// </summary>
    public interface IScreenshot
    {
        Task<string> CaptureAndSaveAsync();

        Task<byte[]> CaptureAsync();
    }
}
