using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;
using WebDevelopmentProject.Models;
using NuGet.Packaging.Core;
using NuGet.Protocol.Core.Types;
using Microsoft.AspNetCore.Authorization;

namespace WebDevProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Roles = "Admin,Member" )]
    public class InsurancePoliciesController : ControllerBase
    {
        // private readonly IRepositoryCertificateInfo repository;
        // private readonly ILogger<InsurancePoliciesController> logger;

        // public InsurancePoliciesController(IRepositoryCertificateInfo repository,ILogger<InsurancePoliciesController> logger ){
        //      this.repository=repository;
        //      this.logger=logger;
        // }
        private readonly InsuranceContext _context;

        public InsurancePoliciesController(InsuranceContext context)
        {
            _context = context;
        }

        // GET: api/InsurancePolicies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InsurancePolicy>>> GetInsurancePolicies()
        {   
            //logger.LogInformation("Getting policies");
            return await _context.InsurancePolics.ToListAsync();
        }

        // GET: api/InsurancePolicies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePolicy>> GetInsurancePolicy(int id)
        {
            var insurancePolicy = await _context.InsurancePolics.FindAsync(id);

            if (insurancePolicy == null)
            {   
                //logger.LogInformation("Policy not found.");
                return NotFound();
            }

            //logger.LogWarning("Policy found.");
            return insurancePolicy;
        }

        // PUT: api/InsurancePolicies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInsurancePolicy(int id, InsurancePolicy insurancePolicy)
        {
            if (id != insurancePolicy.InsurancePolicyId)
            {
                return BadRequest();
            }

            _context.Entry(insurancePolicy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InsurancePolicyExists(id))
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

        // POST: api/InsurancePolicies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InsurancePolicy>> PostInsurancePolicy(InsurancePolicy insurancePolicy)
        {
            _context.InsurancePolics.Add(insurancePolicy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInsurancePolicy", new { id = insurancePolicy.InsurancePolicyId }, insurancePolicy);
        }

        // DELETE: api/InsurancePolicies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInsurancePolicy(int id)
        {
            var insurancePolicy = await _context.InsurancePolics.FindAsync(id);
            if (insurancePolicy == null)
            {
                return NotFound();
            }

            _context.InsurancePolics.Remove(insurancePolicy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InsurancePolicyExists(int id)
        {
            return _context.InsurancePolics.Any(e => e.InsurancePolicyId == id);
        }
    }
}
