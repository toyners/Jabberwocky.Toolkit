
namespace Jabberwocky.Tookit_UnitTests.Validation
{
  using System;
  using System.Collections.Generic;
  using FluentAssertions;
  using Jabberwocky.Toolkit.Validation;
  using NUnit.Framework;

  [TestFixture]
  public class Validation_UnitTests
  {
    #region Methods
    [Test]
    public void Validation_ObjectIsNull_ExpectedExceptionIsThrown()
    {
      Object instance = null;
      Action action = () => instance.VerifyThatObjectIsNotNull();

      action.ShouldThrow<Exception>().WithMessage("Object is null.");
    }

    [Test]
    public void Validation_ObjectIsNullWithCustomMessage_ExpectedExceptionIsThrown()
    {
      Object instance = null;
      Action action = () => instance.VerifyThatObjectIsNotNull("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void Validation_ObjectIsNotNull_ExceptionNotThrown()
    {
      Object instance = new Object();
      Action action = () => instance.VerifyThatObjectIsNotNull();

      action.ShouldNotThrow();
    }

    [Test]
    public void Validation_StringIsNull_ExpectedExceptionIsThrown()
    {
      String instance = null;
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("String is null or empty.");
    }

    [Test]
    public void Validation_StringIsEmpty_ExpectedExceptionIsThrown()
    {
      String instance = String.Empty;
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("String is null or empty.");
    }

    [Test]
    public void Validation_StringIsNullWithCustomMessage_ExpectedExceptionIsThrown()
    {
      String instance = null;
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void Validation_StringIsEmptyWithCustomMessage_ExpectedExceptionIsThrown()
    {
      String instance = String.Empty;
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void Validation_StringIsNotEmpty_ExceptionNotThrown()
    {
      String instance = "Not Empty";
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty();

      action.ShouldNotThrow();
    }

    [Test]
    public void VerifyThatListIsNotNullAndNotEmpty_ListIsNull_ThrowsExpectedException()
    {
      List<String> list = null;

      Action action = () => list.VerifyThatListIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("List is null or empty.");
    }

    [Test]
    public void VerifyThatListIsNotNullAndNotEmpty_ListIsEmpty_ThrowsExpectedException()
    {
      List<String> list = new List<String>();

      Action action = () => list.VerifyThatListIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("List is null or empty.");
    }

    [Test]
    public void VerifyThatListIsNotNullAndNotEmpty_ListIsNullWithCustomMessage_ThrowsExpectedException()
    {
      List<String> list = null;

      Action action = () => list.VerifyThatListIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void VerifyThatListIsNotNullAndNotEmpty_ListIsEmptyWithCustomMessage_ThrowsExpectedException()
    {
      List<String> list = new List<String>();

      Action action = () => list.VerifyThatListIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void VerifyThatListIsNotNullAndNotEmpty_ListIsNotEmpty_ExceptionNotThrown()
    {
      List<String> list = new List<String>() { "String" };

      Action action = () => list.VerifyThatListIsNotNullAndNotEmpty();

      action.ShouldNotThrow();
    }

    [Test]
    public void VerifyThatArrayIsNotNullAndNotEmpty_ArrayIsNull_ThrowsExpectedException()
    {
      String[] array = null;

      Action action = () => array.VerifyThatArrayIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("Array is null or empty.");
    }

    [Test]
    public void VerifyThatArrayIsNotNullAndNotEmpty_ArrayIsEmpty_ThrowsExpectedException()
    {
      String[] array = new String[0];

      Action action = () => array.VerifyThatArrayIsNotNullAndNotEmpty();

      action.ShouldThrow<Exception>().WithMessage("Array is null or empty.");
    }

    [Test]
    public void VerifyThatArrayIsNotNullAndNotEmpty_ArrayIsNullWithCustomMessage_ThrowsExpectedException()
    {
      String[] array = null;

      Action action = () => array.VerifyThatArrayIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void VerifyThatArrayIsNotNullAndNotEmpty_ArrayIsEmptyWithCustomMessage_ThrowsExpectedException()
    {
      String[] array = new String[0];

      Action action = () => array.VerifyThatArrayIsNotNullAndNotEmpty("Custom exception message.");

      action.ShouldThrow<Exception>().WithMessage("Custom exception message.");
    }

    [Test]
    public void VerifyThatArrayIsNotNullAndNotEmpty_ArrayIsNotEmpty_ExceptionNotThrown()
    {
      String[] array = new String[1];

      Action action = () => array.VerifyThatArrayIsNotNullAndNotEmpty();

      action.ShouldNotThrow();
    }
    #endregion
  }
}
