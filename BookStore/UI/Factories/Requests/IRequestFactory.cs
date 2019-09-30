namespace UI.Factory.Requests
{
    using Contracts.Models;
    using UI.Requests.Interfaces;

    public interface IRequestFactory
    {
        IRequest GetRequest<T>() where T : BaseContract, new();

        IRequest PostRequest<T>() where T : BaseContract, new();

        IRequest PutRequest<T>() where T : BaseContract, new();

        IRequest DeleteRequest<T>() where T : BaseContract, new();

        IRequest PostWithXlsx<T>();
    }
}