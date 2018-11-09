namespace API.Controllers
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;
    using BLL.Factory.Interfaces;
    using Ninject;

    public class ImportController : ApiController
    {
        private class FailedImportResult : IHttpActionResult
        {
            private readonly Stream _srcStream;
            private readonly HttpRequestMessage _request;
            private readonly string _failReason;

            public FailedImportResult(string failReason, Stream srcStream, HttpRequestMessage request)
            {
                _srcStream = srcStream;
                _request = request;
                _failReason = failReason;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                var response = _request.CreateResponse(HttpStatusCode.BadRequest);

                using (var memoryStream = new MemoryStream())
                {
                    _srcStream.CopyTo(memoryStream);
                    response.Content = new StreamContent(memoryStream);
                }

                response.ReasonPhrase = _failReason;
                response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "validated_import_file"
                };

                return Task.FromResult(response);
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

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count < 1)
                return BadRequest("Received no files.");

            var importService = _importServiceFactory.GetBookImportService();
            var importStream = httpRequest.Files[0].InputStream;

            try
            {
                importService.Import(importStream);
            }
            catch(FormatException ex)
            {
                return new FailedImportResult(ex.Message, importStream, Request);
            }
            return Ok("Import successfully performed.");
        }
    }
}