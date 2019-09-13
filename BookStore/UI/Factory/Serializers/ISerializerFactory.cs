namespace UI.Factory.Serializers
{
    using DTO.Entities;
    using UI.Serializers.Interfaces;

    public interface ISerializerFactory
    {
        IGenericContentSerializer<Ts, Td> GetEntitySerializer<Ts, Td>()
            where Ts : Dto, new()
            where Td : Ts;

        IGenericContentSerializer<string, string> GetXlsxSerializer();
    }
}