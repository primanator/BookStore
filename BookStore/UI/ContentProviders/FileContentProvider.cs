namespace UI.ContentProviders
{
    using System;
    using System.Net;
    using UI.ContentProviders.Interfaces;

    internal class FileContentProvider<T> : IContentProvider<T>
    {
        public T GetContent()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            Console.WriteLine("\nEnter path to the file you want to import: ");
            return Console.ReadLine();
        }

        public WebHeaderCollection GetHeaders()
        {
            return new WebHeaderCollection();
        }
    }
}