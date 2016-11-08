using System.IO;
using System.Text;

namespace DBO.Data.Objects
{
    /// <summary>
    /// Encapsulates a binary reader which does not close the underlying stream.
    /// </summary>
    public class NoCloseBinaryReader : BinaryReader
    {
        /// <summary>
        /// Creates a new binary reader object.
        /// </summary>
        /// <param name="stream">The underlying stream to write to.</param>
        /// <param name="encoding">The encoding for the stream.</param>
        public NoCloseBinaryReader(Stream stream, Encoding encoding)
            : base(stream, encoding)
        {
        }

        /// <summary>
        /// Creates a new binary reader object using default encoding.
        /// </summary>
        /// <param name="stream">The underlying stream to write to.</param>
        /// <param name="encoding">The encoding for the stream.</param>
        public NoCloseBinaryReader(Stream stream)
            : base(stream)
        {
        }

        /// <summary>
        /// Disposes of the binary reader.
        /// </summary>
        /// <param name="disposing">True to dispose managed objects.</param>
        protected override void Dispose(bool disposeManaged)
        {
            BaseStream.Position = 0;
            // Dispose the binary reader but pass false to the dispose
            // method to stop it from closing the underlying stream
            base.Dispose(false);
        }
    }
}
