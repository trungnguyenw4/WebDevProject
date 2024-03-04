using System;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using WebDevProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace WebDevelopmentProject.Models
{
	public class InsuranceContext: IdentityDbContext<IdentityUser>
    {
        public InsuranceContext(DbContextOptions<InsuranceContext> options): base(options)
        { }

        public DbSet<Customer> C { get; set; }
        public DbSet<InsurancePolicy> InsurancePolics { get; set; }
    }
}

