using TransactionApi.Models;
using TransactionApi.Helpers;

namespace TransactionApi.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Users.Any())
                return;

            var user = new User
            {
                Username = "testuser",
                PasswordHash =
                    PasswordHelper.HashPassword("123456")
            };

            context.Users.Add(user);
            context.SaveChanges();

            var transactions =
                new List<Transaction>
                {
                    new Transaction
                    {
                        Amount = 1500.50m,
                        Date = DateTime.Now.AddDays(-2),
                        Type = "Credit",
                        UserId = user.Id
                    },

                    new Transaction
                    {
                        Amount = 500.00m,
                        Date = DateTime.Now.AddDays(-1),
                        Type = "Debit",
                        UserId = user.Id
                    },

                    new Transaction
                    {
                        Amount = 2500.75m,
                        Date = DateTime.Now,
                        Type = "Credit",
                        UserId = user.Id
                    }
                };

            context.Transactions.AddRange(transactions);

            context.SaveChanges();
        }
    }
}