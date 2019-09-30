namespace UI.Factory.Serializers
{
    using UI.Serializers;
    using UI.Serializers.Interfaces;

    internal class ContentSerializerFactory : ISerializerFactory
    {
        public IGenericContentSerializer<T> GetContractSerializer<T>()
        {
            return new ContractSerializer<T>();
        }

        public IGenericContentSerializer<T> GetXlsxSerializer<T>()
        {
            return new XlsxSerializer<T>();
        }
    }
}
