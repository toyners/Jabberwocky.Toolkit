namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.String
open FsUnit
open NUnit.Framework
open System
open System.Collections.Generic

module ``StringExtensions Unit Tests`` =
    
    [<Test>]
    [<TestCase(0u, "ABCD", ",", "ABCD")>]
    [<TestCase(0u, ",B,C,D", ",", "")>]
    [<TestCase(0u, "A,B,C,D", ",", "A")>]
    [<TestCase(1u, "A,B,C,D", ",", "B")>]
    [<TestCase(2u, "A,B,C,D", ",", "C")>]
    [<TestCase(3u, "A,B,C,D", ",", "D")>]
    [<TestCase(3u, "A,B,C,", ",", "")>]
    [<TestCase(0u, "|A,1|,B,C,D", ",", "A,1")>]
    [<TestCase(1u, "A,|B,2|,C,D", ",", "B,2")>]
    [<TestCase(2u, "A,B,|C,3|,D", ",", "C,3")>]
    [<TestCase(3u, "A,B,C,|D,4|", ",", "D,4")>]
    [<TestCase(3u, "A,B,C,|D,4", ",", "D,4")>]
    [<TestCase(2u, "A,B,|C,3,D", ",", "C,3,D")>]
    [<TestCase(0u, "<sep>B<sep>C<sep>D", "<sep>", "")>]
    [<TestCase(0u, "A<sep>B<sep>C<sep>D", "<sep>", "A")>]
    [<TestCase(1u, "A<sep>B<sep>C<sep>D", "<sep>", "B")>]
    [<TestCase(2u, "A<sep>B<sep>C<sep>D", "<sep>", "C")>]
    [<TestCase(3u, "A<sep>B<sep>C<sep>D", "<sep>", "D")>]
    [<TestCase(3u, "A<sep>B<sep>C<sep>", "<sep>", "")>]
    [<TestCase(0u, "|A<sep>1|<sep>B<sep>C<sep>D", "<sep>", "A<sep>1")>]
    [<TestCase(1u, "A<sep>|B<sep>2|<sep>C<sep>D", "<sep>", "B<sep>2")>]
    [<TestCase(2u, "A<sep>B<sep>|C<sep>3|<sep>D", "<sep>", "C<sep>3")>]
    [<TestCase(3u, "A<sep>B<sep>C<sep>|D<sep>4|", "<sep>", "D<sep>4")>]
    [<TestCase(3u, "A<sep>B<sep>C<sep>|D<sep>4", "<sep>", "D<sep>4")>]
    [<TestCase(2u, "A<sep>B<sep>|C<sep>3<sep>D", "<sep>", "C<sep>3<sep>D")>]
    let ``Correct field returned from different extract scenarios``(index: uint32, line: string, seperator: string, expectedResult: string) =
        StringExtensions.ExtractField(line, seperator, '|', index) |> should equal expectedResult

    [<Test>]
    let ``Index beyond line so meaningful exception is thrown``() =
        (fun () -> StringExtensions.ExtractField("A,B,C,D", ",", '|', 4u) |> ignore) 
        |> should (throwWithMessage "Index 4 is out of range in line 'A,B,C,D' when using seperator (\",\") and qualifier ('|').") typeof<System.IndexOutOfRangeException>

    [<Test>]
    let ``Index beyond last seperator so meaningful exception is thrown``() =
        (fun () -> StringExtensions.ExtractField("A,B,C,", ",", '|', 4u) |> ignore) 
        |> should (throwWithMessage "Index 4 is out of range in line 'A,B,C,' when using seperator (\",\") and qualifier ('|').") typeof<System.IndexOutOfRangeException>

    [<Test>]
    let ``Illegal file characters are substituted correctly``() = 
        let d = dict[
                        "\\", "_backslash_"; 
                        "/", "_forwardslash_"; 
                        ":", "_colon_"; 
                        "*", "_asterix_";
                        "?", "_question_";
                        "\"", "_quote_";
                        "<", "_lessthan_";
                        ">", "_rightthan_";
                        "|", "_bar_"
                        ]

        StringExtensions.Substitute("\\/:*?\"<>|", d) |> should equal "_backslash__forwardslash__colon__asterix__question__quote__lessthan__rightthan__bar_"

    [<Test>]
    [<TestCase("Item", 1, "Item")>]
    [<TestCase("Item", 0, "Items")>]
    [<TestCase("Item", 2, "Items")>]
    let ``Pluralize word when count is different scenarios``(word : string, count : int, expectedReturn : string) = 
        StringExtensions.Pluralize(word, (uint32)count) |> should equal expectedReturn