namespace BLL.Factory.Interfaces
{
    using System.Collections.Generic;
    using System.IO;
    using DTO.Entities;

    public interface IExtractor
    {
        List<Dto> Extract(Stream source, object sourceMap);
    }
}