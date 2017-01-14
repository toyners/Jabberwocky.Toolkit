
namespace Jabberwocky.Toolkit_IntegrationTests.WPF
{
  using System;
  using System.ComponentModel;
  using NUnit.Framework;
  using Shouldly;
  using Toolkit.WPF;

  /// <summary>
  /// Tests for the logic within the NotifyPropertyChangedBase class.
  /// </summary>
  [TestFixture]
  public class NotifyPropertyChangedBase_IntegrationTests : NotifyPropertyChangedBase
  {
    #region Fields
    private Boolean propertyChangedEventTriggered;

    private Int32 value;
    #endregion

    #region Methods
    [SetUp]
    public void SetupBeforeEachTest()
    {
      this.value = 0;
      this.propertyChangedEventTriggered = false;
      this.PropertyChanged -= this.PropertyChangedEventHandler;
    }

    /// <summary>
    /// Test that the property change event is raised when the field is changed.
    /// </summary>
    [Test]
    public void PropertyChangeEventHandlerTriggeredWhenFieldChanged()
    {
      // Arrange
      this.PropertyChanged += this.PropertyChangedEventHandler;

      // Act
      this.SetField(ref value, 1);

      // Assert
      this.propertyChangedEventTriggered.ShouldBeTrue();
    }

    /// <summary>
    /// Test that the property change event is not raised when the field is changed but
    /// no event handler is assigned.
    /// </summary>
    [Test]
    public void PropertyChangeEventHandlerNotTriggeredWhenFieldChanged()
    {
      // Act
      this.SetField(ref value, 1);

      // Assert
      this.propertyChangedEventTriggered.ShouldBeFalse();
    }

    /// <summary>
    /// Test that the property change event is not raised if the field is not changed
    /// because the value is the same.
    /// </summary>
    [Test]
    public void PropertyChangeEventHandlerNotTriggeredWhenFieldIsNotChanged()
    {
      // Arrange
      this.SetField(ref value, 1);
      this.PropertyChanged += PropertyChangedEventHandler;

      // Act
      this.SetField(ref value, 1);

      // Assert
      this.propertyChangedEventTriggered.ShouldBeFalse();
    }

    [Test]
    public void InvokingPropertiesWhenEventHandlerIsSetReturnsTrue()
    {
      // Arrange
      this.PropertyChanged += PropertyChangedEventHandler;

      // Act
      var result = this.TryInvokePropertyChanged(new PropertyChangedEventArgs("Property1"), new PropertyChangedEventArgs("Property2"));

      // Assert
      result.ShouldBeTrue();
    }

    [Test]
    public void InvokingPropertiesWhenNoEventHandlerIsSetReturnsFalse()
    {
      // Arrange
      var result = this.TryInvokePropertyChanged(new PropertyChangedEventArgs("Property1"), new PropertyChangedEventArgs("Property2"));

      // Assert
      result.ShouldBeFalse();
    }

    private void PropertyChangedEventHandler(Object sender, PropertyChangedEventArgs e)
    {
      this.propertyChangedEventTriggered = true;
    }
    #endregion 
  }
}
