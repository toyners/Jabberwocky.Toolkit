
namespace Jabberwocky.Toolkit.Logging
{
  /// <summary>
  /// Provides functionality to create instances of ILogger type.
  /// </summary>
  public interface ILoggerFactory
  {
    /// <summary>
    /// Creates a new instance of the ILogger type.
    /// </summary>
    /// <returns>ILogger instance.</returns>
    ILogger Create();
  }
}
