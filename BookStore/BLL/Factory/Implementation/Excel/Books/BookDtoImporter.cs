namespace BLL.Factory.Implementation.Excel.Books
{
    using System.Collections.Generic;
    using BLL.Factory.Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;

    public class BookDtoImporter : IImporter
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookDtoImporter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Import(List<Dto> importData)
        {
            var repository = _unitOfWork.GetRepository<BookDto>();
            importData.ForEach(newItem => repository.Insert((BookDto)newItem));
            _unitOfWork.Save();
        }
    }
}