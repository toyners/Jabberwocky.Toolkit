namespace Jabberwocky.Toolkit_FUnitTests

open System.IO
open FsUnit
open Jabberwocky.Toolkit.IO
open NUnit.Framework

module ``DirectoryOperations Unit Tests`` =

    [<Test>]
    let ``Directory does not exist so is created``() =
        let directoryPath = Path.GetTempPath() + Path.GetRandomFileName();

        Directory.Exists(directoryPath) |> should be False
        DirectoryOperations.EnsureDirectoryExists(directoryPath) |> ignore
        Directory.Exists(directoryPath) |> should be True

        Directory.Delete(directoryPath) |> ignore

    [<Test>]
    let ``Directory does exist so nothing changes``() =
        let directoryPath = Path.GetTempPath() + Path.GetRandomFileName();
        Directory.CreateDirectory(directoryPath) |> ignore
        DirectoryOperations.EnsureDirectoryExists(directoryPath) |> ignore
        Directory.Exists(directoryPath) |> should be True

        Directory.Delete(directoryPath) |> ignore