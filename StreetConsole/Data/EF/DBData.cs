using Data.Abstract.Repositories;
using Data.Concrete.Repositories;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetConsole.Data.EF
{
    public class DBData : IDisposable
    {
        private IStreetRoadRepository _repo;

        public DBData()
        {
            _repo = new StreetRoadRepository();
        }

        public void AddRoadToDB(StreetRoad street)
        {
            _repo.AddStreet(street);
        }

        public void Dispose() { }
    }
}
