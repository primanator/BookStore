namespace BLL.Services.Implementation
{
    using Interfaces;
    using Factory.Interfaces;
    using System.IO;

    internal class ImportService : IImportService
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

        public void Import(Stream srcStream)
        {
            _validator.ValidatonPassed += _extractor.Extract;
            _extractor.ExtractionPassed += _importer.Import;

            _validator.Validate(srcStream);
        }
    }
}