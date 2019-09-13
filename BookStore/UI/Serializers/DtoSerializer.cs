namespace UI.Serializers
{
    using DTO.Entities;
    using Interfaces;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    internal class DtoSerializer<Ts, Td> : IGenericContentSerializer<Ts, Td>
        where Ts : Dto, new()
        where Td : Ts
    {
        public Ts GetContent()
        {
            return Dto.GetFromUser<Ts>();
        }

        public byte[] ToBytes(Ts target)
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

        public Td FromBytes(byte[] data)
        {
            if (data == null)
                return default(Td);

            using (var memoryStream = new MemoryStream())
            {
                var binaryFormatter = new BinaryFormatter();
                var deserializedObj = binaryFormatter.Deserialize(memoryStream);
                return (Td)deserializedObj;
            }
        }
    }
}
