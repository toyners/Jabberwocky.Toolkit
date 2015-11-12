namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.String
open FsUnit
open NUnit.Framework

module ``String Operations UnitTests`` =
    
    [<Test>]
    let ``Seperator not in line so whole line is returned``() =
        let instance = "ABCD"
        StringOperations.ExtractField(instance, ",", '\000', 0u) |> should equal "ABCD"

    [<Test>]
    [<TestCase(0u, "ABCD", ",", "ABCD")>]
    let ``a``(index: uint32, line: string, seperator: string, expectedResult: string) =
        StringOperations.ExtractField(line, seperator, '|', index) |> should equal expectedResult

    [<Test>]
    let ``Index beyond line so meaningful exception is thrown``() =
        (fun () -> StringOperations.ExtractField("A,B,C,D", ",", '|', 4u) |> ignore) 
        |> should (throwWithMessage "Index 4 is out of range in line 'A,B,C,D' when using seperator (\",\") and qualifier ('|').") typeof<System.IndexOutOfRangeException>