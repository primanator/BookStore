﻿namespace UI
{
    using System.Collections.Generic;
    using System;
    using System.Text;
    using UI.Factory.Requests;

    internal class RequestDesigner
    {
        private readonly IRequestFactory _requestFactory;
        private readonly StringBuilder _commandSaver;

        public RequestDesigner(IRequestFactory requestFactory)
        {
            _requestFactory = requestFactory;
            _commandSaver = new StringBuilder();
        }

        public void SetUp()
        {
            Generic("Enter one of the following commands\nstatistics/manipulate:", new Dictionary<string, Action>
            {
                { "manipulate", () => {
                    Generic($"{_commandSaver.ToString()}\ncreate/read/update/delete:", new Dictionary<string, Action>
                    {
                        { "create", () => { SetUpSingleAndMultipleRequestActions(
                            () => _requestFactory.PostRequest().Send(),
                            () => _requestFactory.PostMultipleRequest().Send());
                        }},
                        { "read", () => { SetUpSingleAndMultipleRequestActions(
                            () => _requestFactory.GetRequest().Send(),
                            () => _requestFactory.GetAllRequest().Send());
                        }},
                        { "update", () => { SetUpSingleAndMultipleRequestActions(
                            () => _requestFactory.PutRequest().Send(),
                            null);
                        }},
                        { "delete", () => { SetUpSingleAndMultipleRequestActions(
                            () => _requestFactory.DeleteRequest().Send(),
                            null);
                        }}
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

        private void SetUpSingleAndMultipleRequestActions(Action requestSingleAction, Action requestMultipleAction)
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