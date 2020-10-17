﻿using Data.Abstract.Repositories;
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
            }

            return PartialView("SortedStreets", streets);
        }

        public void AddDefects()
        {
            for (var i = 0; i < _streets.Count; i++)
            {
                var defect = new DefectsAddingMechanism();

                defect.EnterToSystem();
                defect.AddDefectsToRoads(_streets[i]);
                _repository.SaveIsInspected(_streets[i]);
                defect.CloseBrowser();
            }
        }

        public ActionResult SearchStreet(string streetName)
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