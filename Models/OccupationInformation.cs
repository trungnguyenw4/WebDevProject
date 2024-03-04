using System;
namespace WebDevProject.Models
{
	public class OccupationInformation
	{
        public int OccupationInformationId { get; set; }
        public int CustomerId { get; set; }
        public string Occupation { get; set; }
        public string EmployerName { get; set; }
        public string EmploymentStability { get; set; }

        // Navigation property
        public Customer Customer { get; set; }
    }
}

