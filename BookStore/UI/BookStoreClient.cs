namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    internal class BookStoreClient : IDisposable
    {
        private readonly HttpClient _client;
        private HttpResponseMessage _response;

        public BookStoreClient(Uri baseAddress, IEnumerable<MediaTypeWithQualityHeaderValue> headers)
        {
            _client = new HttpClient
            {
                BaseAddress = baseAddress
            };

            _client.DefaultRequestHeaders.Accept.Clear();

            foreach (var header in headers)
                _client.DefaultRequestHeaders.Accept.Add(header);
        }

        private async Task<Book[]> GetAllBooks()
        {
            _response = await _client.GetAsync("api/books");
            CheckResponse();

            return await _response.Content.ReadAsAsync<Book[]>();
        }

        private async Task<Book> GetBookByTitle(string title)
        {
            _response = await _client.GetAsync("api/books?name=" + title);
            CheckResponse();

            return await _response.Content.ReadAsAsync<Book>();
        }

        private async Task<string> PostNewBook(Book newBook)
        {
            _response = await _client.PostAsJsonAsync("api/books", newBook);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> PutUpdateBook(Book newBook)
        {
            _response = await _client.PutAsJsonAsync("api/books", newBook);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> DeleteBook(string title)
        {
            _response = await _client.DeleteAsync("api/books?name=" + title);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private void CheckResponse()
        {
            if (!_response.IsSuccessStatusCode)
                throw new HttpRequestException("Request returned with failed status code: " + _response.StatusCode);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task SendRequest(string httpVerb)
        {

            switch (httpVerb.ToLowerInvariant())
            {
                case "get":
                    {
                        Console.WriteLine("Enter name (leave empty to choose all):");
                        string name = Console.ReadLine();

                        if (string.IsNullOrEmpty(name))
                        {
                            Book[] books = await GetAllBooks();
                            if (books != null)
                                foreach (var book in books)
                                    Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
                                        book.Isbn, book.Pages, book.LimitedEdition);
                            else
                                Console.WriteLine("There are no books in the library.");
                        }
                        else
                        {
                            Book book = await GetBookByTitle(name);
                            if (book != null)
                                Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
                                    book.Isbn, book.Pages, book.LimitedEdition);
                            else
                                Console.WriteLine("There is no such book in the library.");
                        }
                        break;
                    }
                case "post":
                    {
                        Console.WriteLine("Create new book for library.");
                        var newBook = Book.GetBookFromUserInput();

                        var result = await PostNewBook(newBook);
                        Console.WriteLine(string.IsNullOrEmpty(result) ? "Operation was succesfull." : result);
                        break;
                    }
                case "put":
                    {
                        Console.WriteLine("Enter title of the book you want to update: ");
                        var title = Console.ReadLine();

                        var bookToUpdate = await GetBookByTitle(title);
                        if (bookToUpdate != null)
                        {
                            Book.CopyBookWithUserUpdates(bookToUpdate);
                            var result = await PutUpdateBook(bookToUpdate);
                            Console.WriteLine(string.IsNullOrEmpty(result) ? "Operation was succesfull." : result);
                        }
                        else
                            Console.WriteLine("There is no such book in the library.");

                        break;
                    }
                case "delete":
                    {
                        Console.WriteLine("Enter name:");
                        string toDelete = Console.ReadLine();

                        var result = await DeleteBook(toDelete);
                        Console.WriteLine(string.IsNullOrEmpty(result) ? "Operation was succesfull." : result);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Client does not support such method.");
                        break;
                    }
            }
        }
    }
}