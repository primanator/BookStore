namespace BLL.Factory.Implementation.Excel.Books
{
    using System.Linq;
    using Interfaces;
    using Models;
    using DAL.Interfaces;
    using Contracts.Models;

    internal class BookImporter : IImporter
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookImporter(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Import(object sender, ExtractionEventArgs args)
        {
            var extractedBooks = args.ExtractedData.Cast<Book>().ToList();
            var repository = _unitOfWork.GetRepository<Book>();

            repository.InsertMultiple(extractedBooks.ToArray());

            _unitOfWork.Save();
        }
    }
}