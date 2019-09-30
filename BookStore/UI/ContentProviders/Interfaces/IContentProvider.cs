namespace UI.ContentProviders.Interfaces
{
    using System.Net;

    public interface IContentProvider<T>
    {
        T GetContent();

        string GetName();

        WebHeaderCollection GetHeaders();
    }
}