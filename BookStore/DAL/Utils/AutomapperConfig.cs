namespace DAL.Utils
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using AutoMapper;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<AuthorDto, Author>();
            CreateMap<BookDto, Book>();
            CreateMap<CountryDto, Country>();
            CreateMap<EntityDto, Entity>();
            CreateMap<GenreDto, Genre>();
            CreateMap<LibraryDto, Library>();
            CreateMap<LiteratureFormDto, LiteratureForm>();
            CreateMap<UserDto, User>();
        }
    }
}
