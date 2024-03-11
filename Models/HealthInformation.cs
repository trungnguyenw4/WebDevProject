using System;
namespace WebDevProject.Models
{
	public class HealthInformation
	{
        public int HealthInformationId { get; set; }
        public int CustomerId { get; set; }
        public string? MedicalHistory { get; set; }
        public string CurrentHealthStatus { get; set; }
        public string LifestyleHabits { get; set; }


    }
}

