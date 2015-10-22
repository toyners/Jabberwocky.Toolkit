
namespace Jabberwocky.Toolkit.String
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  /// <summary>
  /// Provides extension methods for the string class.
  /// </summary>
  public static class StringOperations
  {
    #region Methods
    /// <summary>
    /// Extracted a specified field from a string. Field is based on a seperator and qualifier.
    /// </summary>
    /// <param name="instance">String to extract field from.</param>
    /// <param name="seperator">The seperator to use when recognising fields.</param>
    /// <param name="qualifier">The qualifier to use when escaping seperator within fields.</param>
    /// <param name="fieldIndex">The index of the field to extract.</param>
    /// <returns>Extracted field string.</returns>
    public static String ExtractField(this String instance, String seperator, Char qualifier, UInt32 fieldIndex)
    {
      Int32 position = 0;
      Int32 index = GetLastIndexOfUnqualifiedSeperator(instance, seperator, qualifier, 0);

      if (index == -1)
      {
        return instance;
      }

      UInt32 fieldCount = fieldIndex;
      while (fieldCount > 0)
      {
        position = index + 1;
        if (position == instance.Length)
        {
          // field index is outside the last possible index in the line.
          if (fieldCount == 1)
          {
            return String.Empty;
          }
        }

        index = GetLastIndexOfUnqualifiedSeperator(instance, seperator, qualifier, position);
        if (index == -1)
        {
          if (fieldCount > 1)
          {
            // field index is outside the last possible index in the line.
            throw new IndexOutOfRangeException(String.Format("Index {0} is out of range in line '{1}' when using seperator (\"{2}\") and qualifier ('{3}').",
              fieldIndex,
              instance,
              seperator,
              qualifier));
          }

          index = instance.Length;
          RemoveQualifiers(instance, qualifier, ref position, ref index);
          return instance.Substring(position, index - position);
        }

        fieldCount--;
      }

      index -= seperator.Length - 1;

      RemoveQualifiers(instance, qualifier, ref position, ref index);

      return instance.Substring(position, index - position);
    }

    private static Int32 GetLastIndexOfUnqualifiedSeperator(String line, String seperator, Char qualifier, Int32 startingIndex)
    {
      // Assuming that startingIndex is not at the end of the line.
      Int32 index = startingIndex;
      Int32 seperatorIndex = 0;
      Boolean isQualified = false;
      for (; index < line.Length; index++)
      {
        if (line[index] == qualifier)
        {
          isQualified = !isQualified;
        }

        if (isQualified)
        {
          continue;
        }

        if (line[index] == seperator[seperatorIndex])
        {
          seperatorIndex++;
          if (seperatorIndex == seperator.Length)
          {
            return index;
          }

          continue;
        }

        seperatorIndex = 0;
      }

      return -1; // No unqualified seperator found.
    }

    private static void RemoveQualifiers(String line, Char qualifier, ref Int32 startingIndex, ref Int32 endingIndex)
    {
      if (line[startingIndex] == qualifier)
      {
        startingIndex++;
      }

      if (endingIndex > 0 && line[endingIndex - 1] == qualifier)
      {
        endingIndex--;
      }
    }
    #endregion
  }
}
