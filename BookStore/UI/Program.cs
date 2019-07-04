namespace UI
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var requestDesigner = new RequestDesigner();
            while (true)
            {
                requestDesigner.SetUp();

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}