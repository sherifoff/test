using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstate.Models
{
       
    public class EstateItemRepository : IEstateItemRepository
    {
        
        private ApplicationDbContext context;


        public EstateItemRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<EstateItem> EstateItems => context.EstateItems;

        public void AddEstateItem(EstateItem estateItem)
        {
            context.EstateItems.Add(estateItem);
            
            context.SaveChanges();
        }


        public void SaveEstateItem(EstateItem estateItem)
        {
            if (estateItem.Id == 0)
            {
                context.EstateItems.Add(estateItem);
            }
            else
            {
                EstateItem dbEntry = context.EstateItems
                .FirstOrDefault(e => e.Id == estateItem.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = estateItem.Id;
                    dbEntry.Currency = estateItem.Currency;
                    dbEntry.CurrencyId = estateItem.CurrencyId;
                    dbEntry.Image = estateItem.Image;
                    dbEntry.Location = estateItem.Location;
                    dbEntry.Price = estateItem.Price;
                    dbEntry.Property = estateItem.Property;
                    dbEntry.PropertyId = estateItem.PropertyId;
                    dbEntry.Details = estateItem.Details;
                    dbEntry.HouseType = estateItem.HouseType;
                    dbEntry.HouseTypeId = estateItem.HouseTypeId;
                }
            }
            context.SaveChanges();
        }

        public EstateItem DeleteEstateItem(int Id)
        {
            EstateItem dbEntry = context.EstateItems
            .FirstOrDefault(e => e.Id == Id);
            if (dbEntry != null)
            {
                context.EstateItems.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

    }
     
}


        
