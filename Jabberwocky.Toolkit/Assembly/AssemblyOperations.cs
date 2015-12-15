
namespace Jabberwocky.Toolkit.Assembly
{
  using System;
  using System.IO;
  using System.Reflection;

  /// <summary>
  /// Extension methods for the Assembly class.
  /// </summary>
  public static class AssemblyOperations
  {
    #region Construction
    /// <summary>
    /// Writes the embedded resource from an assembly to disk.
    /// </summary>
    /// <param name="assembly">Assembly containing the resource.</param>
    /// <param name="resourceName">Full qualifed name of the resource.</param>
    /// <param name="filePath">Full qualifed name of the file to create.</param>
    public static void CopyEmbeddedResourceToFile(this Assembly assembly, String resourceName, String filePath)
    {
      using (Stream manifestStream = assembly.GetManifestResourceStream(resourceName))
      {
        if (manifestStream == null)
        {
          throw new NullReferenceException(String.Format("Embedded resource '{0}' not found in assembly '{1}'.", resourceName, assembly.GetName().Name));
        }

        using (Stream outputStream = File.Create(filePath))
        {
          Byte[] buffer = new Byte[8192];

          Int32 bytesRead;
          while ((bytesRead = manifestStream.Read(buffer, 0, buffer.Length)) > 0)
          {
            outputStream.Write(buffer, 0, bytesRead);
          }
        }
      }
    }
    #endregion
  }
}
