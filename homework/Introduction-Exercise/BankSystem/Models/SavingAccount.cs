namespace BankSystem.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SavingAccount
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public void AddInterest()
        {
            this.Balance += this.InterestRate;
            Console.WriteLine($"Added interest to {this.AccountNumber}. Current Balance: {this.Balance:F2}");
        }

        public void DepositMoney(decimal money)
        {
            this.Balance += money;
            Console.WriteLine($"Account {this.AccountNumber} has balance of {this.Balance:F2}");
        }

        public void WithdrawMoney(decimal money)
        {
            if (this.Balance - money < 0)
                throw new ArgumentException(
                    $"Unsufficient Amount!\nWanted Amount: {money} -- Current Balance: {this.Balance}");

            this.Balance -= money;
            Console.WriteLine($"Account {this.AccountNumber} has balance of {this.Balance:F2}");
        }
    }
}
