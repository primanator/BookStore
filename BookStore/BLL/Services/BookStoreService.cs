namespace BLL.Services
{
    using System;
    using System.Collections.Generic;
    using DTO;
    using Interfaces;
    using AutoMapper;
    using DAL.Interfaces.UnitOfWork;
    using DAL.Interfaces.Repository;
    using System.Linq;

    public class BookStoreService : IBookStoreService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookStoreService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void UpdateRecord<T>(T freshRecord) where T : Dto
        {
            Type recordType = freshRecord.GetType();
            T recordToUpdate = recordType != typeof(LibraryDto) ? GetSingleRecord<T>(freshRecord.Name) : GetSingleRecord<T>();

            if (recordToUpdate == null)
                return;

            foreach (var property in recordToUpdate.GetType().GetProperties())
            {
                var newValue = recordType.GetProperty(property.Name)?.GetValue(freshRecord);
                property.SetValue(recordToUpdate, newValue);
            }

            GetRepository<T>()?.Update(recordToUpdate);
        }

        public void CreateRecord<T>(T newRecord) where T : Dto
        {
            GetRepository<T>()?.Insert(newRecord);
        }

        public IEnumerable<T> GetAllRecords<T>() where T : Dto
        {
            return Mapper.Map<IEnumerable<T>>(GetRepository<T>()?.FindBy(r => true));
        }

        public T GetSingleRecord<T>(string searchKey = null) where T : Dto
        {
            if (searchKey == null)
                return Mapper.Map<T>(GetRepository<T>()?.FindBy(l => true).FirstOrDefault());
            return Mapper.Map<T>(GetRepository<T>()?.FindBy(r => r.Name == searchKey).FirstOrDefault());
        }

        public void DeleteRecord<T>(T recordToDelete) where T : Dto
        {
            GetRepository<T>()?.Delete(recordToDelete);
        }

        public IEnumerable<AuthorDto> Get21CenturyAuthors()
        {
            return Mapper.Map<IEnumerable<AuthorDto>>(_unitOfWork.GetAuthorRepository().Get21CenturyAuthors());
        }

        private IGenericRepository<T> GetRepository<T>() where T : Dto
        {
            switch (typeof(T).Name)
            {
                case nameof(AuthorDto):
                    return (IGenericRepository<T>)_unitOfWork.GetAuthorRepository();
                case nameof(BookDto):
                    return (IGenericRepository<T>)_unitOfWork.GetBookRepository();
                case nameof(CountryDto):
                    return (IGenericRepository<T>)_unitOfWork.GetCountryRepository();
                case nameof(GenreDto):
                    return (IGenericRepository<T>)_unitOfWork.GetGenreRepository();
                case nameof(LibraryDto):
                    return (IGenericRepository<T>)_unitOfWork.GetLibraryRepository();
                case nameof(LiteratureFormDto):
                    return (IGenericRepository<T>)_unitOfWork.GetLiteratureFormRepository();
                case nameof(UserDto):
                    return (IGenericRepository<T>)_unitOfWork.GetUserRepository();
                default:
                    return null;
            }
        }
    }
}