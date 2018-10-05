using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface IEstateItemRepository
    {
        IQueryable<EstateItem> EstateItems { get; }

        void AddEstateItem(EstateItem estateItem);

        void SaveEstateItem(EstateItem estateItem);

        EstateItem DeleteEstateItem(int Id);
    }
}
