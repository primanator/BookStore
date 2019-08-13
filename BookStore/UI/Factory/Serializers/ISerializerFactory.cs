namespace UI.Factory.Serializers
{
    using UI.Serializers.Interfaces;

    public interface ISerializerFactory
    {
        IContentSerializer GetXlsxSerializer();

        IContentSerializer GetEntitySerializer();
    }
}