namespace UI.Interfaces
{
    public interface IPackageSerializer
    {
        byte[] GetBytes();

        void SaveBytes(byte[] data);
    }
}