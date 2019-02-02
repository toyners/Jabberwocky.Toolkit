
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
        public static string CompleteDirectoryPath(string directoryPath)
        {
            directoryPath.VerifyThatStringIsNotNullAndNotEmpty("Parameter 'directoryPath' is null or empty.");
            if (directoryPath[directoryPath.Length - 1] != Path.DirectorySeparatorChar)
            {
                directoryPath += Path.DirectorySeparatorChar;
            }

            return directoryPath;
        }

        public static string GetAbsolutePath(string path)
        {
            return GetAbsolutePath(null, path);
        }

        public static string GetAbsolutePath(string basePath, string path)
        {
            if (path == null)
                return null;
            if (basePath == null)
                basePath = System.IO.Path.GetFullPath("."); // quick way of getting current working directory
            else
                basePath = GetAbsolutePath(null, basePath); // to be REALLY sure ;)

            string finalPath;
            if (!System.IO.Path.IsPathRooted(path) || "\\".Equals(System.IO.Path.GetPathRoot(path)))
            {
                if (path.StartsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
                    finalPath = System.IO.Path.Combine(System.IO.Path.GetPathRoot(basePath), path.TrimStart(System.IO.Path.DirectorySeparatorChar));
                else
                    finalPath = System.IO.Path.Combine(basePath, path);
            }
            else
            {
                finalPath = path;
            }

            // resolves any internal "..\" to get the true full path.
            return System.IO.Path.GetFullPath(finalPath);
        }
        #endregion
    }
}
