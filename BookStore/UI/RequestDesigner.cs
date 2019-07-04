namespace UI
{
    using System;
    using System.Text;
    using UI.Requests;
    using UI.Serializers;

    internal class RequestDesigner
    {
        private StringBuilder _commandSaver;

        public RequestDesigner()
        {
            _commandSaver = new StringBuilder();
        }

        public void SetUp()
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
                                                
                                                try
                                                {
                                                    var requestStatus = new PostRequest(new XlsxSerializer(), null, "/import").Send();
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
    }
}