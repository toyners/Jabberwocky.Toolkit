
namespace Jabberwocky.Toolkit.WPF
{
  using System.Windows;
  using System.Windows.Media;

  public static class VisualTreeHelperExtensions
  {
    /// <summary>
    /// Get first child element matching the type parameter that is within the parent element or the type default.
    /// </summary>
    /// <typeparam name="T">Type of child element to be returned.</typeparam>
    /// <param name="parent">Parent element that contains child element of interest.</param>
    /// <returns>First child element matching type element or type default.</returns>
    public static T GetDescendantByType<T>(this Visual parent) where T : class
    {
      if (parent == null)
      {
        return default(T);
      }

      if (parent.GetType() == typeof(T))
      {
        return parent as T;
      }

      if (parent is FrameworkElement)
      {
        (parent as FrameworkElement).ApplyTemplate();
      }

      T foundElement = null;
      for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
      {
        var element = VisualTreeHelper.GetChild(parent, i) as Visual;
        foundElement = element.GetDescendantByType<T>();
        if (foundElement != null)
        {
          break;
        }
      }

      return foundElement;
    }
  }
}
