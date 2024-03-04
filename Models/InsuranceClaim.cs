using System;
namespace WebDevProject.Models
{
	public class InsuranceClaim
	{
        public int InsuranceClaimId { get; set; }
        public int InsurancePolicyId { get; set; }
        public string ClaimDetails { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal ClaimAmount { get; set; }

        // Navigation property
        public InsurancePolicy InsurancePolicy { get; set; }
    }
}

