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
                { "manipulate", () => {
                    Generic($"{_commandSaver.ToString()}\ncreate/read/update/delete:", new Dictionary<string, Action>
                    {
                        { "create", () => { SetUpSingleAndMultipleRequestAction(
                            null,
                            () => new PostRequest(new XlsxSerializer(), null, "/import").Send()); }},
                        { "read", () => { SetUpSingleAndMultipleRequestAction(
                            () => new GetRequest(null, "?name=" + Console.ReadLine()).Send(),
                            () => new GetRequest(null, string.Empty).Send()); }},
                        { "update", () => { } },
                        { "delete", () => { } }
                    });
                }},
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

        private void SetUpSingleAndMultipleRequestAction(Action requestSingleAction, Action requestMultipleAction)
        {
            var actionDictionary = new Dictionary<string, Action>();

            if (requestMultipleAction != null)
            {
                actionDictionary.Add("1", requestSingleAction);
            }
            if (requestMultipleAction != null)
            {
                actionDictionary.Add("N", requestMultipleAction);
            }

            Generic($"{_commandSaver.ToString()}\n1/N:", actionDictionary);
        }
    }
}