namespace UI.ContentProviders.Interfaces
{
    using System.Net;

    public interface IContentProvider<T>
    {
        T GetContent();

        WebHeaderCollection GetHeaders();

        string GetRequiredItemName();

        string GetContentPluralName();
    }
}