namespace BLL.Factory.Interfaces
{
    using BLL.Models;

    internal interface IImporter
    {
        void Import(object sender, ExtractionEventArgs args);
    }
}