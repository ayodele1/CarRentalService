using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public enum VehicleType
    {
        Car,
        Suv,
        Truck,
        Luxury,
        All
    }

    public enum WheelDrive
    {
        FrontWheel,
        AllWheel,
        FourWheel,
        RearWheel
    }

    public class Vehicle : IModificationHistory
    {
        public Guid Id { get; set; }

        public int MakeYear { get; set; }

        public string Name { get; set; }

        public string ImageName { get; set; }

        public int PassengerCapacity { get; set; }

        public double PricePerDay { get; set; }

        public VehicleType ModelType { get; set; }

        public WheelDrive WheelDrive { get; set; }

        public int isAvailable { get; set; }

        [NotMapped]
        public bool IsAvailable
        {
            get { return isAvailable != 0; }
            set { isAvailable = value ? 1 : 0; }
        }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
