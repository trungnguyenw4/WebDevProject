using System;
using System.ComponentModel.DataAnnotations;
namespace WebDevProject.Models
{
	public class FinancialInformation
    {
		
        [Key]
        public int FinancialInformationId { get; set; }
        public int CustomerId { get; set; }
        public decimal AnnualIncome { get; set; }
        public decimal? Debts { get; set; }
        public int? FinancialDependents { get; set; }


    }
	
}

