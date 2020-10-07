using Data.Concrete.Repositories;
using StreetConsole.Data.EF;
using StreetConsole.Data.GetData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreetConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            WebDriverData driver = new WebDriverData();
            driver.GetListofStreets();
            driver.ConvertStringToStreets();
            driver.PrintStreets();

            var streets = driver.GetStreets;

            using (DBData db = new DBData())
            {
                foreach (var street in streets)
                {
                    db.AddRoadToDB(street);
                }
            }                
        }
    }
}
