namespace BLL.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class ValidationEventArgs : EventArgs
    {
        public Stream SourceStream { get; set; }

        public Dictionary<string, int> SourceMap { get; set; }

        public int EntriesAmount { get; set; }
    }
}