using Plugin.Screenshot.Abstractions;
using System;

namespace Plugin.Screenshot
{
  /// <summary>
  /// Cross platform Screenshot implemenations
  /// </summary>
  public class CrossScreenshot
  {
    static Lazy<IScreenshot> Implementation = new Lazy<IScreenshot>(() => CreateScreenshot(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

    /// <summary>
    /// Current settings to use
    /// </summary>
    public static IScreenshot Current
    {
      get
      {
        var ret = Implementation.Value;
        if (ret == null)
        {
          throw NotImplementedInReferenceAssembly();
        }
        return ret;
      }
    }

    static IScreenshot CreateScreenshot()
    {
#if PORTABLE
        return null;
#else
        return new ScreenshotImplementation();
#endif
    }

    internal static Exception NotImplementedInReferenceAssembly()
    {
      return new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
    }
  }
}
