using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models.ViewModels.Api
{
    public class ApiVehicleViewModel
    {
        public Guid Id { get; set; }

        public int MakeYear { get; set; }

        public string Name { get; set; }

        public int PassengerCapacity { get; set; }

        public double PricePerDay { get; set; }

        public VehicleType ModelType { get; set; }

        public WheelDrive WheelDrive { get; set; }
    }
}
