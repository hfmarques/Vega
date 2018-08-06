using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega.Controllers.Resources;
using Vega.Models;
using Vega.Persistence;

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakesController : ControllerBase
    {
        private readonly VegaDbContext _context;
        private readonly IMapper _mapper;

        public MakesController(VegaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Makes
        [HttpGet]
        public async Task<IActionResult> GetMakes()
        {
            var makes = await _context.Makes.Include(x => x.Models).ToListAsync();

            var makeResource = _mapper.Map<List<MakeResource>>(makes);

            return Ok(makeResource);
        }

        // GET: api/Makes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var make = await _context.Makes.FindAsync(id);

            if (make == null)
            {
                return NotFound();
            }

            var makeResource = _mapper.Map<MakeResource>(make);

            return Ok(makeResource);
        }

        // PUT: api/Makes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake([FromRoute] int id, [FromBody] Make make)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != make.Id)
            {
                return BadRequest();
            }

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeExists(id))
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

        // POST: api/Makes
        [HttpPost]
        public async Task<IActionResult> PostMake([FromBody] Make make)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Makes.Add(make);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // DELETE: api/Makes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var make = await _context.Makes.FindAsync(id);
            if (make == null)
            {
                return NotFound();
            }

            _context.Makes.Remove(make);
            await _context.SaveChangesAsync();

            return Ok(make);
        }

        private bool MakeExists(int id)
        {
            return _context.Makes.Any(e => e.Id == id);
        }
    }
}