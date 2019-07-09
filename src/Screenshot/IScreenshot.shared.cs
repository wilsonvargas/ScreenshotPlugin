using System.Threading.Tasks;

namespace Plugin.Screenshot
{
    public interface IScreenshot
    {
        Task<string> CaptureAndSaveAsync();

        Task<byte[]> CaptureAsync();
    }
}
