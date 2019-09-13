namespace UI.Serializers.Interfaces
{
    public interface IGenericContentSerializer<Ts, Td>
    {
        Ts GetContent();

        byte[] ToBytes(Ts target);

        Td FromBytes(byte[] data);
    }
}