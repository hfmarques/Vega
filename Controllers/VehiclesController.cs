using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Models;
using Vega.Persistence;
using Vega.Resources;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Vehicles
        [HttpGet]
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _context.Vehicles;
        }

        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            var feature = vehicle.Features.FirstOrDefault();

            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicleResource = _mapper.Map<VehicleResource>(vehicle);

            return Ok(vehicleResource);
        }

        // PUT: api/Vehicles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicle([FromRoute] int id, [FromBody] Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.Id)
            {
                return BadRequest();
            }

            _context.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehicles
        [HttpPost]
        public async Task<IActionResult> PostVehicle([FromBody] VehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = _mapper.Map<Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            _context.Vehicles.Add(vehicle);

            await _context.SaveChangesAsync();

            var resultMap = _mapper.Map<VehicleResource>(vehicle);

            return CreatedAtAction("GetVehicle", new { id = resultMap.Id }, resultMap);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            _context.Vehicles.Remove(vehicle);
            await _context.SaveChangesAsync();

            return Ok(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}