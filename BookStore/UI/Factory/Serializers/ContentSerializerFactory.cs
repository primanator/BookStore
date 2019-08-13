namespace UI.Factory.Serializers
{
    using UI.Serializers.Interfaces;

    internal class ContentSerializerFactory : ISerializerFactory
    {
        public IContentSerializer GetEntitySerializer()
        {
            throw new System.NotImplementedException();
        }

        public IContentSerializer GetXlsxSerializer()
        {
            throw new System.NotImplementedException();
        }
    }
}
