using CarRentalApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Repositories
{
    public class VehicleRepository
    {
        private AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Returns all Vehicles without any filter
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _context.Vehicles.AsEnumerable();
        }

        /// <summary>
        /// Returns only available vehicles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetOnlyAvaliableVehicles()
        {
            return _context.Vehicles.Where(x => x.IsAvailable).ToList();
        }

        public Vehicle GetVehicleById(Guid id)
        {
            return _context.Vehicles.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Vehicle> GetVehiclesByFilter(VehicleProperty selectedVehicleProperty, string filter)
        {
            switch (selectedVehicleProperty.FilterString)
            {
                case "ModelType":
                        return GetVehiclesByModelType(filter);
                case "MakeYear":                    
                        return GetVehiclesByMakeYear(selectedVehicleProperty, filter);
                case "PassengerCapacity":                    
                        return GetVehiclesByPassengerCapacity(selectedVehicleProperty, filter);                    
                case "WheelDrive":                    
                        return GetVehiclesByWheelDrive(filter);                    
                default:
                    return GetVehiclesByModelType(filter);
            }
        }

        /// <summary>
        /// Returns vehicles that match the default filter value for the property argument
        /// </summary>
        /// <param name="selectedVehicleProperty"></param>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetVehiclesByProperty(VehicleProperty selectedVehicleProperty)
        {
            switch (selectedVehicleProperty.FilterString)
            {
                case "ModelType":
                    return GetVehiclesByModelType(selectedVehicleProperty.DefaultModelType);
                case "MakeYear":
                    return GetVehiclesByMakeYear(selectedVehicleProperty, selectedVehicleProperty.DefaultMakeYear);
                case "PassengerCapacity":
                    return GetVehiclesByPassengerCapacity(selectedVehicleProperty, selectedVehicleProperty.DefaultPassangerCapacity);
                case "WheelDrive":
                    return GetVehiclesByWheelDrive(selectedVehicleProperty.DefaultModelType);
                default:
                    return GetVehiclesByModelType(selectedVehicleProperty.DefaultModelType);
            }
        }

        /// <summary>
        /// Filter Vehicles By Their Model Type
        /// </summary>
        /// <param name="modelTypeFilter"></param>
        /// <returns></returns>
        private IEnumerable<Vehicle> GetVehiclesByModelType(string modelTypeFilter)
        {
            VehicleType matchingVehicleTypeEnum;

            //Convert string filter into enum(VehicleType)
            if (Enum.TryParse(modelTypeFilter, out matchingVehicleTypeEnum))
            {
                switch (matchingVehicleTypeEnum)
                {
                    case VehicleType.All:
                        return GetAllVehicles();
                    default:
                        return _context.Vehicles.Where(x => x.ModelType == matchingVehicleTypeEnum).AsEnumerable();
                }
            }

            return GetAllVehicles();
        }

        /// <summary>
        /// Filter Vehicles By Make Year
        /// </summary>
        /// <param name="selectedVehicleProperty"></param>
        /// <param name="makeYearFilter"></param>
        /// <returns></returns>
        private IEnumerable<Vehicle> GetVehiclesByMakeYear(VehicleProperty selectedVehicleProperty, string makeYearFilter)
        {
            try
            {
                return _context.Vehicles.Where(x => x.MakeYear == int.Parse(makeYearFilter));
            }
            catch (Exception)
            {
                return _context.Vehicles.Where(x => x.MakeYear == int.Parse(selectedVehicleProperty.DefaultMakeYear));
            }                       
        }

        /// <summary>
        /// Filter Vehicles By Passanger Capacity
        /// </summary>
        /// <param name="selectedVehicleProperty"></param>
        /// <param name="passangerCapacityFilter"></param>
        /// <returns></returns>
        private IEnumerable<Vehicle> GetVehiclesByPassengerCapacity(VehicleProperty selectedVehicleProperty, string passangerCapacityFilter)
        {
            try
            {
                return _context.Vehicles.Where(x => x.PassengerCapacity == int.Parse(passangerCapacityFilter));
            }
            catch (Exception)
            {
                return _context.Vehicles.Where(x => x.PassengerCapacity == int.Parse(selectedVehicleProperty.DefaultPassangerCapacity));
            }
            
        }

        private IEnumerable<Vehicle> GetVehiclesByWheelDrive(string wheelDriveFilter)
        {
            WheelDrive equivalentWheelDriveEnum;

            //convert filter string into enum(WheelDrive)
            if (Enum.TryParse(wheelDriveFilter, out equivalentWheelDriveEnum))
            {
                switch (equivalentWheelDriveEnum)
                {
                    default:
                        return _context.Vehicles.Where(x => x.WheelDrive == equivalentWheelDriveEnum);
                }
            }
            return GetAllVehicles();
        }

        /// <summary>
        /// Returns All Filter Values for "Model Type" Vehicle Property
        /// </summary>
        /// <returns></returns>
        public SelectList GetModelTypeFilterValues()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "All", PropertyType = "ModelType" },
                new VehicleFilter {Id=0, FilterName="Car", PropertyType = "ModelType"},
                new VehicleFilter {Id=1, FilterName="Suv", PropertyType = "ModelType" },
                new VehicleFilter {Id=2, FilterName="Truck", PropertyType = "ModelType" },
                new VehicleFilter {Id=3, FilterName="Luxury", PropertyType = "ModelType" }
            };
            return new SelectList(filters, "FilterName", "PropertyType");
        }

        /// <summary>
        /// Returns all Filter Values for Make Year Property
        /// </summary>
        /// <returns></returns>
        public SelectList GetMakeYearFilterValues()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "2016", PropertyType = "MakeYear" },
                new VehicleFilter {Id=0, FilterName="2015", PropertyType = "MakeYear"},
                new VehicleFilter {Id=1, FilterName="2014", PropertyType = "MakeYear" },
                new VehicleFilter {Id=2, FilterName="2013", PropertyType = "MakeYear" },
            };
            return new SelectList(filters, "FilterName", "PropertyType");
        }

        /// <summary>
        /// Returns all Filter Values for Passanger Capacity Property
        /// </summary>
        /// <returns></returns>
        public SelectList GetPassengerCapacityFilterValues()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "6", PropertyType = "PassengerCapacity" },
                new VehicleFilter {Id=0, FilterName="5", PropertyType = "PassengerCapacity"},
                new VehicleFilter {Id=1, FilterName="4", PropertyType = "PassengerCapacity" },
                new VehicleFilter {Id=2, FilterName="3", PropertyType = "PassengerCapacity" },
            };
            return new SelectList(filters, "FilterName", "PropertyType");
        }

        /// <summary>
        /// Returns all Filter Values for Wheel Drive Property
        /// </summary>
        /// <returns></returns>
        public SelectList GetWheelDriveFilterValues()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "FrontWheel", PropertyType = "WheelDrive" },
                new VehicleFilter {Id=0, FilterName="AllWheel", PropertyType = "WheelDrive"},
                new VehicleFilter {Id=1, FilterName="FourWheel", PropertyType = "WheelDrive" },
                new VehicleFilter {Id=2, FilterName="RearWheel", PropertyType = "WheelDrive" },
            };
            return new SelectList(filters, "FilterName", "PropertyType");
        }

        /// <summary>
        /// Returns all Vehicle Properties that can be filtered on
        /// </summary>
        /// <returns></returns>
        public SelectList GetVehicleFilterProperties()
        {
            var properties = new List<VehicleProperty>()
            {
                new VehicleProperty {Id=4, FilterString = "ModelType" },
                new VehicleProperty {Id=0, FilterString="MakeYear"},
                new VehicleProperty {Id=1, FilterString="PassengerCapacity" },
                new VehicleProperty {Id=2, FilterString="WheelDrive" },
            };
            return new SelectList(properties, "Id", "FilterString");
        }
    }
}
