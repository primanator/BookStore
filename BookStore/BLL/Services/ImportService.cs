namespace BLL.Services
{
    using BLL.Interfaces;
    using DAL.Interfaces;
    using DTO.Entities;
    using System.Web;

    public class ImportService : IImportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IValidator _validator;

        public ImportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Execute<T>(HttpPostedFile importSource, IValidator<T> validator) where T : Dto
        {
            _validator = validator;

            var importFileIsValid = ((IValidator<T>)_validator).Check(importSource);

            if (importFileIsValid)
            {
                PerformImport<T>(importSource);
                //return null;
            }
            else
            {
                // return updated file
            }
        }

        private void PerformImport<T>(HttpPostedFile importSource) where T : Dto
        {
            var importData = ((IValidator<T>)_validator).Extract(importSource);
            var repository = _unitOfWork.GetRepository<T>();

            importData.ForEach(newItem => repository.Insert(newItem));
            _unitOfWork.Save();
        }
    }
}