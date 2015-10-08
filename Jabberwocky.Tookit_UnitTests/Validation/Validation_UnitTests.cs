
namespace Jabberwocky.Tookit_UnitTests.Validation
{
  using System;
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
    public void Validation_StringIsNotEmpty_ExceptionNotThrown()
    {
      String instance = "Not Empty";
      Action action = () => instance.VerifyThatStringIsNotNullAndNotEmpty();

      action.ShouldNotThrow();
    }
    #endregion
  }
}
