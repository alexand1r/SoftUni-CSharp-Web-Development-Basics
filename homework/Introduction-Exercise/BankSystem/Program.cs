namespace BankSystem
{
    using System;
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    class Program
    {
        private static User user = null;
        private static Random rnd = new Random();

        static void Main(string[] args)
        {
            using (var db = new BankSystemContext())
            {
                db.Database.Migrate();

                var cmd = Console.ReadLine();
                while (!cmd.Equals("Exit"))
                {
                    string[] data = cmd.Split(" ");
                    string command = data[0];

                    try
                    {
                        switch (command)
                        {
                            case "Register":
                                Register(db, data);
                                break;
                            case "Login":
                                Login(db, data);
                                break;
                            case "Logout":
                                Logout();
                                break;
                            case "Add":
                                Add(db, data);
                                break;
                            case "ListAccounts":
                                ListAccounts();
                                break;
                            case "Deposit":
                                Deposit(db, data);
                                break;
                            case "Withdraw":
                                Withdraw(db, data);
                                break;
                            case "DeductFee":
                                DeductFee(db, data);
                                break;
                            case "AddInterest":
                                AddInterest(db, data);
                                break;
                            default: break;
                        }
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                    }

                    cmd = Console.ReadLine();
                }
                
            }
        }

        private static void AddInterest(BankSystemContext db, string[] data)
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            string accountNumber = data[1];
            SavingAccount savingsAccount = user.SavingAccounts.FirstOrDefault(ca => ca.AccountNumber == accountNumber);
            if (savingsAccount != null) 
                savingsAccount.AddInterest();
            else
                throw new ArgumentException("No such existing account.");

            db.SaveChanges();
        }

        private static void DeductFee(BankSystemContext db, string[] data)
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            string accountNumber = data[1];
            CheckingAccount checkingsAccount = user.CheckingAccounts.FirstOrDefault(ca => ca.AccountNumber == accountNumber);
            if (checkingsAccount != null)
                checkingsAccount.DeductFee();
            else
                throw new ArgumentException("No such existing account.");

            db.SaveChanges();
        }

        private static void Withdraw(BankSystemContext db, string[] data)
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            string accountNumber = data[1];
            decimal money = decimal.Parse(data[2]);
            bool found = false;

            foreach (var userSavingAccount in user.SavingAccounts)
            {
                if (userSavingAccount.AccountNumber == accountNumber)
                { 
                    userSavingAccount.WithdrawMoney(money);
                    found = true;
                }
            }
            foreach (var userCheckingAccount in user.CheckingAccounts)
            {
                if (userCheckingAccount.AccountNumber == accountNumber)
                {
                    userCheckingAccount.WithdrawMoney(money);
                    found = true;
                }
            }

            if (!found)
                throw new ArgumentException("This account doesn't exist");

            db.SaveChanges();
        }

        private static void Deposit(BankSystemContext db, string[] data)
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            string accountNumber = data[1];
            decimal money = decimal.Parse(data[2]);
            bool found = false;

            foreach (var userSavingAccount in user.SavingAccounts)
            {
                if (userSavingAccount.AccountNumber == accountNumber)
                {
                    userSavingAccount.DepositMoney(money);
                    found = true;
                }
            }
            foreach (var userCheckingAccount in user.CheckingAccounts)
            {
                if (userCheckingAccount.AccountNumber == accountNumber)
                {
                    userCheckingAccount.DepositMoney(money);
                    found = true;
                }
            }

            if (!found) 
                throw new ArgumentException("This account doesn't exist");

            db.SaveChanges();
        }

        private static void ListAccounts()
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            Console.WriteLine("Saving Accounts:");
            foreach (var userSavingAccount in user.SavingAccounts)
            {
                Console.WriteLine($"--{userSavingAccount.AccountNumber} {userSavingAccount.Balance}");
            }
            Console.WriteLine("Checking Accounts:");
            foreach (var userCheckingAccount in user.CheckingAccounts)
            {
                Console.WriteLine($"--{userCheckingAccount.AccountNumber} {userCheckingAccount.Balance}");
            }
        }

        private static void Add(BankSystemContext db, string[] data)
        {
            if (user == null)
                throw new ArgumentException("You need to login first.");

            string account = data[1];
            string accountNumber = RandomString().Trim();
            decimal balance = decimal.Parse(data[2]);
            decimal rateFee = decimal.Parse(data[3]);

            if (account.Equals("SavingsAccount"))
            {
                user.SavingAccounts.Add(new SavingAccount()
                {
                    AccountNumber = accountNumber,
                    Balance = balance,
                    InterestRate = rateFee
                });
            }
            else
            {
                user.CheckingAccounts.Add(new CheckingAccount()
                {
                    AccountNumber = accountNumber,
                    Balance = balance,
                    Fee = rateFee
                });
            }

            Console.WriteLine($"Succesfully added account with number {accountNumber}");
            db.SaveChanges();
        }

        private static void Logout()
        {
            if (user == null)
                throw new ArgumentException("Cannot log out. No user has logged in.");

            Console.WriteLine($"User {user.Name} successfully logged out");
            user = null;
        }

        private static void Login(BankSystemContext db, string[] data)
        {
            if (user != null)
                throw new ArgumentException("You need to Logout first!");

            string username = data[1];
            string password = data[2];

            var validUser = db.Users.FirstOrDefault(u => u.Name == username && u.Password == password);
            if (validUser != null)
            {
                user = validUser;
                Console.WriteLine($"Successfully logged in {user.Name}");
            }
            else
                throw new ArgumentException("Incorrect username / password");
        }

        private static void Register(BankSystemContext db, string[] data)
        {
            if (user != null)
                throw new ArgumentException("You need to Logout first!");

            string username = data[1];
            string password = data[2];
            string email = data[3];

            var newUser = new User
            {
                Name = username,
                Password = password,
                Email = email
            };
            db.Users.Add(newUser);
            Console.WriteLine(newUser.Name);
            Console.WriteLine(newUser.Password);
            Console.WriteLine(newUser.Email);
            Console.WriteLine(email);
            Console.WriteLine($"{newUser.Name} was registered in the system");

            db.SaveChanges();
        }

        private static string RandomString()
        {
            var text = " ";

            var charset = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            for (var i = 0; i < 10; i++)
                text += charset[rnd.Next(0, charset.Length)];

            return text;
        }
    }
}

//Register vl Tsepesh89 vlad@rom.ro
//Register vlad123 tspesh vlad@rom.ro
//Register vlad123 Tsepesh89 -v-@-rom.ro
//Register vlad123 Tsepesh89 vlad@rom.ro
//Logout
//Login vlad321 Tsepesh89
//Login vlad123 smallPussyCat
//Login vlad123 Tsepesh89
//Add SavingsAccount 1000 0.2
//Add CheckingAccount 100 4.20
//Deposit A8234JDG9M 10.42
//Withdraw A8234JDG9M 5
//Deposit PO8FHH34GM 200
//Withdraw PO8FHH34GM 45.2
//AddInterest A8234JDG9M
//DeductFee PO8FHH34GM
//ListAccounts
//Logout