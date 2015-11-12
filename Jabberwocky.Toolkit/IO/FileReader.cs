
namespace Jabberwocky.Toolkit.IO
{
  using System;
  using System.IO;
  using System.Text;
  using Jabberwocky.Toolkit.Validation;

  /// <summary>
  /// A file reader with functionality to read a line of characters. Line terminator is '\r\n'
  /// </summary>
  public class FileReader : IStreamReader
  {
    #region Fields
    private Byte[] lineTerminator = new Byte[] { (Byte)'\r', (Byte)'\n' };

    private Int32 blockIndex;

    private Int32 blockSize = -1;

    private Byte[] buffer;

    private Boolean disposed;

    private Int64 position;

    private FileStream stream;
    #endregion

    #region Construction
    /// <summary>
    /// Initializes a new instance of the FileReader class.
    /// </summary>
    /// <param name="path">Full path to the file.</param>
    public FileReader(String path) : this(path, 8192)
    {
    }

    /// <summary>
    /// Initializes a new instance of the FileReader class.
    /// </summary>
    /// <param name="path">Full path to the file.</param>
    /// <param name="bufferSize">Size of buffer to store characters from underlying stream.</param>
    public FileReader(String path, Int32 bufferSize)
    {
      path.VerifyThatStringIsNotNullAndNotEmpty("Parameter 'path' is null or empty.");
      if (bufferSize <= 0)
      {
        throw new Exception("Parameter 'bufferSize' must be a positive non-zero integer");
      }

      this.stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 8192, FileOptions.RandomAccess);
      this.buffer = new Byte[bufferSize];
      this.Name = path;
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets a boolean indicating whether the reading position is at the end of the stream.
    /// </summary>
    public Boolean EndOfStream
    {
      get 
      {
        return this.stream.Position == this.stream.Length && this.BlockIsEmpty;
      }
    }

    /// <summary>
    /// Gets or sets the position to read from within the stream.
    /// </summary>
    public Int64 Position
    {
      get 
      {
        return this.position;
      }

      set 
      {
        Int64 difference = value - this.position;
        if (Math.Abs(difference) > this.blockSize)
        {
          this.blockSize = -1;
          this.position = this.stream.Position = value;
          return;
        }

        this.blockIndex += (Int32)difference;
        if (this.blockIndex < 0 || this.blockIndex >= this.blockSize)
        {
          this.blockSize = -1;
          this.position = this.stream.Position = value;
          return;
        }


        this.position += (Int32)difference;
      }
    }

    /// <summary>
    /// Gets the lenght of the stream in bytes.
    /// </summary>
    public Int64 Length
    {
      get
      {
        return this.stream.Length;
      }
    }

    /// <summary>
    /// Gets the full name of the file. 
    /// </summary>
    public String Name { get; private set; }

    private Boolean BlockIsEmpty 
    { 
      get 
      { 
        return this.blockSize == -1 || this.blockIndex >= this.blockSize; 
      }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Closes and disposes the underlying stream.
    /// </summary>
    public void Close()
    {
      this.Dispose(true);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Reads a line from the stream.
    /// </summary>
    /// <returns>The line from the stream.</returns>
    public String ReadLine()
    {
      Byte nextByte;
      StringBuilder builder = new StringBuilder(100);
      Int32 lineTerminatorIndex = 0;

      while (this.TryGetNextByte(out nextByte))
      {
        if (nextByte != lineTerminator[lineTerminatorIndex])
        {
          builder.Append((Char)nextByte);
          continue;
        }

        lineTerminatorIndex++;
        if (lineTerminatorIndex == lineTerminator.Length)
        {
          this.position += lineTerminator.Length;
          break;
        }
      }

      if (builder.Length != 0)
      {
        this.position += builder.Length;
        return builder.ToString();
      }

      return null;
    }

    protected virtual void Dispose(Boolean disposing)
    {
      if (this.disposed)
      {
        return;
      }

      if (disposing)
      {
        this.buffer = null;
        if (this.stream != null)
        {
          this.stream.Dispose();
          this.stream = null;
        }
      }

      this.disposed = true;
    }

    private Boolean TryGetNextByte(out Byte nextByte)
    {
      if (this.EndOfStream)
      {
        nextByte = 0;
        return false;
      }

      if (this.BlockIsEmpty)
      {
        this.ReadNextBlock();
      }

      nextByte = this.buffer[this.blockIndex++];
      return true;
    }

    private void ReadNextBlock()
    {
      this.blockSize = this.stream.Read(this.buffer, 0, this.buffer.Length);
      this.blockIndex = 0;
    }
    #endregion
  }
}
