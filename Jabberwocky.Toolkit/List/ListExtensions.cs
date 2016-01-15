
namespace Jabberwocky.Toolkit.List
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Extension methods for generic lists.
  /// </summary>
  public static class ListExtensions
  {
    /// <summary>
    /// Returns true if the generic list is null or empty. Otherwise false.
    /// </summary>
    /// <typeparam name="T">Type contained within list.</typeparam>
    /// <param name="list">Generic list instance to test.</param>
    /// <returns>True if null or empty, otherwise false.</returns>
    public static Boolean IsNullOrEmpty<T>(this List<T> list)
    {
      return list == null || list.Count == 0;
    }

    /// <summary>
    /// Verifies that the generic list is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="list">List to verify.</param>
    public static void VerifyThatListIsNotNullAndNotEmpty<T>(this List<T> list)
    {
      list.VerifyThatListIsNotNullAndNotEmpty("List is null or empty.");
    }

    /// <summary>
    /// Verifies that the generic list is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="list">List to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatListIsNotNullAndNotEmpty<T>(this List<T> list, String exceptionMessage)
    {
      if (list.IsNullOrEmpty())
      {
        throw new Exception(exceptionMessage);
      }
    }
  }
}
