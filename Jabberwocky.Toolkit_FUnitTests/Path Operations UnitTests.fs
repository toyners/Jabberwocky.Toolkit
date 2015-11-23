namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.Path
open FsUnit
open NUnit.Framework

module ``Path Operations UnitTests`` =

    [<Test>]
    let ``Directory path has no trailing seperator``() =
        PathOperations.CompleteDirectoryPath(@"C:\Directory") |> should equal @"C:\Directory\"

    [<Test>]
    let ``Directory path has trailing seperator``() =
        PathOperations.CompleteDirectoryPath(@"C:\Directory\") |> should equal @"C:\Directory\"

