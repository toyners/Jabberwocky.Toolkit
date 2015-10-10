
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
    /// Verifies that the object is not null. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">Object to verify.</param>
    public static void VerifyThatObjectIsNotNull(this Object instance)
    {
      VerifyThatObjectIsNotNull(instance, "Object is null.");
    }

    /// <summary>
    /// Verifies that the object is not null. Throws an exception (with custom message) if verification fails.
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
    /// Verifies that the string is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">String to verify.</param>
    public static void VerifyThatStringIsNotNullAndNotEmpty(this String instance)
    {
      VerifyThatStringIsNotNullAndNotEmpty(instance, "String is null or empty.");
    }

    /// <summary>
    /// Verifies that the string is not null and not empty. Throws an exception (with custom message) if verification fails.
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

    /// <summary>
    /// Verifies that the generic list is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="list">List to verify.</param>
    public static void VerifyThatListIsNotNullAndNotEmpty<T>(this List<T> list)
    {
      if (list == null || list.Count == 0)
      {
        throw new Exception("List is null or empty.");
      }
    }

    /// <summary>
    /// Verifies that the generic list is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="list">List to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatListIsNotNullAndNotEmpty<T>(this List<T> list, String exceptionMessage)
    {
      if (list == null || list.Count == 0)
      {
        throw new Exception(exceptionMessage);
      }
    }

    /// <summary>
    /// Verifies that the array is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="array">Array to verify.</param>
    public static void VerifyThatArrayIsNotNullAndNotEmpty(this Array array)
    {
      if (array == null || array.Length == 0)
      {
        throw new Exception("Array is null or empty.");
      }
    }

    /// <summary>
    /// Verifies that the array is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="array">Array to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatArrayIsNotNullAndNotEmpty(this Array array, String exceptionMessage)
    {
      if (array == null || array.Length == 0)
      {
        throw new Exception(exceptionMessage);
      }
    }
    #endregion
  }
}
