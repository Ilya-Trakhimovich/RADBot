using Data.Abstract.Repositories;
using Data.DBContext;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.Repositories
{
    public class StreetRoadRepository : IStreetRoadRepository
    {
        private readonly StreetContext _context;

        public StreetRoadRepository()
        {
            _context = new StreetContext();
        }

        public IEnumerable<StreetRoad> StreetRoads => _context.StreetRoads;

        public void AddStreet(StreetRoad streetRoad)
        {
            _context.StreetRoads.Add(streetRoad);
            _context.SaveChanges();
        }

        //private bool disposed = false;

        //public virtual void Dispose(bool disposing)
        //{
        //    if (!disposed)
        //    {
        //        if (disposing)
        //        {
        //            _context.Dispose();
        //        }
        //    }

        //    this.disposed = true;

        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}
    }
}
