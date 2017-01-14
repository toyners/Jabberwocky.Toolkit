
namespace Jabberwocky.Toolkit.WPF
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Runtime.CompilerServices;
  using Object;

  /// <summary>
  /// Provides mechanism for raising notifications for property changes when underlying fields are updated.
  /// Used for updating UI in WPF.
  /// </summary>
  public class NotifyPropertyChangedBase : INotifyPropertyChanged
  {
    /// <summary>
    /// Event raised when field is updated.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    protected void SetField<T>(ref T fieldValue, T newValue, [CallerMemberName] String propertyName = null)
    {
      if (EqualityComparer<T>.Default.Equals(fieldValue, newValue))
      {
        return; 
      }

      fieldValue = newValue;
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected Boolean TryInvokePropertyChanged(params PropertyChangedEventArgs[] propertyChangedEventArgs)
    {
      propertyChangedEventArgs.VerifyThatObjectIsNotNull("Parameter 'propertyChangedEventArgs' is null.");

      if (this.PropertyChanged == null)
      {
        return false;
      }

      foreach(var propertyChangedEventArg in propertyChangedEventArgs)
      {
        this.PropertyChanged.Invoke(this, propertyChangedEventArg);
      }

      return true;
    }
  }
}
