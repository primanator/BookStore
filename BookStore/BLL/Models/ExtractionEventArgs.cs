namespace BLL.Models
{
    using DTO.Entities;
    using System;
    using System.Collections.Generic;

    internal class ExtractionEventArgs : EventArgs
    {
        public List<Dto> ExtractedData { get; set; }
    }
}