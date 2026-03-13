using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HRMSystem.Models
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        // Navigation Property
        public ICollection<Employee> Employees { get; set; }
    }
}