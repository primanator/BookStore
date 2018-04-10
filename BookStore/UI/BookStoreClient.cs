namespace UI
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    class BookStoreClient
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Action:");
                string id = Console.ReadLine();

                GetRequest(id).Wait();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static async Task GetRequest(string ID)
        {
            switch (ID.ToLowerInvariant())
            {
                case "get":
                {
                    Console.WriteLine("Enter name (leave empty to choose all):");
                    string name = Console.ReadLine();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        if (string.IsNullOrEmpty(name))
                        {
                            response = await client.GetAsync("api/books");
                            if (response.IsSuccessStatusCode)
                            {
                                Book[] books = await response.Content.ReadAsAsync<Book[]>();
                                if (books != null)
                                    foreach (var report in books)
                                        Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", report.Id, report.Name,
                                            report.Isbn, report.Pages, report.LimitedEdition);
                                else
                                    Console.WriteLine("There are no books in the library.");
                            }
                        }
                        else
                        {
                            response = await client.GetAsync("api/books?name=" + name);
                            if (response.IsSuccessStatusCode)
                            {
                                Book book = await response.Content.ReadAsAsync<Book>();
                                if (book != null)
                                    Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", book.Id, book.Name, book.Isbn,
                                        book.Pages, book.LimitedEdition);
                                else
                                    Console.WriteLine("There is no such book in the library.");
                            }
                        }
                    }

                    break;
                }
                case "post":
                {
                    Console.WriteLine("Create new book for library.");
                    var newBook = GetBookFromUserInput();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        ShowResponceAsync(await client.PostAsJsonAsync("api/books", newBook));
                    }

                    break;
                }
                case "put":
                {
                    Console.WriteLine("Enter Title of the book you want to update: ");
                    var nameBook = Console.ReadLine();

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        var response = await client.GetAsync("api/books?name=" + nameBook);
                        if (response.IsSuccessStatusCode)
                        {
                            var bookToUpdate = await response.Content.ReadAsAsync<Book>();
                            if (bookToUpdate != null)
                            {
                                ShowResponceAsync(await client.PutAsJsonAsync("api/books", CopyBookWithUserUpdates(bookToUpdate)));
                            }
                            else
                                Console.WriteLine("There is no such book in the library.");
                        }
                    }

                    break;
                }
                case "delete":
                {
                    Console.WriteLine("Enter name:");
                    string toDelete = Console.ReadLine();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        ShowResponceAsync(await client.DeleteAsync("api/books?name=" + toDelete));
                    }

                    break;
                }
            }
        }

        private static async void ShowResponceAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(string.IsNullOrEmpty(content) ? "Operation was succesfull." : content);
        }

        private static Book GetBookFromUserInput()
        {
            Book newBook = new Book();

            var properties = newBook.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(newBook, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            newBook.LibraryId = 1;
            return newBook;
        }

        private static Book CopyBookWithUserUpdates(Book bookToCopy)
        {
            var properties = bookToCopy.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "Name" && prop.Name != "LibraryId")
                {
                    Console.WriteLine("Please enter {0}", prop.Name);
                    var userValue = Console.ReadLine();
                    if (!string.IsNullOrEmpty(userValue))
                        prop.SetValue(bookToCopy, Convert.ChangeType(userValue, prop.PropertyType));
                }
            }

            return bookToCopy;
        }
    }
}