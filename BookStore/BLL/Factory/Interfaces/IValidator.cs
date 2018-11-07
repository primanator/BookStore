namespace BLL.Factory.Interfaces
{
    using System;
    using System.IO;

    public interface IValidator
    {
        event EventHandler<EventArgs> ImportValidated;

        void Validate(Stream source);
    }
}