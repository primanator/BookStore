namespace BLL.Utils
{
    using AutoMapper;
    using BLL.DTO;
    using DAL.Entities;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Book, BookDto>();
        }
    }
}