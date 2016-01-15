
namespace Jabberwocky.Toolkit.Path
{
  using System;
  using System.IO;
  using String;

  /// <summary>
  /// Provides methods implementing path like functionality.
  /// </summary>
  public static class PathOperations
  {
    #region Methods
    /// <summary>
    /// Will complete a directory path by appending a seperator character (\) if it is missing.
    /// </summary>
    /// <param name="directoryPath">Directory path to append the seperator character to.</param>
    /// <returns>Directory path with seperator character appended.</returns>
    public static String CompleteDirectoryPath(String directoryPath)
    {
      directoryPath.VerifyThatStringIsNotNullAndNotEmpty("Parameter 'directoryPath' is null or empty.");
      if (directoryPath[directoryPath.Length - 1] != Path.DirectorySeparatorChar)
      {
        directoryPath += Path.DirectorySeparatorChar;
      }

      return directoryPath;
    }
    #endregion
  }
}
