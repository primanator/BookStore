namespace BLL.Factory.Interfaces
{
    using System.Collections.Generic;
    using DTO.Entities;

    public interface IImporter
    {
        void Import(List<Dto> importData);
    }
}