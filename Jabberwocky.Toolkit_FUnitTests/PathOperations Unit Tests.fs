namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.Path
open NUnit.Framework

module ``PathOperations UnitTests`` =

    [<Test>]
    let ``Directory path has no trailing seperator``() =
        Assert.AreEqual(@"C:\Directory\", PathOperations.CompleteDirectoryPath(@"C:\Directory"))

    [<Test>]
    let ``Directory path has trailing seperator``() =
        Assert.AreEqual(@"C:\Directory\", PathOperations.CompleteDirectoryPath(@"C:\Directory\"))

