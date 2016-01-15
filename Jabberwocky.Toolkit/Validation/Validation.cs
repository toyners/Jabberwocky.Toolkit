
namespace Jabberwocky.Toolkit.Validation
{
  using System;
  using System.Collections.Generic;

  public static class Validation
  {
    #region Methods
    /// <summary>
    /// Returns true if the array is null or empty. Otherwise false.
    /// </summary>
    /// <param name="array">Array instance to test.</param>
    /// <returns>True if null or empty, otherwise false.</returns>
    public static Boolean IsNullOrEmpty(this Array array)
    {
      return (array == null || array.Length == 0);
    }

    

    

    
    #endregion
  }
}
