namespace UI.ContentProviders
{
    using Contracts.Models;
    using System;
    using System.Data.Entity.Design.PluralizationServices;
    using System.Globalization;
    using System.Net;
    using UI.ContentProviders.Interfaces;

    internal class DtoContentProvider<T> : IContentProvider<T>
        where T : BaseContract, new()
    {
        private readonly PluralizationService PluralizationService;

        public DtoContentProvider()
        {
            PluralizationService = PluralizationService.CreateService(CultureInfo.CurrentCulture);
        }

        public T GetContent()
        {
            return BaseContract.GetFromUser<T>();
        }

        public WebHeaderCollection GetHeaders()
        {
            return new WebHeaderCollection();
        }

        public string GetRequiredItemName()
        {
            Console.WriteLine($"\nEnter the name of {typeof(T)} you want to perform action on: ");
            return Console.ReadLine();
        }

        public string GetContentPluralName()
        {
            return PluralizationService.Pluralize(typeof(T).Name).ToLowerInvariant();
        }
    }
}