
namespace Jabberwocky.Toolkit.IO
{
  using System;

  /// <summary>
  /// Provides methods and properties for a stream reader. Includes a read line method.
  /// </summary>
  public interface IStreamReader : IDisposable
  {
    #region Properties
    /// <summary>
    /// Gets a boolean indicating whether the reading position is at the end of the stream.
    /// </summary>
    Boolean EndOfStream { get; }

    /// <summary>
    /// Gets or sets the position to read from within the stream.
    /// </summary>
    Int64 Position { get; set; }
      
    /// <summary>
    /// Gets the full name of the file. 
    /// </summary>
    String Name { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Reads a line from the stream.
    /// </summary>
    /// <returns>The line from the stream.</returns>
    String ReadLine();

    /// <summary>
    /// Closes and disposes the underlying stream.
    /// </summary>
    void Close();
    #endregion
  }
}
