using Data.Abstract.Repositories;
using Data.Concrete.Repositories;
using Data.Entities;
using RADParse.Infrastructure.Defects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RADParse.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStreetRoadRepository _repository;
        private readonly List<StreetRoad> _streets;

        public HomeController(IStreetRoadRepository repo)
        {
            _repository = repo;
            _streets = _repository.StreetRoads.ToList();
        }

        public ViewResult Index()
        {
            var streets = _repository.StreetRoads.ToList();
            ViewBag.Category = new List<string> { "All", "Inspected", "Uninspected" };

            return View(streets);
        }

        public ActionResult SortStreets(string category)
        {
            var streets = _repository.StreetRoads.ToList();

            if (category == "All")
            {
                streets = _repository.StreetRoads.ToList();
            }
            else if (category == "Inspected")
            {
                streets = _repository.StreetRoads.Where(s => s.isInspected == true).ToList();
            }
            else if (category == "Uninspected")
            {
                streets = _repository.StreetRoads.Where(s => s.isInspected == false).ToList();

               return PartialView("SortedStreetsUninspected", streets);
            }

            return PartialView("SortedStreets", streets);
        }

        [HttpPost]
        public void AddDefects(string[] streetsParam)
        {
            List<StreetRoad> uninspectedStreets = new List<StreetRoad>();

            foreach (var str in streetsParam)
            {
                uninspectedStreets.Add(_repository.StreetRoads.Where(s => s.Id == int.Parse(str)).FirstOrDefault());
            }       

            for (var i = 0; i < uninspectedStreets.Count; i++)
            {
                var defect = new DefectsAddingMechanism();

                defect.EnterToSystem();
                defect.AddDefectsToRoads(uninspectedStreets[i]);
                _repository.SaveIsInspected(uninspectedStreets[i]);
                defect.CloseBrowser();
            }
        }

        public ActionResult SearchStreet(string streetName, List<StreetRoad> strs)
        {
            if (!string.IsNullOrWhiteSpace(streetName))
            {
                var streets = _streets.Where(s => s.StreetName.ToLower().Contains(streetName.ToLower())).ToList();

                return PartialView(streets);
            }
            else
            {
                return PartialView("SearchStreetIsNull");
            }
        }
    }
}