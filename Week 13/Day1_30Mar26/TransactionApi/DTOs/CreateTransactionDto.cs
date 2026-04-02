using System.ComponentModel.DataAnnotations;

namespace TransactionApi.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        public decimal Amount { get; set; }

        [Required]
        [RegularExpression(
            "Credit|Debit",
            ErrorMessage =
                "Type must be Credit or Debit")]
        public string Type { get; set; }
    }
}