using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class HouseTypeRepository : IHouseTypeRepository
    {
        private ApplicationDbContext context;


        public HouseTypeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<HouseType> HouseTypes => context.HouseTypes;

        public void AddHouseType(HouseType houseType)
        {
            context.HouseTypes.Add(houseType);

            context.SaveChanges();
        }

        public void SaveHouseType(HouseType houseType)
        {
            if (houseType.Id == 0)
            {
                context.HouseTypes.Add(houseType);
            }
            else
            {
                HouseType dbEntry = context.HouseTypes
                .FirstOrDefault(e => e.Id == houseType.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = houseType.Id;
                    dbEntry.Name = houseType.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
