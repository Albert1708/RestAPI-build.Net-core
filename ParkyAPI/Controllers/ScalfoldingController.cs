using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkyAPI.Data;
using ParkyAPI.Models;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class adsf : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public adsf(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/NationalParks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NationalPark>>> GetNationalParks()
        {
            return await _context.NationalParks.ToListAsync();
        }

        // GET: api/NationalParks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NationalPark>> GetNationalPark(int id)
        {
            var nationalPark = await _context.NationalParks.FindAsync(id);

            if (nationalPark == null)
            {
                return NotFound();
            }

            return nationalPark;
        }

        // PUT: api/NationalParks/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNationalPark(int id, NationalPark nationalPark)
        {
            if (id != nationalPark.Id)
            {
                return BadRequest();
            }

            _context.Entry(nationalPark).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NationalParkExists(id))
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

        // POST: api/NationalParks
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NationalPark>> PostNationalPark(NationalPark nationalPark)
        {
            _context.NationalParks.Add(nationalPark);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNationalPark", new { id = nationalPark.Id }, nationalPark);
        }

        // DELETE: api/NationalParks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NationalPark>> DeleteNationalPark(int id)
        {
            var nationalPark = await _context.NationalParks.FindAsync(id);
            if (nationalPark == null)
            {
                return NotFound();
            }

            _context.NationalParks.Remove(nationalPark);
            await _context.SaveChangesAsync();

            return nationalPark;
        }

        private bool NationalParkExists(int id)
        {
            return _context.NationalParks.Any(e => e.Id == id);
        }
    }
}
