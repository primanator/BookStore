namespace API.Controllers
{
    using System.Web;
    using System.Web.Http;
    using BLL.Factory.Interfaces;
    using Ninject;

    public class ImportController : ApiController
    {
        private readonly IImportServiceFactory _importServiceFactory;

        public ImportController([Named("Excel")] IImportServiceFactory importServiceFactory)
        {
            _importServiceFactory = importServiceFactory;
        }

        public IHttpActionResult PostImportBooks()
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
                return BadRequest("Received no files.");

            var importService = _importServiceFactory.GetBookImportService();
            var result = importService.Import(httpRequest.Files[0]);

            if (result == null)
            {
                return Ok("Import successfully performed.");
            }
            return Ok(result);
        }
    }
}