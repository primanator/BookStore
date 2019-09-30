namespace DependencyResolver
{
    using AutoMapper;
    using Contracts.Models;
    using DTO.Models;

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<Book, BookDto>();
        }
    }
}