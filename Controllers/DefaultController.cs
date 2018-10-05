using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;

namespace RealEstate.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            EstateItemViewModel estateItem = new EstateItemViewModel
            {
                Image = "/images/12.jpg"
            };
            
            return View(estateItem);
        }
    }
}