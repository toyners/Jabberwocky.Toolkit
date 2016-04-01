
namespace Jabberwocky.Toolkit.IO
{
  using System;
  using System.IO;
  using Jabberwocky.Toolkit.String;

  /// <summary>
  /// Helper methods for directory functionality. 
  /// </summary>
  public static class DirectoryOperations
  {
    /// <summary>
    /// Checks that the directory exists. If not then it creates the directory.
    /// </summary>
    /// <param name="directoryPath">Full path to the directory to check.</param>
    public static void EnsureDirectoryExists(String directoryPath)
    {
      directoryPath.VerifyThatStringIsNotNullAndNotEmpty("Parameter 'directoryPath' is null or empty.");
      if (!Directory.Exists(directoryPath))
      {
        Directory.CreateDirectory(directoryPath);
      }
    }

    /// <summary>
    /// Deletes all files and sub directories from the directory. If the directory does not exist it is created.
    /// </summary>
    /// <param name="directoryPath">Full path to the directory to empty.</param>
    public static void EnsureDirectoryIsEmpty(String directoryPath)
    {
      directoryPath.VerifyThatStringIsNotNullAndNotEmpty("Parameter 'directoryPath' is null or empty.");
      if (Directory.Exists(directoryPath))
      {
        Directory.Delete(directoryPath, true);
      }

      Directory.CreateDirectory(directoryPath);
    }
  }
}
