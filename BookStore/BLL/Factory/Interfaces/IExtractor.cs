namespace BLL.Factory.Interfaces
{
    using BLL.Models;
    using System;

    internal delegate void ExtractHandler<TEventArgs>(object sender, ExtractionEventArgs args)
        where TEventArgs : EventArgs;

    internal interface IExtractor
    {
        event ExtractHandler<ExtractionEventArgs> ExtractionPassed;

        ExtractionEventArgs Extract(object sender, ValidationEventArgs e);
    }
}