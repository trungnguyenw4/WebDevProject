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
    public class InsuranceClaimController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public InsuranceClaimController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/InsuranceClaim
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsuranceClaim>>> GetInsuranceClaim()
        {
            return await _context.InsuranceClaim.ToListAsync();
        }

        // GET: api/InsuranceClaim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceClaim>> GetInsuranceClaim(int id)
        {
            var insuranceClaim = await _context.InsuranceClaim.FindAsync(id);

            if (insuranceClaim == null)
            {
                return NotFound();
            }

            return insuranceClaim;
        }

        // PUT: api/InsuranceClaim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsuranceClaim(int id, InsuranceClaim insuranceClaim)
        {
            if (id != insuranceClaim.InsuranceClaimId)
            {
                return BadRequest();
            }

            _context.Entry(insuranceClaim).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsuranceClaimExists(id))
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

        // POST: api/InsuranceClaim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsuranceClaim>> PostInsuranceClaim(InsuranceClaim insuranceClaim)
        {
            _context.InsuranceClaim.Add(insuranceClaim);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsuranceClaim", new { id = insuranceClaim.InsuranceClaimId }, insuranceClaim);
        }

        // DELETE: api/InsuranceClaim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsuranceClaim(int id)
        {
            var insuranceClaim = await _context.InsuranceClaim.FindAsync(id);
            if (insuranceClaim == null)
            {
                return NotFound();
            }

            _context.InsuranceClaim.Remove(insuranceClaim);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsuranceClaimExists(int id)
        {
            return _context.InsuranceClaim.Any(e => e.InsuranceClaimId == id);
        }
    }
}
