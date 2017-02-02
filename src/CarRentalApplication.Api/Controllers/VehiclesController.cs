using CarRentalApplication.Models;
using CarRentalApplication.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalApplication.Api.Controllers
{
    [Route("api/Vehicles")]
    public class VehiclesController : Controller
    {
        private VehicleRepository _vehicleRepository;

        public VehiclesController(VehicleRepository vehicleRepo)
        {
            _vehicleRepository = vehicleRepo;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_vehicleRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult Get(Guid id)
        {
            var vehicle = _vehicleRepository.GetVehicleById(id);
            if(vehicle == null)
            {
                return BadRequest();
            }
            return Ok(vehicle);
        } 
    }
}
