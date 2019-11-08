namespace Contracts.Models
{
    using System;
    using System.Collections.Generic;

    public class Book : BaseContract
    {
        public Book()
        {
        }

        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
    }
}