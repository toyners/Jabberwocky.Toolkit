
namespace Jabberwocky.Toolkit_IntegrationTests.File
{
  using System;
  using System.IO;
  using Jabberwocky.Toolkit.File;
  using NUnit.Framework;
  using Shouldly;

  [TestFixture]
  public class FilePatternResolver_IntegrationTests
  {
    private const String FileContent = "";

    private String parentDirectory = null;
    private String subDirectory = null;
    private String firstFileName = null;
    private String secondFileName = null;
    private String thirdFileName = null;
    private String fourthFileName = null;

    [TestFixtureSetUp]
    public void SetUpBeforeAllTests()
    {
      this.parentDirectory = Path.GetTempPath() + @"FilePatternResolver_IntegrationTests\";
      this.firstFileName = this.parentDirectory + "File1A.txt";
      this.secondFileName = this.parentDirectory + "File2B.txt";
      this.thirdFileName = this.parentDirectory + "File3C.txt";

      this.subDirectory = this.parentDirectory + @"Sub\";
      this.fourthFileName = this.subDirectory + "File4D.txt";
    }

    [SetUp]
    public void SetUpBeforeEachTest()
    {
      if (Directory.Exists(this.parentDirectory))
      {
        Directory.Delete(this.parentDirectory, true);
      }

      Directory.CreateDirectory(this.parentDirectory);
      Directory.CreateDirectory(this.subDirectory);
      File.WriteAllText(this.firstFileName, FileContent);
      File.WriteAllText(this.secondFileName, FileContent);
      File.WriteAllText(this.thirdFileName, FileContent);
      File.WriteAllText(this.fourthFileName, FileContent);
    }

    [Test]
    public void MatchesAllFilesInInitialDirectory()
    {
      String[] fileList = FilePatternResolver.ResolveFilePattern(this.parentDirectory + "File*.txt", FilePatternResolver.SearchDepths.InitialDirectoryOnly);

      fileList.Length.ShouldBe(3);
      fileList.ShouldContain(this.firstFileName);
      fileList.ShouldContain(this.secondFileName);
      fileList.ShouldContain(this.thirdFileName);
    }

    [Test]
    public void MatchesSomeFilesInInitialDirectory()
    {
      String[] fileList = FilePatternResolver.ResolveFilePattern(this.parentDirectory + "File1*.txt", FilePatternResolver.SearchDepths.InitialDirectoryOnly);

      fileList.Length.ShouldBe(1);
      fileList.ShouldContain(this.firstFileName);
    }

    [Test]
    public void MatchesNoFilesInInitialDirectory()
    {
      String[] fileList = FilePatternResolver.ResolveFilePattern(this.parentDirectory + "File4*.txt", FilePatternResolver.SearchDepths.InitialDirectoryOnly);

      fileList.Length.ShouldBe(0);
    }

    [Test]
    public void MatchesAllFilesInAllDirectories()
    {
      String[] fileList = FilePatternResolver.ResolveFilePattern(this.parentDirectory + "File*.txt", FilePatternResolver.SearchDepths.AllDirectories);

      fileList.Length.ShouldBe(4);
      fileList.ShouldContain(this.firstFileName);
      fileList.ShouldContain(this.secondFileName);
      fileList.ShouldContain(this.thirdFileName);
      fileList.ShouldContain(this.fourthFileName);
    }

    [Test]
    public void MatchesFilesInSubDirectory()
    {
      String[] fileList = FilePatternResolver.ResolveFilePattern(this.parentDirectory + "File4*.txt", FilePatternResolver.SearchDepths.AllDirectories);

      fileList.Length.ShouldBe(1);
      fileList.ShouldContain(this.fourthFileName);
    }
  }
}
