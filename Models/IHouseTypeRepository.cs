using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface IHouseTypeRepository
    {
        IQueryable<HouseType> HouseTypes { get; }

        void AddHouseType(HouseType houseType);

        void SaveHouseType(HouseType houseType);
    }
}
