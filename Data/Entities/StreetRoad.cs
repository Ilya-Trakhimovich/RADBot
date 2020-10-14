using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class StreetRoad
    {
        [Key]
        public int Id { get; set; }
        public string StreetName { get; set; }
        public int StreetLenght { get; set; }
        public int BeginOfStreet { get; set; }
        public int EndOfStreet { get; set; }
        public bool isInspected { get; set; }
    }
}
