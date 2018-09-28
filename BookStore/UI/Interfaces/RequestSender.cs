namespace UI.Interfaces
{
    using DTO.Entities;
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    internal abstract class RequestSender<T>: IDisposable
        where T: Entity
    {
        protected HttpClient client;
        protected HttpResponseMessage response;

        public RequestSender()
        {
            client = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["Uri"])
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual async Task<T[]> SendAsync(){ return new T[] { }; }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}