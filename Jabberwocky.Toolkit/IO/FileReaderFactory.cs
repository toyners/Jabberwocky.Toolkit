
namespace Jabberwocky.Toolkit.IO
{
  using System;

  class FileReaderFactory : IStreamReaderFactory
  {
    public IStreamReader CreateStreamReader(String filePath)
    {
      return new FileReader(filePath);
    }
  }
}
