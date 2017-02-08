
namespace Jabberwocky.Toolkit.Logging
{
  using System;

  /// <summary>
  /// Provides functionality to create instances of ILogger type.
  /// </summary>
  public interface ILoggerFactory
  {
    /// <summary>
    /// Creates a new instance of the ILogger type.
    /// </summary>
    /// <param name="name">Name of ILogger instance.</param>
    /// <returns>ILogger instance.</returns>
    ILogger Create(String name);
  }
}
