using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;
using WebDevelopmentProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebDevProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin,Member" )]
    public class OccupationInformationController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public OccupationInformationController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/OccupationInformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OccupationInformation>>> GetOccupationInformation()
        {
            return await _context.OccupationInformation.ToListAsync();
        }

        // GET: api/OccupationInformation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OccupationInformation>> GetOccupationInformation(int id)
        {
            var occupationInformation = await _context.OccupationInformation.FindAsync(id);

            if (occupationInformation == null)
            {
                return NotFound();
            }

            return occupationInformation;
        }

        // PUT: api/OccupationInformation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOccupationInformation(int id, OccupationInformation occupationInformation)
        {
            if (id != occupationInformation.OccupationInformationId)
            {
                return BadRequest();
            }

            _context.Entry(occupationInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OccupationInformationExists(id))
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

        // POST: api/OccupationInformation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OccupationInformation>> PostOccupationInformation(OccupationInformation occupationInformation)
        {
            _context.OccupationInformation.Add(occupationInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOccupationInformation", new { id = occupationInformation.OccupationInformationId }, occupationInformation);
        }

        // DELETE: api/OccupationInformation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOccupationInformation(int id)
        {
            var occupationInformation = await _context.OccupationInformation.FindAsync(id);
            if (occupationInformation == null)
            {
                return NotFound();
            }

            _context.OccupationInformation.Remove(occupationInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OccupationInformationExists(int id)
        {
            return _context.OccupationInformation.Any(e => e.OccupationInformationId == id);
        }
    }
}
