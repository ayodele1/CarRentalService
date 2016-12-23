using CarRentalApplication.Models;
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

        public IEnumerable<Vehicle> GetVehicleByFilter(VehicleType vt)
        {
            if (vt == VehicleType.All)
            {
                return GetAll();
            }
            return _context.Vehicles.Where(x => x.ModelType == vt);
        }

        public void AddNewVehicle(Vehicle newVehicle)
        {
            _context.Vehicles.Add(newVehicle);
            _context.SaveChanges();
        }
    }
}
