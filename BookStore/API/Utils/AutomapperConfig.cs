namespace API.Utils
{
    using API.Models;
    using AutoMapper;
    using BLL.DTO;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BookModel, BookDto>();
        }
    }
}