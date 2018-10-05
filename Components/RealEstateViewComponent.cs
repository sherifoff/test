using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateComponents.Components
{
    public class RealEstateViewComponent : ViewComponent 
    {
        public IViewComponentResult Invoke(EstateItemViewModel estateItem)
        {            
            return View(estateItem);
        }
    }
}
