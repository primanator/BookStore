using AutoMapper;
using DAL.Interfaces;

namespace DAL.Utils
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using AutoMapper;
    using DAL.Interfaces;
    using DAL.Implementation;
    using DAL.EF;

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
            CreateMap<IGenericRepository<BookDto>, IGenericRepository<Book>>()
                .ConstructUsing(context => 
                {
                    context.FindBy(book => true);
                    return new GenericRepository<Book>(new BookStoreContext());
                });

    });
        }
    }

    public static class MapperHelper
    {
        public static IGenericRepository<TDestination> ReMapRepository<TSource, TDestination>(this IMapper mapper, IGenericRepository<TSource> source)
            where TSource : class
            where TDestination : class
        {
            return mapper.Map<IGenericRepository<TDestination>>(source);
        }
    }
}
