namespace Jabberwocky.Toolkit_FUnitTests

open Jabberwocky.Toolkit.Assembly
open NUnit.Framework

module ``AssemblyExtensions Unit Tests`` = 
    open System

    [<Test>]
    let ``Embedded resource is not in assembly so meaningful exception is thrown``() =
        let exceptionMessage = "Embedded resource 'OutputFile.txt' not found in assembly '" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + "'."
        Assert.Throws<NullReferenceException>(
            (fun () -> AssemblyExtensions.CopyEmbeddedResourceToFile(System.Reflection.Assembly.GetExecutingAssembly(), "OutputFile.txt", System.IO.Path.GetTempPath() + "OutputFile.txt") |> ignore),
            exceptionMessage)  |> ignore

