
namespace Jabberwocky.Toolkit.File
{
  using System;
  using System.IO;

  public static class FilePatternResolver
  {
    public enum SearchDepths
    {
      AllDirectories,
      InitialDirectoryOnly
    }

    public static String[] ResolveFilePattern(String filePattern, SearchDepths searchDepth)
    {
      SearchOption searchOption = (searchDepth == SearchDepths.InitialDirectoryOnly ? SearchOption.TopDirectoryOnly : SearchOption.AllDirectories);

      String path = Path.GetDirectoryName(filePattern);
      String searchPattern = Path.GetFileName(filePattern);

      return Directory.GetFiles(path, searchPattern, searchOption);
    }
  }
}
