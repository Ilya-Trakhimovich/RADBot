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
        private string _category;

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
                _category = category;
                streets = _repository.StreetRoads.ToList();
            }
            else if (category == "Inspected")
            {
                _category = category;
                streets = _repository.StreetRoads.Where(s => s.isInspected == true).ToList();
            }
            else if (category == "Uninspected")
            {
                _category = category;
                streets = _repository.StreetRoads.Where(s => s.isInspected == false).ToList();

                return PartialView("SortedStreetsUninspected", streets);
            }

            return PartialView("SortedStreets", streets);
        }

        [HttpPost]
        public void AddDefects(string[] streetsParam)
        {
            if (streetsParam.Length <= 0)
            {
                throw new Exception();
            }

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

        public ActionResult SearchStreet(string streetName, string cat = "All")
        {
            var streets = new List<StreetRoad>();

            if (!string.IsNullOrWhiteSpace(streetName))
            {
                if (cat == "All")
                {
                    streets = _streets.Where(s => s.StreetName.ToLower().Contains(streetName.ToLower())).ToList();
                }
                else if (cat == "Inspected")
                {

                }
                else if (cat == "Uninspected")
                {

                }

                return PartialView(streets);
            }
            else
            {
                return PartialView("SearchStreetIsNull");
            }
        }
    }
}