
namespace Jabberwocky.Toolkit.IO
{
  using System;

  interface IStreamReaderFactory
  {
    IStreamReader CreateStreamReader(String filePath);
  }
}
