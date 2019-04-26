namespace BLL.Factory.Implementation.Excel.Books
{
    using System.Linq;
    using Interfaces;
    using Models;
    using DAL.Interfaces;
    using DTO.Entities;

    internal class BookDtoImporter : IImporter
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookDtoImporter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Import(object sender, ExtractionEventArgs args)
        {
            var extractedBooks = args.ExtractedData.Cast<BookDto>().ToList();
            var repository = _unitOfWork.GetRepository<BookDto>();

            repository.InsertMultiple(extractedBooks.ToArray());

            _unitOfWork.Save();
        }
    }
}