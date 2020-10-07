using Data.Abstract.Repositories;
using Data.Concrete.Repositories;
using Data.Entities;
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

        public HomeController(IStreetRoadRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index()
        {
            var streets = _repository.StreetRoads.ToList();

            return View(streets);
        }
    }
}