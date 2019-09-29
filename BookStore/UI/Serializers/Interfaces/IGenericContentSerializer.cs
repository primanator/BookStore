namespace UI.Serializers.Interfaces
{
    public interface IGenericContentSerializer<T>
    {
        byte[] ToBytes(T target);

        T FromBytes(byte[] data);
    }
}