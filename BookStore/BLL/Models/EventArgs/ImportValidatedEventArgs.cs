namespace BLL.Models.EventArgs
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class ImportValidatedEventArgs : EventArgs
    {
        public Dictionary<string, int> SourceMap { get; set; }
        public Stream Source { get; set; }
    }
}