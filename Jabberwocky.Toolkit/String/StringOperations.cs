
namespace Jabberwocky.Toolkit.String
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public static class StringOperations
  {
    #region Methods
    public static String ExtractField(this String instance, String seperator, Char qualifier, UInt32 fieldIndex)
    {
      Int32 position = 0;
      Int32 index = 0;
      while (true)
      {
        Int32 seperateIndex = 0;

        Boolean isQualified = false;
        for (; index < instance.Length; index++)
        {
          if (instance[index] == qualifier)
          {
            isQualified = !isQualified;
          }

          if (!isQualified && instance[index] == seperator[seperateIndex])
          {
            break;
          }
        }

        if (index == -1)
        {
          return instance.Substring(position, instance.Length - position);
        }

        seperateIndex++;
        Boolean gotSeperator = true;
        for (; seperateIndex < seperator.Length; seperateIndex++)
        {
          index++;
          if (index == instance.Length)
          {
            // End of line. Seperator was not completed.
            break;
          }

          if (instance[index] != seperator[seperateIndex])
          {
            gotSeperator = false;
            break;
          }
        }

        if (!gotSeperator)
        {
          continue;
        }

        if (fieldIndex == 1)
        {
          position = index;
        }
        else if (fieldIndex == 0)
        {
          //Int32 count = index - position;
          if (instance[position] == qualifier)
          {
            position++;
          }

          if (index > 0 && instance[index - 1] == qualifier)
          {
            index--;
          }

          return instance.Substring(position, index - position);
        }

        fieldIndex--;
      }

      throw new NotImplementedException();
    }
    #endregion
  }
}
