
namespace Jabberwocky.Toolkit_IntegrationTests.IO
{
  using System;
  using System.IO;
  using NUnit.Framework;
  using Shouldly;
  using Jabberwocky.Toolkit.IO;

  [TestFixture]
  public class DirectoryOperations_IntegrationTests
  {
    #region Methods
    [Test]
    public void NonExistantDirectoryIsCreated()
    {
      // Arrange
      String directoryPath = Path.GetTempPath() + Path.GetRandomFileName() + @"\";

      // Act
      DirectoryOperations.EnsureDirectoryIsEmpty(directoryPath);

      // Assert
      Directory.Exists(directoryPath).ShouldBeTrue();
      Directory.GetFileSystemEntries(directoryPath).Length.ShouldBe(0);
    }

    [Test]
    public void EmptyDirectoryRemainsUnchanged()
    {
      // Arrange
      String directoryPath = Path.GetTempPath() + Path.GetRandomFileName() + @"\";
      Directory.CreateDirectory(directoryPath);

      // Act
      DirectoryOperations.EnsureDirectoryIsEmpty(directoryPath);

      // Assert
      Directory.Exists(directoryPath).ShouldBeTrue();
      Directory.GetFileSystemEntries(directoryPath).Length.ShouldBe(0);
    }

    [Test]
    public void DirectoryWithFilesAndFoldersIsEmptied()
    {
      // Arrange
      String directoryPath = Path.GetTempPath() + Path.GetRandomFileName() + @"\";

      Directory.CreateDirectory(directoryPath);
      File.WriteAllText(directoryPath + "File1.txt", "File Content");
      Directory.CreateDirectory(directoryPath + @"Sub Folder\");
      File.WriteAllText(directoryPath + "File2.txt", "File Content");

      // Act
      DirectoryOperations.EnsureDirectoryIsEmpty(directoryPath);

      // Assert
      Directory.Exists(directoryPath).ShouldBeTrue();
      Directory.GetFileSystemEntries(directoryPath).Length.ShouldBe(0);
    }

    [Test]
    public void FileWithSameNameAlreadyExists()
    {
      // Arrange
      String directoryPath = Path.GetTempPath() + Path.GetRandomFileName();
      File.WriteAllText(directoryPath, "File Content");

      // Act and Assert
      Assert.Throws<IOException>(() => { DirectoryOperations.EnsureDirectoryExists(directoryPath); },
        $"Cannot create directory '{directoryPath}' because a file with the same name already exists.");
    }
    #endregion 
  }
}
