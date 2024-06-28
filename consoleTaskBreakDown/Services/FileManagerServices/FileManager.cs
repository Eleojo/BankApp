using BankApp.Classes;
using BankApp.Models;
using BankApp.Services.AccountServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.FileManagerServices
{
    internal class FileManager : IFileManager
    {
        public void WriteUsersToFile(List<User> users, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (var user in users)
                {
                    writer.WriteLine($"{user.FirstName},{user.LastName},{user.Id},{user.Email},{user.Password}");
                }
            }
        }

        public void WriteTransactionsToFile(List<TransactionHistory> transactions, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (var transaction in transactions)
                {
                    writer.WriteLine($"{transaction.Id},{transaction.AccountId},{transaction.TransactionType},{transaction.Amount},{transaction.Sender},{transaction.Description},{transaction.Timestamp}");
                }
            }
        }

        public void WriteAccountsToFile(List<Account> accounts, string filepath)
        {
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                foreach (var account in accounts)
                {
                    writer.WriteLine($"{account.Id},{account.userId},{account.accountNumber},{account.accountType},{account.accountBalance}");
                }
            }
        }

        //public List<User> ReadUsersFromFile()
        //{
        //    List<User> users = new List<User>();
        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        string line;
        //        while ((line = reader.ReadLine()) != null)
        //        {
        //            var parts = line.Split(',');
        //            if (parts.Length == 5)
        //            {
        //                users.Add(new User
        //                {
        //                    Id = Guid.Parse(parts[0]),
        //                    FirstName = parts[1],
        //                    LastName = parts[2],
        //                    Email = parts[3],
        //                    Password = parts[4]
        //                });
        //            }
        //        }
        //    }
        //    return users;
        //}



        public List<User> ReadUsersFromFile(string filePath)
        {
            List<User> users = new List<User>();
            //List<Account> accounts = new List<Account>();
            //List<TransactionHistory> transactions = new List<TransactionHistory>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        users.Add(new User
                        {
                            

                            Id = Guid.Parse(parts[2]),
                            FirstName = parts[0],
                            LastName = parts[1],
                            Email = parts[3],
                            Password = parts[4]
                        });
                    }
                   
                    else
                    {
                        Console.WriteLine($"Invalid line: {line}");
                    }
                }
            }

            return users;
        }



        public List<Account> ReadAccountsFromFile(string filePath)
        {
            //List<User> users = new List<User>();
            List<Account> accounts = new List<Account>();
            //List<TransactionHistory> transactions = new List<TransactionHistory>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {

                        accounts.Add(new Account
                        {
                       
                            Id = Guid.Parse(parts[0]),
                            userId = Guid.Parse(parts[1]),
                            accountNumber = parts[2],
                            accountType = parts[3],
                            accountBalance = decimal.Parse(parts[4]),
                            //this.note = note;
                        });
                    }

                    else
                    {
                        Console.WriteLine($"Invalid line: {line}");
                    }
                }
            }

            return accounts;
        }


        public List<TransactionHistory> ReadTransactionsFromFile(string filePath)
        {
            //List<User> users = new List<User>();
            //List<Account> accounts = new List<Account>();
            List<TransactionHistory> transactions = new List<TransactionHistory>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 7)
                    {

                        transactions.Add(new TransactionHistory
                        {

                            Id = Guid.Parse(parts[0]),
                            AccountId = Guid.Parse(parts[1]),
                            TransactionType = parts[2],
                            Amount = decimal.Parse(parts[3]),
                            Sender = parts[4],
                            Description = parts[5],
                            Timestamp = DateTime.Parse(parts[6]),
                        });
                    }

                    else
                    {
                        Console.WriteLine($"Invalid line: {line}");
                    }
                }
            }

            return transactions;
        }


    }
}

    // Example usage:
    //    var users = new List<User>
    //{
    //    new User { FirstName = "John", LastName = "Doe", Id = 1, Email = "john@example.com", Password = "secret" },
    //    new User { FirstName = "Jane", LastName = "Smith", Id = 2, Email = "jane@example.com", Password = "password" }
    //};

    //    var fileHandler = new FileHandler("users.txt");
    //    fileHandler.WriteUsersToFile(users);

    //var readUsers = fileHandler.ReadUsersFromFile();
    //foreach (var user in readUsers)
    //{
    //    Console.WriteLine($"User: {user.FirstName} {user.LastName}, Email: {user.Email}");
    //}



    //public List<User> WriteUsersToFile(string filePath)
    //{


    //    List<User> customers = new List<User>();

    //    using (StreamReader reader = new StreamReader(filePath))

    //    {

    //        string line;

    //        while ((line = reader.ReadLine()) != null)

    //        {

    //            if (!string.IsNullOrWhiteSpace(line))

    //            {

    //                string[] fields = line.Split('|');

    //                if (fields.Length >= 4)

    //                {

    //                    string firstName = fields[1].Trim();

    //                    string lastName = fields[2].Trim();

    //                    string email = fields[3].Trim();

    //                    string password = fields[4].Trim();

    //                    User customer = new User(firstName, lastName, email, password);

    //                    customers.Add(customer);

    //                }

    //            }

    //        }

    //    }

    //    return customers;

    //}




    //public List<Account> LoadAccountsAsync(string filePath)
    //{

    //    List<Account> accounts = new List<Account>();

    //    using (StreamReader reader = new StreamReader(filePath))

    //    {

    //        string line;

    //        while ((line = reader.ReadLine()) != null)

    //        {

    //            if (!string.IsNullOrWhiteSpace(line))

    //            {

    //                string[] fields = line.Split('|');

    //                if (fields.Length >= 4)

    //                {


    //                    Guid userId = fields[1].Trim();

    //                    string lastName = fields[2].Trim();

    //                    string email = fields[3].Trim();

    //                    string password = fields[4].Trim();

    //                    User customer = new User(firstName, lastName, email, password);

    //                    customers.Add(customer);

    //                    this.userId = userId;
    //                    this.accountNumber = accountNumber;
    //                    this.accountType = accountType;
    //                    this.accountBalance = accountBalance;

    //                }

    //            }

    //        }

    //    }

    //    return customers;
    //}




    //public List<User>LoadTransactionsAsync(string filePath)
    //{

    //    List<User> customers = new List<User>();

    //    using (StreamReader reader = new StreamReader(filePath))

    //    {

    //        string line;

    //        while ((line = reader.ReadLine()) != null)

    //        {

    //            if (!string.IsNullOrWhiteSpace(line))

    //            {

    //                string[] fields = line.Split('|');

    //                if (fields.Length >= 4)

    //                {

    //                    string firstName = fields[1].Trim();

    //                    string lastName = fields[2].Trim();

    //                    string email = fields[3].Trim();

    //                    string password = fields[4].Trim();

    //                    User customer = new User(firstName, lastName, email, password);

    //                    customers.Add(customer);

    //                }

    //            }

    //        }

    //    }

    //    return customers;

    //}









    //public async Task ReadUsersFromFile<T>(List<T> entities, string filePath)
    //{
    //    var jsonData = JsonConvert.SerializeObject(entities, Formatting.Indented);
    //    await File.WriteAllTextAsync(filePath, jsonData);
    //}
    //public async Task<List<T>> LoadEntitiesAsync<T>(string filePath)
    //{
    //    if (!File.Exists(filePath))
    //    {
    //        throw new FileNotFoundException($"File not found: {filePath}");
    //    }

    //    var jsonData = await File.ReadAllTextAsync(filePath, Encoding.UTF8);
    //    return  JsonConvert.DeserializeObject<List<T>>(jsonData);


    //}


    //public async Task SaveEntitiesAsync<T>(List<T> entities, string filePath)
    //        {
    //            if (typeof(T) == typeof(User))
    //            {
    //                var users = entities.Cast<User>();
    //                var lines = new List<string> { "Id,FirstName,LastName,Email,Password" };

    //                // Write each user to a line
    //                lines.AddRange(users.Select(u => $"{u.Id},{u.FirstName},{u.LastName},{u.Email},{u.Password}"));

    //                // Write all lines to the file
    //                await File.WriteAllLinesAsync(filePath, lines);
    //            }
    //            else
    //            {
    //                throw new NotSupportedException($"The type {typeof(T)} is not supported by SaveEntitiesAsync.");
    //            }
    //        }






    //public async Task<List<T>> LoadUsersAsync<T>(string filePath)
    //{

    // Read existing data from the file

    //var jsonData = await File.ReadAllTextAsync(filePath);
    // return JsonConvert.DeserializeObject<List<T>>(jsonData);

    // Re-throw the exception or handle it as appropriate

    //if (!File.Exists(filePath))
    //{
    // Create an empty file if it doesn't exist
    //File.Create(filePath).Close();

    //// Check if the file is empty after creation
    //if (new FileInfo(filePath).Length == 0)
    //{
    //    if (typeof(T) == typeof(User))
    //    {
    //        Console.WriteLine("You are our first user! Proceed to register:");

    //        // Prompt user for registration details
    //        string firstName = Validations.GetValidInput("Enter your FirstName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
    //        string lastName = Validations.GetValidInput("Enter your LastName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
    //        string email = Validations.GetValidInput("Enter your email:", Validations.IsValidEmail, "Incorrect email address. Please try again.");
    //        string password = Validations.GetValidInput("Enter your password:", Validations.IsValidPassword, "Password must contain at least 6 characters, one uppercase letter, one digit, and one special character. Please try again.");

    //        // Create a new user object
    //        var firstUser = new User(Validations.Capitalize(firstName), Validations.Capitalize(lastName), email, password);

    //        // Save the first user to the file
    //        var firstList = new List<User> { firstUser };
    //        await SaveEntitiesAsync(firstList, filePath);

    //        // Return the list containing the first user, cast to List<T>
    //        return firstList.Cast<T>().ToList();
    //    }
    //    else if (typeof(T) == typeof(Account))
    //    {
    //        // You can handle the account creation logic here
    //        var firstList = await LoadEntitiesAsync<User>("Users.txt");
    //        Guid userId = firstList[0].Id;
    //        string accountType = Validations.GetValidInput("Enter your preffered account type: Press 1 for Current and 2 for Savings:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
    //        decimal accountBalance = 0;
    //        //string accountNumber = await _accountService.GenerateRandomAccountNumber();
    //        Random random = new Random();
    //        string accountNumber = random.Next(1000000000, 1999999999).ToString();

    //        var firstAccount = new Account(userId,accountBalance,accountNumber,accountType);
    //        var firstAccountList = new List<Account> { firstAccount };
    //        await SaveEntitiesAsync(firstList, filePath);

    //public async Task<List<User>> LoadUsersAsync(string filePath)
    //{
    //    if (!File.Exists(filePath))
    //    {
    //        throw new FileNotFoundException($"File not found: {filePath}");
    //    }

    //    var lines = await File.ReadAllLinesAsync(filePath);
    //    var users = new List<User>();

    //    // Debugging: Print out number of lines read
    //    Console.WriteLine($"Number of lines read: {lines.Length}");

    //    // Skip the header line and iterate through each line
    //    foreach (var line in lines.Skip(1))
    //    {
    //        // Debugging: Print the current line
    //        Console.WriteLine($"Processing line: {line}");

    //        // Skip empty lines
    //        if (string.IsNullOrWhiteSpace(line))
    //        {
    //            Console.WriteLine("Skipping empty line");
    //            continue;
    //        }

    //        var parts = line.Split(',');

    //        // Debugging: Print number of parts found
    //        Console.WriteLine($"Number of parts found: {parts.Length}");

    //        if (parts.Length == 5)
    //        {
    //            try
    //            {
    //                var user = new User
    //                {
    //                    Id = Guid.Parse(parts[0]),
    //                    FirstName = parts[1].Trim(),
    //                    LastName = parts[2].Trim(),
    //                    Email = parts[3].Trim(),
    //                    Password = parts[4].Trim()
    //                };

    //                // Debugging: Print user details
    //                Console.WriteLine($"Adding user: {user.FirstName} {user.LastName}, Email: {user.Email}");

    //                users.Add(user);
    //            }
    //            catch (Exception ex)
    //            {
    //                // Log error but continue processing remaining lines
    //                Console.WriteLine($"Error processing line: {line}");
    //                Console.WriteLine($"Exception: {ex.Message}");
    //            }
    //        }
    //        else
    //        {
    //            // Debugging: Print warning for unexpected format
    //            Console.WriteLine($"Unexpected line format: {line}");
    //        }
    //    }

    //    return users;
    //}


    // public async Task<List<Account>> LoadAccountsAsync(string filePath)
    // {
    //     if (!File.Exists(filePath))
    //     {
    //         throw new FileNotFoundException($"File not found: {filePath}");
    //     }

    //     var lines = await File.ReadAllLinesAsync(filePath);
    //     var accounts = new List<Account>();

    //     // Skip the header line
    //     foreach (var line in lines.Skip(1))
    //     {
    //         var parts = line.Split(',');

    //         if (parts.Length == 4)
    //         {
    //             accounts.Add(new Account
    //             {
    //                 Id = Guid.Parse(parts[0]),
    //                 userId = Guid.Parse(parts[1]),
    //                 accountNumber = parts[2],
    //                 accountType = parts[3],
    //                 accountBalance = decimal.Parse(parts[4])
    //             });
    //         }
    //     }

    //     return accounts;
    // }
    //public async Task<List<TransactionHistory>> LoadTransactionsAsync(string filePath)
    // {
    //     if (!File.Exists(filePath))
    //     {
    //         throw new FileNotFoundException($"File not found: {filePath}");
    //     }

    //     var lines = await File.ReadAllLinesAsync(filePath);
    //     var transactions = new List<TransactionHistory>();

    //     // Skip the header line if there is one
    //     foreach (var line in lines.Skip(1))
    //     {
    //         var parts = line.Split(',');

    //         if (parts.Length == 5)
    //         {
    //             var newTransaction = new TransactionHistory(Guid.Parse(parts[0]), parts[1], decimal.Parse(parts[2]), parts[3], parts[4]);

    //             transactions.Add(newTransaction);
    //         }
    //     }

    //     return transactions;
    // }












    


