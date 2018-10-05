using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public interface ICurrencyRepository
    {
        IQueryable<Currency> Currency { get; }

        void AddCurrency(Currency currencies);

        void SaveCurrency(Currency currencies);
    }
}
