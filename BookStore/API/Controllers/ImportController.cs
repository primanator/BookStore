namespace API.Controllers
{
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using BLL.Factory.Interfaces;
    using DTO.Utils;
    using Ninject;

    public class ImportController : ApiController
    {
        private class FailedImportResult : IHttpActionResult
        {
            private readonly Stream _srcStream;
            private readonly HttpRequestMessage _request;
            private readonly string _failReason;

            public FailedImportResult(HttpRequestMessage request, Stream srcStream, string failReason)
            {
                srcStream.Position = 0;
                _srcStream = srcStream;
                _request = request;
                _failReason = failReason;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK)
                {
                    Content = new StreamContent(_srcStream),
                    ReasonPhrase = _failReason,
                    RequestMessage = _request
                };
                result.Content.Headers.ContentLength = _srcStream.Length;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "importFileForEdit"
                };

                return Task.FromResult(result);
            }
        }

        private readonly IImportServiceFactory _importServiceFactory;

        public ImportController([Named("Excel")] IImportServiceFactory importServiceFactory)
        {
            _importServiceFactory = importServiceFactory;
        }

        public IHttpActionResult PostImportBooks()
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            try
            {
                _importServiceFactory
                    .GetBookImportService()
                    .Import(HttpContext.Current.Request.InputStream);
            }
            catch (FailedImportException ex)
            {
                return new FailedImportResult(Request, ex.ImportSource, ex.Message);
            }
            return Ok("Import successfully performed.");
        }
    }
}