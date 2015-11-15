
namespace Jabberwocky.Toolkit.IO
{
  using System;

  /// <summary>
  /// Provides methods and properties for creating stream reader objects.
  /// </summary>
  public interface IStreamReaderFactory
  {
    /// <summary>
    /// Creates the stream reader based on the file path.
    /// </summary>
    /// <param name="filePath">Full path to the file.</param>
    /// <returns>Returns stream reader object.</returns>
    IStreamReader CreateStreamReader(String filePath);
  }
}
