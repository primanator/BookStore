namespace UI
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Net;
    using System.Text;
    using DTO.Entities;

    internal class InputHandler
    {
        private string _baseUriString;
        private StringBuilder _commandSaver;

        public InputHandler()
        {
            _baseUriString = ConfigurationManager.AppSettings["Uri"];
            _commandSaver = new StringBuilder();
        }

        public void Process()
        {
            Console.WriteLine("Enter one of the following commands\nstatistics/manipulate:");
            var command = Console.ReadLine().ToLowerInvariant();
            _commandSaver.AppendLine(command);
            switch (command)
            {
                case "manipulate":
                    {
                        Console.Clear();
                        Console.WriteLine($"{_commandSaver.ToString()}\ncreate/read/update/delete:");
                        command = Console.ReadLine().ToLowerInvariant();
                        _commandSaver.AppendLine(command);
                        switch (command)
                        {
                            case "create":
                                {
                                    Console.Clear();
                                    Console.WriteLine($"{_commandSaver.ToString()}\n1/n:");
                                    command = Console.ReadLine().ToLowerInvariant();
                                    _commandSaver.AppendLine(command);

                                    switch (command)
                                    {
                                        case "n":
                                            {
                                                Console.Clear();
                                                Console.WriteLine($"{ _commandSaver.ToString()}\nEnter path to the .xlsx file you want to import: ");
                                                var path = Console.ReadLine();

                                                try
                                                {
                                                    if (new FileInfo(path).Extension != ".xlsx")
                                                        throw new ArgumentException("Input file is not in .xlsx format.");

                                                    var requestStatus = new Post(_baseUriString + "/import", null).Send(File.ReadAllBytes(path));
                                                    Console.WriteLine($"Operation status is {requestStatus}");
                                                }
                                                catch (Exception e)
                                                {
                                                    Console.WriteLine(e.Message);
                                                    break;
                                                }
                                                break;
                                            }
                                    }
                                    break;
                                }
                        }
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