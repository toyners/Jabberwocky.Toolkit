
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
    public void ExtractField_IndexBeyondLine_ThrowsMeaningfulException()
    {
      String line = "A,B,C,D";

      Action action = () => line.ExtractField(",", '|', 4);
      
      action.ShouldThrow<IndexOutOfRangeException>().WithMessage("Index 4 is out of range in line 'A,B,C,D' when using seperator (\",\") and qualifier ('|').");
    }

    [Test]
    public void ExtractField_IndexBeyondLineEndingInSeperator_ThrowsMeaningfulException()
    {
      String line = "A,B,C,";

      Action action = () => line.ExtractField(",", '|', 4);

      action.ShouldThrow<IndexOutOfRangeException>().WithMessage("Index 4 is out of range in line 'A,B,C,' when using seperator (\",\") and qualifier ('|').");
    }

    [Test]
    [TestCase(0, "ABCD", ",", "ABCD")] // Seperator not in the line so whole line returned.
    [TestCase(0, ",B,C,D", ",", "")]
    [TestCase(0, "A,B,C,D", ",", "A")]
    [TestCase(1, "A,B,C,D", ",", "B")]
    [TestCase(2, "A,B,C,D", ",", "C")]
    [TestCase(3, "A,B,C,D", ",", "D")]
    [TestCase(3, "A,B,C,", ",", "")]
    [TestCase(0, "|A,1|,B,C,D", ",", "A,1")]
    [TestCase(1, "A,|B,2|,C,D", ",", "B,2")]
    [TestCase(2, "A,B,|C,3|,D", ",", "C,3")]
    [TestCase(3, "A,B,C,|D,4|", ",", "D,4")]
    [TestCase(3, "A,B,C,|D,4", ",", "D,4")]
    [TestCase(2, "A,B,|C,3,D", ",", "C,3,D")]
    [TestCase(0, "<sep>B<sep>C<sep>D", "<sep>", "")]
    [TestCase(0, "A<sep>B<sep>C<sep>D", "<sep>", "A")]
    [TestCase(1, "A<sep>B<sep>C<sep>D", "<sep>", "B")]
    [TestCase(2, "A<sep>B<sep>C<sep>D", "<sep>", "C")]
    [TestCase(3, "A<sep>B<sep>C<sep>D", "<sep>", "D")]
    [TestCase(3, "A<sep>B<sep>C<sep>", "<sep>", "")]
    [TestCase(0, "|A<sep>1|<sep>B<sep>C<sep>D", "<sep>", "A<sep>1")]
    [TestCase(1, "A<sep>|B<sep>2|<sep>C<sep>D", "<sep>", "B<sep>2")]
    [TestCase(2, "A<sep>B<sep>|C<sep>3|<sep>D", "<sep>", "C<sep>3")]
    [TestCase(3, "A<sep>B<sep>C<sep>|D<sep>4|", "<sep>", "D<sep>4")]
    [TestCase(3, "A<sep>B<sep>C<sep>|D<sep>4", "<sep>", "D<sep>4")]
    [TestCase(2, "A<sep>B<sep>|C<sep>3<sep>D", "<sep>", "C<sep>3<sep>D")]
    public void ExtractField_AskForField_ReturnsField(Int32 index, String line, String seperator, String expectedResult)
    {
      line.ExtractField(seperator, '|', (UInt32)index).Should().Be(expectedResult);
    }
    #endregion
  }
}
