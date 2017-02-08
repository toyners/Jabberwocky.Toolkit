
namespace Jabberwocky.Toolkit.Logging
{
  using System;

  /// <summary>
  /// Provides functionality for logging general messages and exceptions.
  /// </summary>
  public interface ILogger : IDisposable
  {
    /// <summary>
    /// Logs a general message.
    /// </summary>
    /// <param name="message">Message content to be logged.</param>
    void Message(String message);

    /// <summary>
    /// Logs a general message. Allows an optional line break to be appended to the message content.
    /// </summary>
    /// <param name="message">Message content to be logged.</param>
    /// <param name="lineBreak">True to append line break, otherwise false.</param>
    void Message(String message, Boolean lineBreak);

    /// <summary>
    /// Logs an exception message.
    /// </summary>
    /// <param name="message">Message content to be logged.</param>
    void Exception(String message);
  }
}
