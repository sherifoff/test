using Microsoft.AspNetCore.Http;
using RealEstate.Controllers;
using System.Collections.Generic;
using System.ComponentModel;
using RealEstate.Models.ViewModels;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace RealEstate.Models
{
    public class EstateItemViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Currency { get; set; }
        public string Property { get; set; }
        public string Image { get; set; }
        public string HouseType { get; set; }
        public string Location { get; set; }
        public IFormFile ImageUploaded { get; set; }
       
        public int CurrencyId { get; set; }
        public List<Currency> CurrencyList { get; set; }              
               
        public int PropertyId { get; set; }
        public List<Property> PropertyList { get; set; }
       
        public int HouseTypeId { get; set; }
        public List<HouseType> HouseTypeList { get; set; }

        public string Details { get; set; }

        public IEnumerable<EstateItem> EstateItems { get; set; }

        public PagingInfo PagingInfo { get; set; }        
    }
    
}
