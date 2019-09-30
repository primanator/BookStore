namespace UI.ContentProviders
{
    using Contracts.Models;
    using System;
    using System.Net;
    using UI.ContentProviders.Interfaces;

    internal class DtoContentProvider<T> : IContentProvider<T>
        where T: BaseContract, new()
    {
        public T GetContent()
        {
            return BaseContract.GetFromUser<T>();
        }

        public string GetName()
        {
            Console.WriteLine($"\nEnter the name of {typeof(T)} you want to perform action on: ");
            return Console.ReadLine();
        }

        public WebHeaderCollection GetHeaders()
        {
            return new WebHeaderCollection();
        }
    }
}