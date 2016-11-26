namespace Jabberwocky.Toolkit_FUnitTests

open System
open Jabberwocky.Toolkit.Object
//open System.IO
//open FsUnit
//open Jabberwocky.Toolkit.IO
open NUnit.Framework

type ObjectExtensions_UnitTests() =

    [<Test>]
    member public this.``Object is null so meaningful exception is thrown``() = 
        let instance = null;

        let mutable testSuccessful = false
        try
            instance.VerifyThatObjectIsNotNull()
        with
        | _ as e ->
            let isNullReferenceException = e :? System.NullReferenceException
            Assert.IsTrue(isNullReferenceException)

            let nullReferenceException = e :?> System.NullReferenceException
            Assert.AreEqual(nullReferenceException.Message, "Object is null.")

            testSuccessful <- true

        Assert.IsTrue(testSuccessful)

    [<Test>]
    member public this.``Object is null so custom meaningful exception is thrown``() = 
        let instance = null;

        let mutable testSuccessful = false
        try
            instance.VerifyThatObjectIsNotNull("Custom exception message.")
        with
        | _ as e ->
            let isNullReferenceException = e :? System.NullReferenceException
            Assert.IsTrue(isNullReferenceException)

            let nullReferenceException = e :?> System.NullReferenceException
            Assert.AreEqual(nullReferenceException.Message, "Custom exception message.")

            testSuccessful <- true

        Assert.IsTrue(testSuccessful)
