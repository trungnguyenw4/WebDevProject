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
    public class HealthInformationController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public HealthInformationController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/HealthInformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HealthInformation>>> GetHealthInformation()
        {
            return await _context.HealthInformation.ToListAsync();
        }

        // GET: api/HealthInformation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HealthInformation>> GetHealthInformation(int id)
        {
            var healthInformation = await _context.HealthInformation.FindAsync(id);

            if (healthInformation == null)
            {
                return NotFound();
            }

            return healthInformation;
        }

        // PUT: api/HealthInformation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHealthInformation(int id, HealthInformation healthInformation)
        {
            if (id != healthInformation.HealthInformationId)
            {
                return BadRequest();
            }

            _context.Entry(healthInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HealthInformationExists(id))
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

        // POST: api/HealthInformation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HealthInformation>> PostHealthInformation(HealthInformation healthInformation)
        {
            _context.HealthInformation.Add(healthInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHealthInformation", new { id = healthInformation.HealthInformationId }, healthInformation);
        }

        // DELETE: api/HealthInformation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHealthInformation(int id)
        {
            var healthInformation = await _context.HealthInformation.FindAsync(id);
            if (healthInformation == null)
            {
                return NotFound();
            }

            _context.HealthInformation.Remove(healthInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HealthInformationExists(int id)
        {
            return _context.HealthInformation.Any(e => e.HealthInformationId == id);
        }
    }
}
