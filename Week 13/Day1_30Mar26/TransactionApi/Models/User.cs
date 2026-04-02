using System.ComponentModel.DataAnnotations;

namespace TransactionApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public List<Transaction> Transactions { get; set; }
    }
}