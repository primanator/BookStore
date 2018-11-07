namespace BLL.Interfaces
{
    using DTO.Entities;
    using System.Collections.Generic;
    using System.Web;

    public interface IDtoExtractor<T>
        where T: Dto
    {
        List<T> Extract(HttpPostedFile source);
    }
}