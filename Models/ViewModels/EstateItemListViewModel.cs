using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.Models.ViewModels
{
    public class EstateItemListViewModel
    {
        public IEnumerable<EstateItemViewModel> EstateItems { get; set; }

        public PagingInfo PagingInfo { get; set; }        
    }
}
