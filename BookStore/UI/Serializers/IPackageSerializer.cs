namespace UI.Serializers
{
    public interface IPackageSerializer
    {
        byte[] GetBytes();

        void SaveBytes(byte[] data);
    }
}