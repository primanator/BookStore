namespace BLL.Interfaces
{
    using System.Collections.Generic;
    using DTO;

    public interface IBookStoreService
    {
        void CreateRecord<T>(T newRecord) where T : Dto;
        void UpdateRecord<T>(T freshRecord) where T : Dto;
        IEnumerable<T> GetAllRecords<T>() where T : Dto;
        T GetSingleRecord<T>(string searchKey = null) where T : Dto;
        void DeleteRecord<T>(T recordToDelete) where T : Dto;
        IEnumerable<AuthorDto> Get21CenturyAuthors();
    }
}