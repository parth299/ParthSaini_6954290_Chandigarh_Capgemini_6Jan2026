using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TransactionApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Type { get; set; } // Credit / Debit

        [ForeignKey("User")]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}