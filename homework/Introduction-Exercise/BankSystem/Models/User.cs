using System.ComponentModel.DataAnnotations;

namespace BankSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    public class User
    {
        private string name;
        private string password;
        private string email;

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name
        {
            get => this.name;
            set
            {
                string pattern = "[a-zA-Z][a-zA-Z0-9]{2,}";
                if (Regex.IsMatch(value, pattern))
                {
                    this.name = value;
                }
                else
                {
                    throw new ArgumentException("Invalid username.");
                }
            }
        }

        [Required]
        public string Password
        {
            get => this.password;
            set
            {
                string pattern = "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{6,}";
                if (Regex.IsMatch(value, pattern))
                {
                    this.password = value;
                }
                else
                {
                    throw new ArgumentException("Invalid password.");
                }
            }
        }

        [Required]
        public string Email
        {
            get => this.email;
            set
            {
                string pattern = "^\\w+([-_+.\']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*$";
                if (Regex.IsMatch(value, pattern))
                {
                    this.email = value;
                }
                else
                {
                    throw new ArgumentException("Invalid email.");
                }
            }
        }

        public List<SavingAccount> SavingAccounts { get; set; } = new List<SavingAccount>();

        public List<CheckingAccount> CheckingAccounts { get; set; } = new List<CheckingAccount>();
    }
}
