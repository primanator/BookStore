namespace UI.Factory.Serializers
{
    using UI.Serializers;
    using UI.Serializers.Interfaces;

    internal class ContentSerializerFactory : ISerializerFactory
    {
        public IGenericContentSerializer<T> GetEntitySerializer<T>()
        {
            return new DtoSerializer<T>();
        }

        public IGenericContentSerializer<T> GetXlsxSerializer<T>()
        {
            return new XlsxSerializer<T>();
        }
    }
}
