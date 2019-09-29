namespace UI.Factory.Serializers
{
    using UI.Serializers.Interfaces;

    public interface ISerializerFactory
    {
        IGenericContentSerializer<T> GetEntitySerializer<T>();

        IGenericContentSerializer<T> GetXlsxSerializer<T>();
    }
}