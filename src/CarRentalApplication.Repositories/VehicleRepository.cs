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

        public IEnumerable<Vehicle> GetAll()
        {
            return _context.Vehicles.ToList();
        }

        public IEnumerable<Vehicle> GetAllAvailableVehicles()
        {
            return _context.Vehicles.Where(x => x.IsAvailable).ToList();
        }

        public Vehicle GetVehicleById(Guid id)
        {
            return _context.Vehicles.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Vehicle> GetVehiclesByFilter(VehicleProperty selectedVehicleProperty, string filter)
        {
            switch (selectedVehicleProperty.FilterName)
            {
                case "ModelType":
                        return GetVehiclesByModelType(filter);
                case "MakeYear":                    
                        return GetVehiclesByMakeYear(filter);
                case "PassengerCapacity":                    
                        return GetVehiclesByPassengerCapacity(filter);                    
                case "WheelDrive":                    
                        return GetVehiclesByWheelDrive(filter);                    
                default:
                    return GetVehiclesByModelType(filter);
            }
        }

        public IEnumerable<Vehicle> GetVehiclesByProperty(VehicleProperty selectedVehicleProperty)
        {
            switch (selectedVehicleProperty.FilterName)
            {
                case "ModelType":
                    return GetVehiclesByModelType("All");
                case "MakeYear":
                    return GetVehiclesByMakeYear("2016");
                case "PassengerCapacity":
                    return GetVehiclesByPassengerCapacity("5");
                case "WheelDrive":
                    return GetVehiclesByWheelDrive("AllWheel");
                default:
                    return GetVehiclesByModelType("All");
            }
        }

        public void AddNewVehicle(Vehicle newVehicle)
        {
            _context.Vehicles.Add(newVehicle);
            _context.SaveChanges();
        }

        private IEnumerable<Vehicle> GetVehiclesByModelType(string vehiclefilter)
        {
            VehicleType equivalentVehicleType;
            if (Enum.TryParse(vehiclefilter, out equivalentVehicleType))
            {
                switch (equivalentVehicleType)
                {
                    case VehicleType.All:
                        return GetAll();
                    default:
                        return _context.Vehicles.Where(x => x.ModelType == equivalentVehicleType);
                }
            }

            return GetAll();
        }

        private IEnumerable<Vehicle> GetVehiclesByMakeYear(string vehicleFilter)
        {
            int b = int.Parse(vehicleFilter);
            return _context.Vehicles.Where(x => x.MakeYear == int.Parse(vehicleFilter));
        }

        private IEnumerable<Vehicle> GetVehiclesByPassengerCapacity(string vehicleFilter)
        {
            int a = int.Parse(vehicleFilter);
            return _context.Vehicles.Where(x => x.PassengerCapacity == int.Parse(vehicleFilter));
        }

        private IEnumerable<Vehicle> GetVehiclesByWheelDrive(string vehicleFilter)
        {
            WheelDrive equivalentWheelDrive;
            if (Enum.TryParse(vehicleFilter, out equivalentWheelDrive))
            {
                switch (equivalentWheelDrive)
                {
                    default:
                        return _context.Vehicles.Where(x => x.WheelDrive == equivalentWheelDrive);
                }
            }
            return GetAll();
        }

        public SelectList GetFiltersByModelTypes()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "All", PropertyType = "ModelType" },
                new VehicleFilter {Id=0, FilterName="Car", PropertyType = "ModelType"},
                new VehicleFilter {Id=1, FilterName="Suv", PropertyType = "ModelType" },
                new VehicleFilter {Id=2, FilterName="Truck", PropertyType = "ModelType" },
                new VehicleFilter {Id=3, FilterName="Luxury", PropertyType = "ModelType" }
            };
            return new SelectList(filters, "FilterName", "FilterName");
        }

        public SelectList GetFilterByMakeYear()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "2016", PropertyType = "MakeYear" },
                new VehicleFilter {Id=0, FilterName="2015", PropertyType = "MakeYear"},
                new VehicleFilter {Id=1, FilterName="2014", PropertyType = "MakeYear" },
                new VehicleFilter {Id=2, FilterName="2013", PropertyType = "MakeYear" },
            };
            return new SelectList(filters, "FilterName", "FilterName");
        }

        public SelectList GetFilterByPassengerCapacity()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "6", PropertyType = "PassengerCapacity" },
                new VehicleFilter {Id=0, FilterName="5", PropertyType = "PassengerCapacity"},
                new VehicleFilter {Id=1, FilterName="4", PropertyType = "PassengerCapacity" },
                new VehicleFilter {Id=2, FilterName="3", PropertyType = "PassengerCapacity" },
            };
            return new SelectList(filters, "FilterName", "FilterName");
        }

        public SelectList GetFilterByWheelDrive()
        {
            var filters = new List<VehicleFilter>()
            {
                new VehicleFilter {Id=4, FilterName = "FrontWheel", PropertyType = "WheelDrive" },
                new VehicleFilter {Id=0, FilterName="AllWheel", PropertyType = "WheelDrive"},
                new VehicleFilter {Id=1, FilterName="FourWheel", PropertyType = "WheelDrive" },
                new VehicleFilter {Id=2, FilterName="RearWheel", PropertyType = "WheelDrive" },
            };
            return new SelectList(filters, "FilterName", "FilterName");
        }

        public SelectList GetVehicleFilterProperties()
        {
            var properties = new List<VehicleProperty>()
            {
                new VehicleProperty {Id=4, FilterName = "ModelType" },
                new VehicleProperty {Id=0, FilterName="MakeYear"},
                new VehicleProperty {Id=1, FilterName="PassengerCapacity" },
                new VehicleProperty {Id=2, FilterName="WheelDrive" },
            };
            return new SelectList(properties, "FilterName", "FilterName");
        }
    }
}
