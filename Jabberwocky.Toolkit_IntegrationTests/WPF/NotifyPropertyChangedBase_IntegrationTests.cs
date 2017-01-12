
namespace Jabberwocky.Toolkit_IntegrationTests.WPF
{
  using System;
  using NUnit.Framework;
  using Shouldly;
  using Toolkit.WPF;

  /// <summary>
  /// Tests for the logic within the NotifyPropertyChangedBase class.
  /// </summary>
  [TestFixture]
  public class NotifyPropertyChangedBase_IntegrationTests : NotifyPropertyChangedBase
  {
    private Boolean propertyChangedEventWasTriggered;

    private Int32 value;

    #region Methods
    [SetUp]
    public void SetupBeforeEachTest()
    {
      this.value = 0;
      this.propertyChangedEventWasTriggered = false;
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
      this.propertyChangedEventWasTriggered.ShouldBeTrue();
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
      this.propertyChangedEventWasTriggered.ShouldBeFalse();
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
      this.propertyChangedEventWasTriggered.ShouldBeFalse();
    }

    private void PropertyChangedEventHandler(Object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      this.propertyChangedEventWasTriggered = true;
    }
    #endregion 
  }
}
