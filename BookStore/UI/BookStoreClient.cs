namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using DTO.Entities;

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

        private async Task<BookDto[]> GetAllBooksAsync()
        {
            _response = await _client.GetAsync("api/books");
            CheckResponse();

            return await _response.Content.ReadAsAsync<BookDto[]>();
        }

        private async Task<BookDto> GetBookByTitleAsync(string title)
        {
            _response = await _client.GetAsync("api/books?name=" + title);
            CheckResponse();

            return await _response.Content.ReadAsAsync<BookDto>();
        }

        private async Task<string> PostNewBookAsync(BookDto newBook)
        {
            _response = await _client.PostAsJsonAsync("api/books", newBook);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> PutUpdateBookAsync(BookDto newBook)
        {
            _response = await _client.PutAsJsonAsync("api/books", newBook);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> DeleteBookAsync(string title)
        {
            _response = await _client.DeleteAsync("api/books?name=" + title);
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private async Task<string> PostDocument(FileInfo file)
        {
            HttpContent fileStreamContent = null;
            try
            {
                fileStreamContent = new StreamContent(file.OpenRead());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "runtime error occured";
            }

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent, "importDocument", file.Name);
                _response = await _client.PostAsync("api/documents", formData);
            }
            CheckResponse();

            return await _response.Content.ReadAsStringAsync();
        }

        private void CheckResponse()
        {
            if (!_response.IsSuccessStatusCode)
                Console.WriteLine("Request returned with failed status code: " + _response.StatusCode);
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        public async Task SendRequestAsync(string httpVerb)
        {
            switch (httpVerb.ToLowerInvariant())
            {
                case "get":
                    {
                        Console.WriteLine("Enter name (leave empty to choose all):");
                        string name = Console.ReadLine();

                        if (string.IsNullOrEmpty(name))
                        {
                            BookDto[] books = await GetAllBooksAsync();
                            if (books != null)
                                foreach (var book in books)
                                    Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
                                        book.Isbn, book.Pages, book.LimitedEdition);
                            else
                                Console.WriteLine("There are no books in the library.");
                        }
                        else
                        {
                            BookDto book = await GetBookByTitleAsync(name);
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
                        var newBook = GetBookFromUserInput();

                        var result = await PostNewBookAsync(newBook);
                        Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                        break;
                    }
                case "import":
                    {
                        Console.WriteLine("Enter path to the .xlsx file you want to import: ");
                        var path = Console.ReadLine();

                        FileInfo toImport = null;
                        try
                        {
                            toImport = new FileInfo(path);
                            if (toImport.Extension != "xlsx")
                                throw new ArgumentException("Input file is not in .xlsx format.");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.Message);
                            break;
                        }

                        var result = await PostDocument(toImport);
                        Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                        break;
                    }
                case "put":
                    {
                        Console.WriteLine("Enter title of the book you want to update: ");
                        var title = Console.ReadLine();

                        var bookToUpdate = await GetBookByTitleAsync(title);
                        if (bookToUpdate != null)
                        {
                            CopyBookWithUserUpdates(bookToUpdate);
                            var result = await PutUpdateBookAsync(bookToUpdate);
                            Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                        }
                        else
                            Console.WriteLine("There is no such book in the library.");

                        break;
                    }
                case "delete":
                    {
                        Console.WriteLine("Enter name:");
                        string toDelete = Console.ReadLine();

                        var result = await DeleteBookAsync(toDelete);
                        Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Client does not support such method.");
                        break;
                    }
            }
        }

        public static BookDto GetBookFromUserInput()
        {
            BookDto newBook = new BookDto();

            var properties = newBook.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (!prop.Name.Contains("Id"))
                {
                    Console.WriteLine("Please enter book's {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(newBook, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            newBook.LibraryId = 1;
            return newBook;
        }

        public static BookDto CopyBookWithUserUpdates(BookDto bookToCopy)
        {
            var properties = bookToCopy.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "Name" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter book's {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(bookToCopy, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            return bookToCopy;
        }
    }
}