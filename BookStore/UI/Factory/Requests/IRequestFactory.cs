namespace UI.Factory.Requests
{
    using UI.Requests.Interfaces;

    public interface IRequestFactory
    {
        IRequest GetRequest();

        IRequest GetAllRequest();

        IRequest PostRequest();

        IRequest PostMultipleRequest();

        IRequest DeleteRequest();

        IRequest PutRequest();
    }
}
