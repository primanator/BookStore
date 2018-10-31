namespace BLL.Services.Implementation
{
    using System;
    using System.Web;
    using Interfaces;
    using Factory.Interfaces;

    public class ImportService : IImportService
    {
        private readonly IValidator _validator;
        private readonly IExtractor _extractor;
        private readonly IImporter _importer;

        public ImportService(IValidator validator, IExtractor extractor, IImporter importer)
        {
            _validator = validator;
            _extractor = extractor;
            _importer = importer;
        }

        public HttpPostedFile Import(HttpPostedFile source)
        {
            if(!Validate(source))
            {
                return source;
            }
            var importData = _extractor.Extract(source);
            _importer.Import(importData);

            return null;
        }

        private bool Validate(HttpPostedFile source)
        {
            if (!_validator.CheckStructure(source, out string failReason))
            {
                throw new ArgumentException(failReason);
            }
            return _validator.CheckContent(source);
        }
    }
}