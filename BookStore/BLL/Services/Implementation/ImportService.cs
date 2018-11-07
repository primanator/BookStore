namespace BLL.Services.Implementation
{
    using Interfaces;
    using Factory.Interfaces;
    using System.IO;

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

        public Stream Import(Stream source)
        {
            //var importData = source, _validator.SourceMap);
            //_importer.Import(importData);
            //_extractor.Export();
            _validator.ImportValidated =+ _extractor.Extract();

            _validator.Validate(source);
            return null;
        }
    }
}