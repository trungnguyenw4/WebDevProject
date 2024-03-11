using System.Text.Json.Serialization;

namespace WebDevProject.Models

{
    public class Customer
	{
		
        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
  
       
        [JsonIgnore]
        public HealthInformation? HealthInformation { get; set; }
        
        public FinancialInformation? FinancialInformation { get; set; }
        [JsonIgnore]
        public ICollection<InsurancePolicy>? InsurancePolicies { get; set; }
        [JsonIgnore]
        public OccupationInformation? OccupationInformation { get; set; }
    }
	
}

