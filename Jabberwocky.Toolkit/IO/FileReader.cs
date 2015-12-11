
namespace Jabberwocky.Toolkit.IO
{
  using System;
  using System.IO;
  using System.Text;
  using Validation;

  /// <summary>
  /// A file reader with functionality to read a line of characters. Line terminator is '\r\n'
  /// </summary>
  public class FileReader : IStreamReader
  {
    #region Enums
    private enum TryGetNextByteResult
    {
      EndOfStream,
      EndOfFileCharacter,
      Valid
    }
    #endregion

    #region Fields
    private const Byte EF = 239;
    private const Byte BB = 187;
    private const Byte BF = 191;

    private Byte[] lineTerminator = new Byte[] { (Byte)'\r', (Byte)'\n' };

    private Int32 blockIndex;

    private Int32 blockSize = -1;

    private Byte[] buffer;

    private Boolean disposed;

    private Int64 position;

    private FileStream stream;

    private StringBuilder builder = new StringBuilder(100);
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

      if (this.stream.ReadByte() == 0)
      {
        this.SeekToEndOfFile();
        return;
      }

      this.stream.Seek(0, SeekOrigin.Begin);
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
    public String Name
    { get; private set; }

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
      if (this.EndOfStream)
      {
        return null;
      }

      Byte nextByte;
      this.builder.Clear();
      Int32 lineTerminatorIndex = 0;

      if (this.Position == 0)
      {
        this.ResolveAnyByteOrderMarks();
      }

      TryGetNextByteResult tryGetNextByteResult;
      while ((tryGetNextByteResult = this.TryGetNextByte(out nextByte)) == TryGetNextByteResult.Valid)
      {
        if (nextByte != lineTerminator[lineTerminatorIndex])
        {
          this.builder.Append((Char)nextByte);
          continue;
        }

        lineTerminatorIndex++;
        if (lineTerminatorIndex == lineTerminator.Length)
        {
          this.position += lineTerminator.Length;
          break;
        }
      }

      if (this.builder.Length != 0)
      {
        if (this.EndOfStream || tryGetNextByteResult == TryGetNextByteResult.EndOfFileCharacter)
        {
          this.SeekToEndOfFile();
        }
        else
        { 
          this.position += builder.Length;
        }

        return this.builder.ToString();
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

    private void ResolveAnyByteOrderMarks()
    {
      Byte firstByte, secondByte, thirdByte;
      this.TryGetNextByte(out firstByte);

      if (firstByte == EF && this.TryGetNextByte(out secondByte) == TryGetNextByteResult.Valid)
      {
        if (secondByte == BB && this.TryGetNextByte(out thirdByte) == TryGetNextByteResult.Valid)
        {
          if (thirdByte != BF)
          {
            this.builder.Append((Char)firstByte);
            this.builder.Append((Char)secondByte);
            this.builder.Append((Char)thirdByte);
          }

          // Ensure that the position is set correctly to account for the BOM
          this.position = 3;
        }
        else
        {
          this.builder.Append((Char)firstByte);
          this.builder.Append((Char)secondByte);
        }
      }
      else
      {
        this.builder.Append((Char)firstByte);
      }
    }

    private TryGetNextByteResult TryGetNextByte(out Byte nextByte)
    {
      if (this.EndOfStream)
      {
        nextByte = 0;
        return TryGetNextByteResult.EndOfStream;
      }

      if (this.BlockIsEmpty)
      {
        this.ReadNextBlock();
      }

      nextByte = this.buffer[this.blockIndex++];
      return (nextByte != 0 ? TryGetNextByteResult.Valid : TryGetNextByteResult.EndOfFileCharacter);
    }

    private void ReadNextBlock()
    {
      this.blockSize = this.stream.Read(this.buffer, 0, this.buffer.Length);
      this.blockIndex = 0;
    }

    private void SeekToEndOfFile()
    {
      this.Position = this.Length;
      this.blockSize = -1;
    }
    #endregion
  }
}
