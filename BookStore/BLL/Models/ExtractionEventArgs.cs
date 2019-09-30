namespace BLL.Models
{
    using Contracts.Models;
    using System;
    using System.Collections.Generic;

    internal class ExtractionEventArgs : EventArgs
    {
        public List<BaseContract> ExtractedData { get; set; }
    }
}