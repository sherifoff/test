using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;


namespace RealEstate.Models
{
    public class EstateItem
    {
        [Key]        
        public int Id { get; set; }        
        public int Price { get; set; }             
        public string Image { get; set; }        
        public string Location { get; set; }
       
        [ForeignKey("Currency")]
        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }
        public Property Property { get; set; }

        [ForeignKey("HouseType")]
        public int HouseTypeId { get; set; }
        public HouseType HouseType { get; set; }

        public string Details { get; set; }
    }
}
