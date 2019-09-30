namespace UI.Factory.Serializers
{
    using UI.Serializers.Interfaces;

    public interface ISerializerFactory
    {
        IGenericContentSerializer<T> GetContractSerializer<T>();

        IGenericContentSerializer<T> GetXlsxSerializer<T>();
    }
}