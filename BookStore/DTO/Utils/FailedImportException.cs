namespace DTO.Utils
{
    using System;
    using System.IO;

    public class FailedImportException : Exception
    {
        public Stream ImportSource { private set; get; }

        public FailedImportException(Stream importSource, string message) : base(message)
        {
            ImportSource = importSource;
        }
    }
}