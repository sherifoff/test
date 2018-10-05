using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface IPropertyRepository
    {
        IQueryable<Property> Property { get; }

        void AddProperty(Property properties);

        void SaveProperty(Property properties);
    }
}
