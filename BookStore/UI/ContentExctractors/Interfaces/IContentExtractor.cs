namespace UI.ContentExtractors.Interfaces
{
    public interface IContentExtractor<T>
    {
        string GetContentName();

        T GetFullContent();
    }
}