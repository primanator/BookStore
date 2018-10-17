namespace DependencyResolver
{
    using AutoMapper;
    using DTO.Entities;
    using DTO_EF.Entities;

    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<BookDto, Book>();
        }
    }
}