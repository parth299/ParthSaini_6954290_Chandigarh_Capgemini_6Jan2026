using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMSystem.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Position { get; set; }

        public decimal Salary { get; set; }

        // Foreign Key
        public int CompanyId { get; set; }

        // Navigation Property
        public Company Company { get; set; }
    }
}