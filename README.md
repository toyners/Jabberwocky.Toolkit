# Jabberwocky.Toolkit
A tookit containing general purpose interfaces, helper code and extension methods.

# Interfaces
Provides an interface for a stream reader that includes the ability to read a line (IStreamReader) and 
an interface for the creation of objects that implement this IStreamReader interface (IStreamReaderFactory)

# Extension methods
Allows the checking for null on instances and for the checking of emptiness on containers (array, lists)

# Helper code
Includes implementation of both IStreamReader (FileReader) and IStreamReaderFactory (FileReaderFactory) interfaces.
Static methods for ensuring a directory exists and that a directory path has a trailing directory seperator character.
