using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class VehicleFilter
    {
        public int Id { get; set; }
        public string FilterName { get; set; }
        public string PropertyType { get; set; }
    }
}
