namespace UI.ContentExtractors
{
    using System;
    using UI.ContentExtractors.Interfaces;

    internal class FileContentExtractor<T> : IContentExtractor<T>
    {
        public string GetContentName()
        {
            Console.WriteLine("\nEnter path to the file you want to import: ");
            return Console.ReadLine();
        }

        public T GetFullContent()
        {
            throw new NotImplementedException();
        }
    }
}