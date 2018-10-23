namespace API.Controllers
{
    using BLL.Interfaces;
    using DTO.Entities;
    using System.Web;
    using System.Web.Http;

    public class ImportController : ApiController
    {
        private readonly IImportService _importService;

        public ImportController(IImportService service)
        {
            _importService = service;
        }

        public IHttpActionResult PostImportBooks(IValidator validator)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
                return BadRequest("Received no files.");

            _importService.Execute<BookDto>(httpRequest.Files[0], (IValidator<BookDto>)validator);

            return Ok("Import successfully performed.");
        }
    }
}