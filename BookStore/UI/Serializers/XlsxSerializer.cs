namespace UI.Serializers
{
    using System;
    using System.IO;
    using UI.Serializers.Interfaces;

    internal class XlsxSerializer<Ts, Td> : IGenericContentSerializer<Ts, Td>
        where Ts : class
        where Td : Ts
    {
        public Ts GetContent()
        {
            Console.WriteLine("\nEnter path to the .xlsx file you want to import: ");
            return Console.ReadLine() as Ts;
        }

        public byte[] ToBytes(Ts target)
        {
            var targetStr = target as string ?? throw new ArgumentOutOfRangeException("Path to the imported file should be passed as a string.");

            CheckTarget(targetStr);

            return File.ReadAllBytes(targetStr);
        }

        public Td FromBytes(byte[] data)
        {
            File.WriteAllBytes("returnedDoc.xlsx", data);
            return (Td)(object)"File succesfully imported";
        }

        private void CheckTarget(string packagePath)
        {
            if (new FileInfo(packagePath).Extension != ".xlsx")
                throw new ArgumentException("Input file is not in .xlsx format.");
        }
    }
}