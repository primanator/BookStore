namespace UI
{
    using System.Collections.Generic;
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
            Generic("Enter one of the following commands\nstatistics/manipulate:", new Dictionary<string, Action>
                {
                    {
                        "manipulate", () =>
                        {
                            Generic($"{_commandSaver.ToString()}\ncreate/read/update/delete:", new Dictionary<string, Action>
                            {
                                {
                                    "create", () =>
                                    {
                                        Generic($"{_commandSaver.ToString()}\n1/N:", new Dictionary<string, Action>
                                        {
                                            {
                                                "1", () => {  }
                                            },
                                            {
                                                "N", () => { new PostRequest(new XlsxSerializer(), null, "/import").Send(); }
                                            }
                                        });
                                    }
                                },
                                {
                                    "read", () =>
                                    {

                                    }
                                },
                                {
                                    "update", () =>
                                    {

                                    }
                                },
                                {
                                    "delete", () =>
                                    {

                                    }
                                }
                            });
                        }
                    },
                });
        }

        private void Generic(string text, Dictionary<string, Action> actions)
        {
            Console.Clear();
            Console.WriteLine(text);
            var command = Console.ReadLine().ToLowerInvariant();
            _commandSaver.AppendLine(command);

            if (actions.TryGetValue(command, out Action act))
            {
                act();
            }
            else
            {
                Console.WriteLine("Client does not support such method.");
            }
        }

        //try
        //{
        //    var requestStatus = new PostRequest(new XlsxSerializer(), null, "/import").Send();
        //    Console.WriteLine($"Operation status is {requestStatus}");
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e.Message);
        //    return;
        //}
    }
}