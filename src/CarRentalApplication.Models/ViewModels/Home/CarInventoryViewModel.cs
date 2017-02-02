using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Home
{
    public class CarInventoryViewModel
    {
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public SelectList VehicleFilters { get; set; }
        public SelectList VehicleProperties { get; set; }
        public string SelectedFilter { get; set; }
        public VehicleProperty selectedVehiclePropery { get; set; }

        public string PropertyFilter { get; set; }

    }
}
