namespace UI.Serializers
{
    using Interfaces;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class ContractSerializer<T> : IGenericContentSerializer<T>
    {
        public byte[] ToBytes(T target)
        {
            if (target == null)
                return null;

            using (var ms = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(ms, target);
                return ms.ToArray();
            }
        }

        public T FromBytes(byte[] data)
        {
            if (data == null)
                return default(T);

            using (var memoryStream = new MemoryStream(data))
            {
                var binaryFormatter = new BinaryFormatter();
                var deserializedObj = binaryFormatter.Deserialize(memoryStream);
                return (T)deserializedObj;
            }
        }
    }
}
