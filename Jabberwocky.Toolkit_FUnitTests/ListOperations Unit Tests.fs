namespace Jabberwocky.Toolkit_FUnitTests

open System
open System.Collections.Generic
open FsUnit
open Jabberwocky.Toolkit.List
open NUnit.Framework

module ``ListOperations UnitTests`` =

    [<Test>]
    let ``Null Generic list reference returns true``() =
        ListOperations.IsEmpty(null) |> should be True

    [<Test>]
    let ``Empty Generic list returns true``() =
        let emptyList = System.Collections.Generic.List<String>()
        ListOperations.IsEmpty(emptyList) |> should be True

    [<Test>]
    let ``Nonempty Generic list returns false``() =
        let emptyList = System.Collections.Generic.List<String>([|"a"|])
        ListOperations.IsEmpty(emptyList) |> should be False
