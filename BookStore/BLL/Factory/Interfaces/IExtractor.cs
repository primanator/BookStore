namespace BLL.Factory.Interfaces
{
    using System.Collections.Generic;
    using System.Web;
    using DTO.Entities;

    public interface IExtractor
    {
        List<Dto> Extract(HttpPostedFile source, object sourceMap);
    }
}