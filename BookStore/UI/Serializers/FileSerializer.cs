namespace UI.Serializers
{
    using System;
    using System.IO;

    internal class XlsxSerializer : IPackageSerializer
    {
        private readonly string _package;

        public XlsxSerializer()
        {
            Console.WriteLine("\nEnter path to the .xlsx file you want to import: ");
            _package = Console.ReadLine();
        }

        public byte[] GetBytes()
        {
            CheckPackage();
            return File.ReadAllBytes(_package);
        }

        public void SaveBytes(byte[] data)
        {
            File.WriteAllBytes("returnedDoc.xlsx", data);
        }

        private void CheckPackage()
        {
            if (new FileInfo(_package).Extension != ".xlsx")
                throw new ArgumentException("Input file is not in .xlsx format.");
        }
    }
}