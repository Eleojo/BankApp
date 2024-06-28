using BankApp.Classes;
using BankApp.Database;
using BankApp.Models;
using BankApp.Services.FileManagerServices;
using ConsoleTableExt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.TransactionHistoryServices
{
    internal class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly IFileManager _fileManager;

        public TransactionHistoryService(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public void DisplayTransactionHistory(User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();
            //var users = await FileManager.LoadEntitiesAsync<User>("Accounts.txt");

            var users =  _fileManager.ReadUsersFromFile("Users.txt");
            var accounts = _fileManager.ReadAccountsFromFile("Accounts.txt");
            var transactions = _fileManager.ReadTransactionsFromFile("Transactions.txt");

            //var foundAcount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));
            //var userTransactions = transactions.Where(t => t.AccountId == foundAcount.Id).ToList();

            //if (transactions.Count == 0)
            //{
            //    Console.WriteLine("No transactions found.");
            //    return;
            //}

            //ConsoleTableBuilder
            //    .From(transactions.Select(t => new
            //    {
            //        t.Id,
            //        t.TransactionType,
            //        t.Amount,
            //        t.Sender,
            //        t.Description,
            //        t.Timestamp
            //    }).ToList())
            //    .WithFormat(ConsoleTableBuilderFormat.Alternative)
            //    .ExportAndWriteLine();



            var joinedData = from user in users
                             where user.Id == sessionUser.Id
                             join transaction in transactions on user.Id equals transaction.AccountId
                             select new
                             
                             {
                                 UserId = user.Id,
                                 UserName = $"{user.FirstName} {user.LastName}",
                                 TransactionId = transaction.Id,
                                 TransactionType = transaction.TransactionType,
                                 Amount = transaction.Amount,
                                 Sender = transaction.Sender,
                                 Description = transaction.Description,
                                 Timestamp = transaction.Timestamp
                             };

            // Formatting and displaying using ConsoleTableBuilder
            ConsoleTableBuilder
                .From(joinedData.ToList())
                .WithFormat(ConsoleTableBuilderFormat.Alternative)
                .ExportAndWriteLine();
        }

    }

}

