using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class PropertyRepository : IPropertyRepository
    {
        private ApplicationDbContext context;


        public PropertyRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Property> Property => context.Property;

        public void AddProperty(Property properties)
        {
            context.Property.Add(properties);

            context.SaveChanges();
        }

        public void SaveProperty(Property properties)
        {
            if (properties.Id == 0)
            {
                context.Property.Add(properties);
            }
            else
            {
                Property dbEntry = context.Property
                .FirstOrDefault(e => e.Id == properties.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = properties.Id;
                    dbEntry.Name = properties.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
