using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract.Repositories
{
    public interface IStreetRoadRepository
    {
        IEnumerable<StreetRoad> StreetRoads { get; }
        void AddStreet(StreetRoad streetRoad);
    }
}
