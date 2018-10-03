using System;

namespace DAL.Utils
{
    using DTO.Entities;
    using DTO_EF.Entities;
    using AutoMapper;

    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<BookDto, Book>();
        }
    }
}
