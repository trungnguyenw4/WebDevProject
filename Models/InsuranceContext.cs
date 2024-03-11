using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace WebDevelopmentProject.Models
{
	public class InsuranceContext: IdentityDbContext<IdentityUser>
    {

        public InsuranceContext(DbContextOptions<InsuranceContext> options): base(options)
        {
       

        }


        public DbSet<Customer> C { get; set; }
        public DbSet<FinancialInformation> FinancialInformation { get; set; }
        public DbSet<InsuranceClaim> InsuranceClaim { get; set; }
        public DbSet<OccupationInformation> OccupationInformation { get; set; }
        public DbSet<HealthInformation> HealthInformation { get; set; }
        public DbSet<InsurancePolicy> InsurancePolics { get; set; }

   

    }
}

