using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Models
{
    public class VehicleProperty
    {
        private readonly string _defaultMakeYear = "2016";
        private readonly string _defaultModelType = "All";
        private readonly string _defaultPassangerCapacity = "5";
        private readonly string _defaultWheelDrive = "AllWheel";

        public int Id { get; set; }

        public string DefaultMakeYear { get { return _defaultMakeYear; } }

        public string DefaultModelType { get { return _defaultModelType; } }

        public string DefaultPassangerCapacity { get { return _defaultPassangerCapacity; } }

        public string DefaultWheelDrive { get { return _defaultWheelDrive; } }

        public string FilterString
        {
            //How the property is displayed in the UI
            get;
            set;
        }

        public string DefaultFilterValue { get; set; }//Initial value to filter by e.g. make year can be 2016.

        public override string ToString()
        {
            return FilterString;
        }
    }
}
