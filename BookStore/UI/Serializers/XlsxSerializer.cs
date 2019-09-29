namespace UI.Serializers
{
    using System;
    using System.IO;
    using UI.Serializers.Interfaces;

    internal class XlsxSerializer<T> : IGenericContentSerializer<T>
    {
        public byte[] ToBytes(T target)
        {
            var targetStr = target as string ?? throw new ArgumentOutOfRangeException("Path to the imported file should be passed as a string.");

            CheckTarget(targetStr);

            return File.ReadAllBytes(targetStr);
        }

        public T FromBytes(byte[] data)
        {
            File.WriteAllBytes("returnedDoc.xlsx", data);
            return (T)(object)"File succesfully imported";
        }

        private void CheckTarget(string packagePath)
        {
            if (new FileInfo(packagePath).Extension != ".xlsx")
                throw new ArgumentException("Input file is not in .xlsx format.");
        }
    }
}