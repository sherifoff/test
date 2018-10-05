using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstate.Models;
using RealEstate.Models.ViewModels;


namespace RealEstate.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private IEstateItemRepository repository;
        private ICurrencyRepository currencyrepository;
        private IPropertyRepository propertyrepository;
        private IHouseTypeRepository houseTypeRepository;

        public int PageSize = 5;

        public HomeController(IEstateItemRepository repo,
            ICurrencyRepository currencyrepo,
            IPropertyRepository propertyrepo,
            IHouseTypeRepository housetyperepo)
        {
            repository = repo;
            currencyrepository = currencyrepo;
            propertyrepository = propertyrepo;
            houseTypeRepository = housetyperepo;
        }

        [AllowAnonymous]
        public ViewResult Index(int estateItemPage=1)
        {
            List<Currency> currency = currencyrepository.Currency.ToList();
            List<Property> propertyList = propertyrepository.Property.ToList();
            List<HouseType> houseTypeList = houseTypeRepository.HouseTypes.ToList();

            return
                 View(new EstateItemListViewModel
                 {
                     EstateItems = repository.EstateItems
                        .OrderBy(p => p.Id)
                        .Skip((estateItemPage - 1) * PageSize)
                        .Take(PageSize)
                        .Select(x =>
                             new EstateItemViewModel
                             {
                                 Id = x.Id,
                                 Currency = x.Currency.Name,
                                 HouseType = x.HouseType.Name,
                                 CurrencyList = currency,
                                 PropertyList = propertyList,
                                 HouseTypeList = houseTypeList,
                                 Image = x.Image,
                                 Location = x.Location,
                                 Price = x.Price,
                                 Property = x.Property.Name
                             }),
                     PagingInfo = new PagingInfo
                     {
                         CurrentPage = estateItemPage,
                         ItemsPerPage = PageSize,
                         TotalItems = repository.EstateItems.Count()
                     }
                 });
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Get(string path)
        {
            var image = System.IO.File.OpenRead(path);
            return File(image, "image/jpg");
        }



        public ViewResult Search(EstateItemViewModel model,
              string search,
              string currency,
              int price,
              string housetype,
              string property)
        {
            List<Currency> currencyList = currencyrepository.Currency.ToList();
            List<Property> propertyList = propertyrepository.Property.ToList();
            List<HouseType> houseTypeList = houseTypeRepository.HouseTypes.ToList();


            var query = new EstateItemListViewModel
            {
                EstateItems = repository.EstateItems
                             .Select(x =>
                                         new EstateItemViewModel
                                         {
                                             Id = x.Id,
                                             Currency = x.Currency.Name,
                                             HouseType = x.HouseType.Name,
                                             CurrencyList = currencyList,
                                             PropertyList = propertyList,
                                             HouseTypeList = houseTypeList,
                                             Image = x.Image,
                                             Location = x.Location,
                                             Price = x.Price,
                                             Property = x.Property.Name
                                         })
                                .Where(x =>
                                   x.Location == search
                                  || x.Price == price
                                || x.Location.StartsWith(search)
                                  || x.Location == model.Location
                                   || x.Price == model.Price
                                   && x.HouseType.Contains(housetype)
                                   && x.Property.Contains(property)
                                   && x.Currency.Contains(currency)
                             ).ToList()
            };

            var axtar = new EstateItemListViewModel
            {
                EstateItems = repository.EstateItems
                .Select(x =>
                            new EstateItemViewModel
                            {
                                Id = x.Id,
                                Currency = x.Currency.Name,
                                HouseType = x.HouseType.Name,
                                CurrencyList = currencyList,
                                PropertyList = propertyList,
                                HouseTypeList = houseTypeList,
                                Image = x.Image,
                                Location = x.Location,
                                Price = x.Price,
                                Property = x.Property.Name
                            })
                   .Where(x =>
                    x.Location == search
                    || x.Price == price
                    || x.Price == model.Price
                    || x.Location.StartsWith(search)
                    || x.Location == model.Location
                    && x.HouseType == housetype
                    && x.HouseType.StartsWith(housetype)
                    && x.Property == property
                    && x.Property.StartsWith(property)
                    && x.Currency == currency
                    && x.Currency.StartsWith(currency)
                    ).ToList()
            };

            if (search == "Location")
            {
                return View(query);
            }

            else
            {
                return View(axtar);
            }
        }


        public ViewResult Create()
        {
            /*  var estateViewModel = new EstateItemViewModel();
              estateViewModel.CurrencyList = currencyrepository.Currency.ToList();
              estateViewModel.PropertyList = propertyrepository.Property.ToList();
              estateViewModel.HouseTypeList = houseTypeRepository.HouseTypes.ToList();*/

            var estateViewModel = new EstateItemViewModel
            {
                CurrencyList = currencyrepository.Currency.ToList(),
                PropertyList = propertyrepository.Property.ToList(),
                HouseTypeList = houseTypeRepository.HouseTypes.ToList()
            };

            return View(estateViewModel);
        }

        [HttpPost]
        public IActionResult Create(EstateItemViewModel newEstateItem)
        {
            string imagePath = null;
            if (newEstateItem.ImageUploaded.Length > 0)
            {
                imagePath = $"E:\\images\\{Guid.NewGuid()}.jpg";
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    newEstateItem.ImageUploaded.CopyTo(stream);
                }
            }

            var estateItem = new EstateItem()
            {
                HouseTypeId = newEstateItem.HouseTypeId,
                Image = imagePath,
                Location = newEstateItem.Location,
                Price = newEstateItem.Price,
                CurrencyId = newEstateItem.CurrencyId,
                PropertyId = newEstateItem.PropertyId                
            };
            repository.AddEstateItem(estateItem);
            return RedirectToAction("Index");
        }


        
        public ViewResult List(int estateItemPage = 1)
          => View(new EstateItemListViewModel
          {
              EstateItems = repository.EstateItems
              .OrderBy(p => p.Id)
              .Skip((estateItemPage - 1) * PageSize)
              .Take(PageSize)
              .Select(p => new EstateItemViewModel
              {
                  Id = p.Id,
                  Currency = p.Currency.Name,
                  HouseType = p.HouseType.Name,
                  Image = p.Image,
                  Location = p.Location,
                  Price = p.Price,
                  Property = p.Property.Name                  
              }),
              PagingInfo = new PagingInfo
              {
                  CurrentPage = estateItemPage,
                  ItemsPerPage = PageSize,
                  TotalItems = repository.EstateItems.Count()
              }
          });


        public ViewResult Details(int ID)
        {
            List<Currency> currency = currencyrepository.Currency.ToList();
            List<Property> propertyList = propertyrepository.Property.ToList();
            List<HouseType> houseTypeList = houseTypeRepository.HouseTypes.ToList();

            return  View(repository.EstateItems
                  .Select(x =>
                             new EstateItemViewModel
                             {
                                 Id = x.Id,
                                 Currency = x.Currency.Name,
                                 HouseType = x.HouseType.Name,                                
                                 Image = x.Image,
                                 Location = x.Location,
                                 Price = x.Price,
                                 Property = x.Property.Name,
                                 Details = x.Details
                             })
                .FirstOrDefault(e => e.Id == ID));
        }

        //public ViewResult Edit() => View("Create", new EstateItem());

        public ViewResult Edit(int ID)
        {
            List<Currency> currency = currencyrepository.Currency.ToList();
            List<Property> propertyList = propertyrepository.Property.ToList();
            List<HouseType> houseTypeList = houseTypeRepository.HouseTypes.ToList();

            return View(repository.EstateItems
                    .Select(x =>
                             new EstateItemViewModel
                             {
                                 Id = x.Id,
                                 Currency = x.Currency.Name,
                                 HouseType = x.HouseType.Name,
                                 Image = x.Image,
                                 Location = x.Location,
                                 Price = x.Price,
                                 Property = x.Property.Name,
                                 Details = x.Details
                             })
                .FirstOrDefault(e => e.Id == ID));
        }

        [HttpPost]
        public IActionResult Edit(EstateItemViewModel estateItemViewModel)
        {
            string imagePath = null;
            if (estateItemViewModel.ImageUploaded.Length > 0)
            {
                imagePath = $"E:\\images\\{Guid.NewGuid()}.jpg";
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    estateItemViewModel.ImageUploaded.CopyTo(stream);
                }
            }

            int currencyID = currencyrepository.Currency
                .Select(x => new Currency { Id = x.Id, Name = x.Name })
                .FirstOrDefault(e => e.Name == estateItemViewModel.Currency).Id;

            int houseTypeID = houseTypeRepository.HouseTypes
                .Select(x => new HouseType { Id = x.Id, Name = x.Name })
                .FirstOrDefault(e => e.Name == estateItemViewModel.HouseType).Id;

            int propertyID = propertyrepository.Property
                .Select(x => new Property { Id = x.Id, Name = x.Name })
                .FirstOrDefault(e => e.Name == estateItemViewModel.Property).Id;

            var estateItem = new EstateItem()
            {
                Id = estateItemViewModel.Id,
                CurrencyId = currencyID,
                HouseTypeId = houseTypeID,
                Image = imagePath,
                Location = estateItemViewModel.Location,
                Price = estateItemViewModel.Price,
                PropertyId = propertyID,
                Details = estateItemViewModel.Details
            };
                repository.SaveEstateItem(estateItem);
                return RedirectToAction("Index");           
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            EstateItem deletedEstateItem = repository.DeleteEstateItem(Id);
            if (deletedEstateItem != null)
            {
                TempData["message"] = $"{deletedEstateItem.Id} was deleted";
            }
            return RedirectToAction("Index");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
