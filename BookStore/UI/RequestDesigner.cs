using System.Collections.Generic;

namespace UI
{
    using System;
    using System.Text;
    using UI.Requests;
    using UI.Serializers;

    internal class RequestDesigner
    {
        private readonly StringBuilder _commandSaver;

        public RequestDesigner()
        {
            _commandSaver = new StringBuilder();
        }

        public void SetUp()
        {
            Generic("Enter one of the following commands\nstatistics/manipulate:", 
                new Dictionary<string, Action>
                {
                    {
                    "n", () =>
                        {
                            Generic($"{_commandSaver.ToString()}\ncreate/read/update/delete:",
                                new Dictionary<string, Action>
                                {
                                    {
                                        () => { Generic($"{_commandSaver.ToString()}\n1/n:", N); });
                                    }
                                }
                        }
                    }
    }
            );

            //"n", () =>
            //{
            //    Generic($"{_commandSaver.ToString()}\ncreate/read/update/delete:",
            //        new Dictionary<string, Action>
            //        {
            //            {
            //                () => { Generic($"{_commandSaver.ToString()}\n1/n:", N);
            //                });
            //            }
            //        }
            //}
        }

        private void Manipulate()
        {
            string command;
            Console.Clear();
            Console.WriteLine($"{_commandSaver.ToString()}\ncreate/read/update/delete:");
            command = Console.ReadLine().ToLowerInvariant();
            _commandSaver.AppendLine(command);

            switch (command)
            {
                case "create":
                    {
                        Create();

                        break;
                    }
            }
        }

        private void Create()
        {
            string command;
            Console.Clear();
            Console.WriteLine($"{_commandSaver.ToString()}\n1/n:");
            command = Console.ReadLine().ToLowerInvariant();
            _commandSaver.AppendLine(command);

            switch (command)
            {
                case "n":
                    {
                        N();
                        break;
                    }
            }
        }


        private void Generic(string text, Dictionary<string, Action> actions)
        {
            Console.Clear();
            Console.WriteLine(text);
            var command = Console.ReadLine().ToLowerInvariant();
            _commandSaver.AppendLine(command);

            if (actions.TryGetValue(command, out Action a))
            {
                a();
            }
            else
            {
                Console.WriteLine("Client does not support such method.");
            }
        }

        private static void N()
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
                return;
            }
        }
    }
}