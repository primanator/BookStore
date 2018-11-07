namespace BLL.Factory.Interfaces
{
    using System;

    public interface IExtractor
    {
        event EventHandler<EventArgs> ImportExtracted;

        void Extract(object sender, EventArgs e);
    }
}