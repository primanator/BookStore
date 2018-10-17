namespace DependencyResolver
{
    using AutoMapper;
    using DTO.Entities;
    using DTO_EF.Entities;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BookDto, Book>();
        }
    }
}