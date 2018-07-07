namespace Jabberwocky.Toolkit_FUnitTests

open System
open Jabberwocky.Toolkit.List
open NUnit.Framework

module ``ListExtensions Unit Tests`` =

    [<Test>]
    let ``Null Generic list reference returns true``() =
        Assert.AreEqual(true, ListExtensions.IsNullOrEmpty(null))

    [<Test>]
    let ``Empty Generic list returns true``() =
        let emptyList = System.Collections.Generic.List<String>()
        Assert.AreEqual(true, ListExtensions.IsNullOrEmpty(emptyList))

    [<Test>]
    let ``Nonempty Generic list returns false``() =
        let list = System.Collections.Generic.List<String>([|"a"|])
        Assert.AreEqual(false, ListExtensions.IsNullOrEmpty(list))
