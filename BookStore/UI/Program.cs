namespace UI
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var inputHandler = new InputHandler();
            while (true)
            {
                inputHandler.Process();

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}