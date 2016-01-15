namespace Jabberwocky.Toolkit_FUnitTests

open System
open System.Collections.Generic
open FsUnit
open Jabberwocky.Toolkit.List
open NUnit.Framework

module ``ListExtensions Unit Tests`` =

    [<Test>]
    let ``Null Generic list reference returns true``() =
        ListExtensions.IsNullOrEmpty(null) |> should be True

    [<Test>]
    let ``Empty Generic list returns true``() =
        let emptyList = System.Collections.Generic.List<String>()
        ListExtensions.IsNullOrEmpty(emptyList) |> should be True

    [<Test>]
    let ``Nonempty Generic list returns false``() =
        let emptyList = System.Collections.Generic.List<String>([|"a"|])
        ListExtensions.IsNullOrEmpty(emptyList) |> should be False
