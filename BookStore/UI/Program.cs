namespace UI
{
    using System;
    using UI.Factory.Requests;
    using UI.Factory.Serializers;

    class Program
    {
        static void Main(string[] args)
        {
            var serializerFactory = new ContentSerializerFactory();
            var webRequestFactory = new WebRequestFactory(serializerFactory);
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