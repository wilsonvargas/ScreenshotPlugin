using System;
using System.Threading.Tasks;

namespace Plugin.Screenshot.Abstractions
{
  /// <summary>
  /// Interface for Screenshot
  /// </summary>
  public interface IScreenshot
  {
        Task<byte[]> CaptureAsync();
        Task<string> CaptureAndSaveAsync();
    }
}
