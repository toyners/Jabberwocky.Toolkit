
namespace Jabberwocky.Toolkit.String
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Extension methods for the string class.
  /// </summary>
  public static class StringExtensions
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

    /// <summary>
    /// Returns a pluralised version of the string (appended s character) if the count is not 1.
    /// </summary>
    /// <param name="instance">Word to pluralize.</param>
    /// <param name="count">Number of items.</param>
    /// <returns>Plural form of the word if count is not one; otherwise returns the original word.</returns>
    public static String Pluralize(this String instance, UInt32 count)
    {     
      return instance + (count != 1 ? "s" : null);
    }

    /// <summary>
    /// Substitute fragments within a string based on a dictionary of find and replace values. 
    /// </summary>
    /// <param name="instance">String to substitute values.</param>
    /// <param name="dictionary">Contains key to find in the string and values to replace them with.</param>
    /// <returns>String with values substituted.</returns>
    public static String Substitute(this String instance, IDictionary<String, String> dictionary)
    {
      System.Text.StringBuilder sb = new System.Text.StringBuilder(instance);
      
      foreach (KeyValuePair<String, String> pair in dictionary)
      {
        sb = sb.Replace(pair.Key, pair.Value);
      }

      return sb.ToString();
    }

    /// <summary>
    /// Verifies that the string is not null and not empty. Throws an exception if verification fails.
    /// </summary>
    /// <param name="instance">String to verify.</param>
    public static void VerifyThatStringIsNotNullAndNotEmpty(this String instance)
    {
      instance.VerifyThatStringIsNotNullAndNotEmpty("String is null or empty.");
    }

    /// <summary>
    /// Verifies that the string is not null and not empty. Throws an exception (with custom message) if verification fails.
    /// </summary>
    /// <param name="instance">String to verify.</param>
    /// <param name="exceptionMessage">Custom message to use in exception.</param>
    public static void VerifyThatStringIsNotNullAndNotEmpty(this String instance, String exceptionMessage)
    {
      if (String.IsNullOrEmpty(instance))
      {
        throw new Exception(exceptionMessage);
      }
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
