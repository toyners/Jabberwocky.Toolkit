
namespace Jabberwocky.Tookit_UnitTests.String
{
  using System;
  using FluentAssertions;
  using NUnit.Framework;
  using Jabberwocky.Toolkit.String;

  [TestFixture]
  public class StringOperations_UnitTests
  {
    #region Methods
    [Test]
    public void ExtractField_SeperatorNotInLine_WholeLineReturned()
    {
      String line = "ABCD";

      line.ExtractField(",", '\0', 0).Should().Be("ABCD");
    }

    [Test]
    public void ExtractField_IndexBeyondLine_ThrowsMeaningfulException()
    {
      String line = "A,B,C,D";

      Action action = () => line.ExtractField(",", '\0', 4);
      
      action.ShouldThrow<IndexOutOfRangeException>().WithMessage("Index 4 is out of range in line 'A,B,C,D' when using seperator (\",\") and qualifier('\0').");
    }

    [Test]
    [TestCase(0, "A,B,C,D", "A")]
    [TestCase(1, "A,B,C,D", "B")]
    [TestCase(2, "A,B,C,D", "C")]
    [TestCase(3, "A,B,C,D", "D")]
    [TestCase(0, "|A,1|B,C,D", "A,1")]
    public void ExtractField_AskForField_ReturnsField(Int32 index, String line, String expectedResult)
    {
      line.ExtractField(",", '|', (UInt32)index).Should().Be(expectedResult);
    }
    #endregion
  }
}
