using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RealEstate.Models
{
    public class Currency
    {
        //[Key]
        public int Id   { get; set; }
        
        public string Name { get; set; }        
    }
}
