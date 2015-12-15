namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.Assembly
open FsUnit
open NUnit.Framework

module ``AssemblyOperations Unit Tests`` = 

    [<Test>]
    let ``Embedded resource is not in assembly so meaningful exception is thrown``() =
        let exceptionMessage = "Embedded resource 'OutputFile.txt' not found in assembly '" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "'."
        (fun () -> AssemblyOperations.CopyEmbeddedResourceToFile(System.Reflection.Assembly.GetExecutingAssembly(), "OutputFile.txt", System.IO.Path.GetTempPath() + "OutputFile.txt") |> ignore)
        |> should (throwWithMessage exceptionMessage) typeof<System.NullReferenceException>;

