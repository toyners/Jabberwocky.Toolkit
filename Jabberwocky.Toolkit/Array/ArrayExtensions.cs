
namespace Jabberwocky.Toolkit.Array
{
  using System;

  /// <summary>
  /// Extension methods for the Array class.
  /// </summary>
  public static class ArrayExtensions
  {
    /// <summary>
    /// Returns true if the array is null or empty. Otherwise false.
    /// </summary>
    /// <param name="array">Array instance to test.</param>
    /// <returns>True if null or empty, otherwise false.</returns>
    public static Boolean IsNullOrEmpty(this Array array)
    {
      return (array == null || array.Length == 0);
    }

    /// <summary>
    /// Verifies that the array is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="array">Array to verify.</param>
    public static void VerifyThatArrayIsNotNullAndNotEmpty(this Array array)
    {
      array.VerifyThatArrayIsNotNullAndNotEmpty("Array is null or empty.");
    }

    /// <summary>
    /// Verifies that the array is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="array">Array to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatArrayIsNotNullAndNotEmpty(this Array array, String exceptionMessage)
    {
      if (array.IsNullOrEmpty())
      {
        throw new Exception(exceptionMessage);
      }
    }
  }
}
