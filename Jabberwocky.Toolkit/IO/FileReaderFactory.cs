
namespace Jabberwocky.Toolkit.IO
{
  using System;

  /// <summary>
  /// Factory for creating FileReader objects.
  /// </summary>
  public class FileReaderFactory : IStreamReaderFactory
  {
    /// <summary>
    /// Creates a FileReader based on the file path.
    /// </summary>
    /// <param name="filePath">Full path to the file.</param>
    /// <returns>Returns stream reader object.</returns>
    public IStreamReader CreateStreamReader(String filePath)
    {
      return new FileReader(filePath);
    }
  }
}
