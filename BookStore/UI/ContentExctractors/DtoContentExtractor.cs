namespace UI.ContentExtractors
{
    using DTO.Entities;
    using System;
    using UI.ContentExtractors.Interfaces;

    internal class DtoContentExtractor<T> : IContentExtractor<T>
        where T: Dto, new()
    {
        public string GetContentName()
        {
            Console.WriteLine("\nEnter path to the file you want to import: ");
            return Console.ReadLine();
        }

        public T GetFullContent()
        {
            return Dto.GetFromUser<T>();
        }
    }
}