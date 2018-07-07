namespace Jabberwocky.Toolkit_FUnitTests

open System.IO
open NUnit.Framework
open Jabberwocky.Toolkit.IO

module ``DirectoryOperations Unit Tests`` =

    [<Test>]
    let ``Directory does not exist so is created``() =
        let directoryPath = Path.GetTempPath() + Path.GetRandomFileName();

        Assert.AreEqual(false, Directory.Exists(directoryPath))
        DirectoryOperations.EnsureDirectoryExists(directoryPath) |> ignore
        Assert.AreEqual(true, Directory.Exists(directoryPath))

        Directory.Delete(directoryPath) |> ignore

    [<Test>]
    let ``Directory does exist so nothing changes``() =
        let directoryPath = Path.GetTempPath() + Path.GetRandomFileName();
        Directory.CreateDirectory(directoryPath) |> ignore
        DirectoryOperations.EnsureDirectoryExists(directoryPath) |> ignore

        Assert.AreEqual(true, Directory.Exists(directoryPath))

        Directory.Delete(directoryPath) |> ignore