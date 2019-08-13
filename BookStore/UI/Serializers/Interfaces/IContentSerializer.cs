namespace UI.Serializers.Interfaces
{
    public interface IContentSerializer
    {
        byte[] ToBytes();

        void ReadBytes(byte[] data);
    }
}