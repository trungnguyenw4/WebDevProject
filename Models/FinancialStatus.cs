using System;
namespace WebDevProject.Models
{
	public class FinancialInformation
    {
		

            public int FinancialInformationId { get; set; }
        public int CustomerId { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal Debts { get; set; }
        public int FinancialDependents { get; set; }

        // Navigation property
        public Customer Customer { get; set; }
    }
	
}

