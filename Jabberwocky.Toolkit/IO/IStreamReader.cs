
namespace Jabberwocky.Toolkit.IO
{
  using System;

  public interface IStreamReader : IDisposable
  {
    #region Properties
    Boolean EndOfStream { get; }

    Int64 Position { get; set; }
    #endregion

    #region Methods
    String ReadLine();

    void Close();
    #endregion
  }
}
