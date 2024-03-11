using System;
using System.Text.Json.Serialization;


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

        // Additional property for relationship with claims
        [JsonIgnore]
        public ICollection<InsuranceClaim>? InsuranceClaims { get; set; }
        

    }
}