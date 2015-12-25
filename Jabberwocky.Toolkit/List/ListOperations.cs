
namespace Jabberwocky.Toolkit.List
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Extension methods for generic lists.
  /// </summary>
  public static class ListOperations
  {
    /// <summary>
    /// Returns true if the generic list is null or empty. Otherwise false.
    /// </summary>
    /// <typeparam name="T">Type contained within list.</typeparam>
    /// <param name="list">Generic list instance to test.</param>
    /// <returns>True if null or empty, otherwise false.</returns>
    public static Boolean IsEmpty<T>(this List<T> list)
    {
      return list == null || list.Count == 0;
    }
  }
}
