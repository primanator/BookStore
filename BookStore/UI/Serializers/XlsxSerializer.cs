namespace UI.Serializers
{
    using System;
    using System.IO;
    using UI.Serializers.Interfaces;

    internal class XlsxSerializer : IContentSerializer
    {
        private readonly string _package;

        public XlsxSerializer()
        {
            Console.WriteLine("\nEnter path to the .xlsx file you want to import: ");
            _package = Console.ReadLine();
        }

        public void ReadBytes(byte[] data)
        {
            File.WriteAllBytes("returnedDoc.xlsx", data);
        }

        public byte[] ToBytes()
        {
            CheckPackage();
            return File.ReadAllBytes(_package);
        }

        private void CheckPackage()
        {
            if (new FileInfo(_package).Extension != ".xlsx")
                throw new ArgumentException("Input file is not in .xlsx format.");
        }
    }
}