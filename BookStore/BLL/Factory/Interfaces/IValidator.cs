namespace BLL.Factory.Interfaces
{
    using System.IO;

    public interface IValidator
    {

        void Validate(Stream source);
    }
}