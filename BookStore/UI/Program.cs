﻿namespace UI
{
    using System;
    using UI.Factories.ContentProviders;
    using UI.Factory.Requests;
    using UI.Factory.Serializers;

    class Program
    {
        static void Main(string[] args)
        {
            var serializerFactory = new ContentSerializerFactory();
            var contentExtractorFactory = new ContentProviderFactory();
            var webRequestFactory = new WebRequestFactory(serializerFactory, contentExtractorFactory);
            var requestDesigner = new RequestDesigner(webRequestFactory);
            while (true)
            {
                requestDesigner.SetUp();

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}