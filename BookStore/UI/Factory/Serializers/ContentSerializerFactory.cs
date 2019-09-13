namespace UI.Factory.Serializers
{
    using DTO.Entities;
    using UI.Serializers;
    using UI.Serializers.Interfaces;

    internal class ContentSerializerFactory : ISerializerFactory
    {
        public  IGenericContentSerializer<Ts, Td> GetEntitySerializer<Ts, Td>()
            where Ts : Dto, new()
            where Td : Ts
        {
            return new DtoSerializer<Ts, Td>();
        }

        public IGenericContentSerializer<string, string> GetXlsxSerializer()
        {
            return new XlsxSerializer<string, string>();
        }
    }
}
