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

            return View(streets);
        }

        public void AddDefects()
        {       
            for (var i = 0; i < _streets.Count; i++)
            {
                var defect = new DefectsAddingMechanism();
                defect.EnterToSystem();
                defect.AddDefectsToRoads(_streets[i]);
                defect.CloseBrowser();
            }
        }
    }
}