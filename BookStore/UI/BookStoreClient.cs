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

        static async Task GetRequest(string ID)
        {
            switch (ID.ToLowerInvariant())
            {
                case "get":
                    Console.WriteLine("Enter name (leave empty to choose all):");
                    string name = Console.ReadLine();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        //empty name means select all records    
                        if (string.IsNullOrEmpty(name))
                        {
                            response = await client.GetAsync("api/books");
                            if (response.IsSuccessStatusCode)
                            {
                                Book[] books = await response.Content.ReadAsAsync<Book[]>();
                                if (books != null)
                                    foreach (var report in books)
                                        Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", report.Id, report.Name, report.Isbn, report.Pages, report.LimitedEdition);
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
                                    Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}", book.Id, book.Name, book.Isbn, book.Pages, book.LimitedEdition);
                                else
                                    Console.WriteLine("There is no such book in the library.");
                            }
                        }
                    }
                    break;

                case "post":
                    /*BookStoreClient newReport = new BookStoreClient();
                    Console.WriteLine("Enter data:");
                    Console.WriteLine("Enter Id:");
                    newReport.Id = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter Name:");
                    newReport.Name = Console.ReadLine();
                    Console.WriteLine("Enter Isbn:");
                    newReport.Isbn = Console.ReadLine();
                    Console.WriteLine("Enter Pages:");
                    newReport.Pages = Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Enter LimitedEdition:");
                    newReport.LimitedEdition = Boolean.Parse(Console.ReadLine());*/

                    Book newReport = new Book()
                    {
                        Id = 3,
                        Name = "Miguel de Cervantes",
                        Isbn = "9785699687275",
                        LimitedEdition = false,
                        Pages = 154,
                        WrittenIn = new DateTime(1615, 1, 1),
                        LibraryId = 1
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        ShowResponceAsync(await client.PostAsJsonAsync("api/books", newReport));
                    }
                    break;

                case "put":
                    Book update = new Book()
                    {
                        Id = 1,
                        Name = "The Young Man and The Land.",
                        Isbn = "6666666666",
                        Pages = 666,
                        LimitedEdition = true,
                        WrittenIn = new DateTime(1666, 1, 1)
                    };

                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        ShowResponceAsync(await client.PutAsJsonAsync("api/books", update));
                    }
                    break;

                case "delete":
                    Console.WriteLine("Enter name:");
                    string toDelete = Console.ReadLine();
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:50402/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json"));

                        ShowResponceAsync(await client.DeleteAsync("api/books?=" + toDelete));
                    }
                    break;
            }

        }

        static async void ShowResponceAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
                Console.WriteLine("Operation was succesfull.");
            else
                Console.WriteLine(content);
        }
    }

    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public int Pages { get; set; }
        public bool LimitedEdition { get; set; }
        public DateTime WrittenIn { get; set; }
        public int LibraryId { get; set; }
    }
}