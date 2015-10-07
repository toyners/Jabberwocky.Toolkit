
namespace Jabberwocky.Toolkit.Validation
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public static class Validation
  {
    #region Methods
    /// <summary>
    /// (extension) Verifies that the object is not null. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">Object to verify.</param>
    public static void VerifyThatObjectIsNotNull(this Object instance)
    {
      VerifyThatObjectIsNotNull(instance, "Object is null.");
    }

    /// <summary>
    /// (extension) Verifies that the object is not null. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="instance">Object to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatObjectIsNotNull(this Object instance, String exceptionMessage)
    {
      if (instance == null)
      {
        throw new Exception(exceptionMessage);
      }
    }

    /// <summary>
    /// (extension) Verifies that the string is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">String to verify.</param>
    public static void VerifyThatStringIsNotNullAndNotEmpty(this String instance)
    {
      VerifyThatStringIsNotNullAndNotEmpty(instance, "String is null or empty.");
    }

    /// <summary>
    /// (extension) Verifies that the string is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="instance">String to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatStringIsNotNullAndNotEmpty(this String instance, String exceptionMessage)
    {
      if (String.IsNullOrEmpty(instance))
      {
        throw new Exception(exceptionMessage);
      }
    }
    #endregion
  }
}
