
namespace Jabberwocky.Toolkit.Logging
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public interface ILogger
  {
    void Message(String message);

    void Message(String message, Boolean lineBreak);

    void Exception(String message);
  }
}
