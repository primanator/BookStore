namespace UI
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using DTO.Entities;
    using UI.Implementation;
    using UI.Interfaces;


    internal class Client
    {
        private RequestSender<Book> _requestSender;
        private bool stopInput;
        public async Task ProcessInput()
        {
            Console.WriteLine("Enter command:\n1. Get\n2. Post\n2. Input\n2. Delete\n2. Import");

            while(!stopInput)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            _requestSender = new GetRequestSender<Book>();
                            break;
                        }
                    //case "2":
                    //    {
                    //        _requestSender = new PostRequestSender<Book>();
                    //        await _requestSender.SendAsync();
                    //        break;
                    //    }
                    //case "import":
                    //    {
                    //        _requestSender = new GetRequestSender<Book>();
                    //        await _requestSender.SendAsync();
                    //        Console.WriteLine("Enter path to the .xlsx file you want to import: ");
                    //        var path = Console.ReadLine();

                    //        FileInfo toImport = null;
                    //        try
                    //        {
                    //            toImport = new FileInfo(path);
                    //            if (toImport.Extension != "xlsx")
                    //                throw new ArgumentException("Input file is not in .xlsx format.");
                    //        }
                    //        catch (Exception e)
                    //        {
                    //            Console.WriteLine(e.Message);
                    //            break;
                    //        }

                    //        var result = await PostDocument(toImport);
                    //        Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                    //        break;
                    //    }
                    //case "put":
                    //    {
                    //        Console.WriteLine("Enter title of the book you want to update: ");
                    //        var title = Console.ReadLine();

                    //        var bookToUpdate = await GetBookByTitleAsync(title);
                    //        if (bookToUpdate != null)
                    //        {
                    //            CopyBookWithUserUpdates(bookToUpdate);
                    //            var result = await PutUpdateBookAsync(bookToUpdate);
                    //            Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                    //        }
                    //        else
                    //            Console.WriteLine("There is no such book in the library.");

                    //        break;
                    //    }
                    //case "delete":
                    //    {
                    //        Console.WriteLine("Enter name:");
                    //        string toDelete = Console.ReadLine();

                    //        var result = await DeleteBookAsync(toDelete);
                    //        Console.WriteLine("Operation result is: " + (string.IsNullOrEmpty(result) ? "succesfull" : result));
                    //        break;
                    //    }
                    default:
                        {
                            //Console.WriteLine("Client does not support such method.");
                            stopInput = true;
                            break;
                        }
                }

                var result = await _requestSender.SendAsync();
                foreach (var book in result)
                {
                    Console.WriteLine("\nId:{0}\tName:{1}\tIsbn:{2}\tPages:{3}\tLimitedEdition:{4}", book.Id, book.Name,
                        book.Isbn, book.Pages, book.LimitedEdition);
                }
                //result.ToString(); // TO DO override string
            }

            Console.ReadKey();
            Console.Clear();

            _requestSender.Dispose();
        }

        public static Book GetBookFromUserInput()
        {
            Book newBook = new Book();

            var properties = newBook.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (prop.Name != "Id" && prop.Name != "LibraryId")
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

        public static Book CopyBookWithUserUpdates(Book bookToCopy)
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

        //private async Task<string> PostNewBookAsync(Book newBook)
        //{
        //    _response = await _client.PostAsJsonAsync("api/books", newBook);
        //    CheckResponse();

        //    return await _response.Content.ReadAsStringAsync();
        //}

        //private async Task<string> PutUpdateBookAsync(Book newBook)
        //{
        //    _response = await _client.PutAsJsonAsync("api/books", newBook);
        //    CheckResponse();

        //    return await _response.Content.ReadAsStringAsync();
        //}

        //private async Task<string> DeleteBookAsync(string title)
        //{
        //    _response = await _client.DeleteAsync("api/books?name=" + title);
        //    CheckResponse();

        //    return await _response.Content.ReadAsStringAsync();
        //}

        //private async Task<string> PostDocument(FileInfo file)
        //{
        //    HttpContent fileStreamContent = null;
        //    try
        //    {
        //        fileStreamContent = new StreamContent(file.OpenRead());
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        return "runtime error occured";
        //    }

        //    using (var formData = new MultipartFormDataContent())
        //    {
        //        formData.Add(fileStreamContent, "importDocument", file.Name);
        //        _response = await _client.PostAsync("api/documents", formData);
        //    }
        //    CheckResponse();

        //    return await _response.Content.ReadAsStringAsync();
        //}
    }
}