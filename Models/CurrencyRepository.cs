using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private ApplicationDbContext context;


        public CurrencyRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Currency> Currency => context.Currency;

        public void AddCurrency(Currency currencies)
        {
            context.Currency.Add(currencies);

            context.SaveChanges();
        }

        public void SaveCurrency(Currency currencies)
        {
            if (currencies.Id == 0)
            {
                context.Currency.Add(currencies);
            }
            else
            {
                Currency dbEntry = context.Currency
                .FirstOrDefault(e => e.Id == currencies.Id);
                if (dbEntry != null)
                {
                    dbEntry.Id = currencies.Id;
                    dbEntry.Name = currencies.Name;                    
                }
            }
            context.SaveChanges();
        }
    }
}
