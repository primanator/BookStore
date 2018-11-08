namespace BLL.Factory.Interfaces
{
    using BLL.Models;
    using System;
    using System.IO;

    internal delegate ExtractionEventArgs SuccessfulValidationHandler<TEventArgs>(object sender, ValidationEventArgs args)
        where TEventArgs : EventArgs;

    internal interface IValidator
    {
        event SuccessfulValidationHandler<ValidationEventArgs> ValidatonPassed;

        void Validate(Stream source);
    }
}