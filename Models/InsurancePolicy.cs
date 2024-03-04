using System;


namespace WebDevProject.Models

{
public class InsurancePolicy
	{
        public int InsurancePolicyId { get; set; }
        public int CustomerId { get; set; }
        public decimal CoverageAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }

        // Navigation property
        public Customer Customer { get; set; }

        // Additional property for relationship with claims
        public ICollection<InsuranceClaim>? InsuranceClaims { get; set; }
    }
}