
namespace Jabberwocky.Toolkit.Object
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  /// <summary>
  /// Extension methods for the Object class.
  /// </summary>
  public static class ObjectExtensions
  {
    /// <summary>
    /// Verifies that the object is not null. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">Object to verify.</param>
    public static void VerifyThatObjectIsNotNull(this Object instance)
    {
      instance.VerifyThatObjectIsNotNull("Object is null.");
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
  }
}
