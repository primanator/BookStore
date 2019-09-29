namespace UI.Factory.Requests
{
    using DTO.Entities;
    using UI.Requests.Interfaces;

    public interface IRequestFactory
    {
        IRequest GetRequest<T>() where T : Dto, new();

        IRequest PostRequest<T>() where T : Dto, new();

        IRequest PutRequest<T>() where T : Dto, new();

        IRequest DeleteRequest<T>() where T : Dto, new();

        IRequest PostWithXlsx<T>();
    }
}