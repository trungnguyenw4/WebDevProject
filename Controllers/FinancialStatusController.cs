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
    public class FinancialInformationController : ControllerBase
    {
        private readonly InsuranceContext _context;

        public FinancialInformationController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/FinancialInformation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinancialInformation>>> GetFinancialInformation()
        {
            return await _context.FinancialInformation.ToListAsync();
        }

        // GET: api/FinancialInformation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinancialInformation>> GetFinancialInformation(int id)
        {
            var FinancialInformation = await _context.FinancialInformation.FindAsync(id);

            if (FinancialInformation == null)
            {
                return NotFound();
            }

            return FinancialInformation;
        }

        // PUT: api/FinancialInformation/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinancialInformation(int id, FinancialInformation FinancialInformation)
        {
            if (id != FinancialInformation.FinancialInformationId)
            {
                return BadRequest();
            }

            _context.Entry(FinancialInformation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinancialInformationExists(id))
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

        // POST: api/FinancialInformation
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinancialInformation>> PostFinancialInformation(FinancialInformation FinancialInformation)
        {
            _context.FinancialInformation.Add(FinancialInformation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinancialInformation", new { id = FinancialInformation.FinancialInformationId }, FinancialInformation);
        }

        // DELETE: api/FinancialInformation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinancialInformation(int id)
        {
            var FinancialInformation = await _context.FinancialInformation.FindAsync(id);
            if (FinancialInformation == null)
            {
                return NotFound();
            }

            _context.FinancialInformation.Remove(FinancialInformation);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinancialInformationExists(int id)
        {
            return _context.FinancialInformation.Any(e => e.FinancialInformationId == id);
        }
    }
}
