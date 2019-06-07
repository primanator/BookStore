namespace UI
{
    using System.IO;
    using System.Net;

    internal class Post : ApiRequest
    {
        public Post(string requestUriString, WebHeaderCollection headers) : base(requestUriString, headers)
        {
            _webRequest.Method = "POST";
            _webRequest.ContentType = "application/x-www-form-urlencoded";
        }

        public HttpStatusCode Send(byte[] importBytes)
        {
            WriteBytesToRequest(importBytes);

            _webResponse = _webRequest.GetResponse();
            var httpWebResponse = (HttpWebResponse)_webResponse;

            if (httpWebResponse.StatusCode == HttpStatusCode.Created)
            {
                var objStream = httpWebResponse.GetResponseStream();

                using (var binaryReader = new BinaryReader(objStream))
                {
                    var data = binaryReader.ReadBytes((int)_webResponse.ContentLength);
                    File.WriteAllBytes("returnedDoc.xlsx", data);
                }
            }

            return httpWebResponse.StatusCode;
        }

        private void WriteBytesToRequest(byte[] importBytes)
        {
            _webRequest.ContentLength = importBytes.Length;
            var requestStream = _webRequest.GetRequestStream();
            requestStream.Write(importBytes, 0, importBytes.Length);
            requestStream.Close();
        }
    }
}
