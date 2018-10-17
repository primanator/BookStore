namespace API.Controllers
{
    using BLL.Interfaces;
    using System.Web;
    using System.Web.Http;

    public class DocumentsController : ApiController
    {
        private readonly IDocumentService _documentService;

        public DocumentsController(IDocumentService service)
        {
            _documentService = service;
        }

        public IHttpActionResult PostImportFile()
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
                return BadRequest("Received no files.");

            _documentService.PerformImport(httpRequest.Files[0].InputStream);

            return Ok("Import successfully performed.");
        }
    }
}