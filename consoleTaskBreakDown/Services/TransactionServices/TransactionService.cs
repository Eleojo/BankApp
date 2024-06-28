using BankApp.Classes;
using BankApp.Database;
using BankApp.Models;
using BankApp.Services.AccountServices;
using BankApp.Services.FileManagerServices;
using BankApp.Services.TransactionHistoryServices;
using BankApp.Utilities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankApp.Services.TransactionServices
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountService _accountService;
        private readonly IFileManager _fileManager;
        private readonly string _usersFilePath = "Users.txt";
        private readonly string _accountsFilePath = "Accounts.txt";
        private readonly string _transactionsFilePath = "Transactions.txt";


        private List<User> _users;
        private List<Account> _accounts;
        private List<TransactionHistory> _transactions;

        public TransactionService(IAccountService accountService, IFileManager fileManager)
        {
            _accountService = accountService;
            _fileManager = fileManager;
            InitializeData();
        }


        

        private void InitializeData()
        {
            _users = _fileManager.ReadUsersFromFile(_usersFilePath);
            _accounts = _fileManager.ReadAccountsFromFile(_accountsFilePath);
            _transactions = _fileManager.ReadTransactionsFromFile(_transactionsFilePath);
        }

        public void WithdrawMoney( User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();
            var users = _fileManager.ReadUsersFromFile("Users.txt");

            //using (BankAppDbContext db = new BankAppDbContext())
            //{

            var accounts = _fileManager.ReadAccountsFromFile("Accounts.txt");
            var transactions = _fileManager.ReadTransactionsFromFile("Transactions.txt");


            // var accounts = await db.GetAllEntities<Account>();

            // Find the user with the specified account number
            var foundAccount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));

            // Prompt for the amount to withdraw
            decimal withdrawAmount = Validations.GetValidInput("Enter amount to withdraw:", Validations.IsValidAmount, "Use Numbers.Please try again.");

            // Check if the account was found
            if (foundAccount != null)
            {
                // Check if there are sufficient funds in the account
                if (withdrawAmount > foundAccount.accountBalance)
                {
                    Console.WriteLine("Insufficient Funds");
                }
                else
                {
                    // Perform the withdrawal
                    foundAccount.accountBalance -= withdrawAmount;

                    // Save updated  accounts and transactions list  back to file
                    _fileManager.WriteAccountsToFile(accounts,"Accounts.txt");
                    var recordedTransaction = new TransactionHistory(Guid.NewGuid(), foundAccount.userId, "Credit", withdrawAmount, sessionUser.FirstName, "Self");
                    transactions.Add(recordedTransaction);
                    _fileManager.WriteTransactionsToFile(transactions, "Transactions.txt");


                    Console.WriteLine($"Success !! You have withdrawn {withdrawAmount} and your new balance is {foundAccount.accountBalance} ");
                    _accountService.DisplayAccountInfo(sessionUser);
                    Console.WriteLine("Press Enter key to return to main menu");
                }
            }
            else
            {
                Console.WriteLine("Sorry, you do not have an account with us.");
            }
        }


        public void DepositMoney(User sessionUser)
        {
            //BankApp_DbContext db = new BankApp_DbContext();
            //List<User> users = db.GetAllEntities<User>();

            //var users = _fileManager.ReadUsersFromFile("Users.txt");

            var accounts = _fileManager.ReadAccountsFromFile("Accounts.txt");
            var transactions = _fileManager.ReadTransactionsFromFile("Transactions.txt");
            //var accounts = await db.GetAllEntities<User>();
            //var accounts =  _fileManager.ReadAccountsFromFile("Accounts.txt");
            //var transactions = _fileManager.ReadTransactionsFromFile("Transactions.txt");


            decimal depositAmount = Validations.GetValidInput("Enter amount to deposit:", Validations.IsValidAmount, "Use Numbers.Please try again.");


            // Find the account associated with the user
            var foundAcount = accounts.FirstOrDefault(account => account.userId.Equals(sessionUser.Id));
            // Check if the account was found

            if (foundAcount != null)
            {
                // Perform the deposit
                //foundAccount.accountBa += depositAmount;
                foundAcount.accountBalance += depositAmount;
                foreach(var account in accounts)
                {
                    if (foundAcount.Id.Equals(account.Id))
                    {
                        Console.WriteLine(account.accountBalance);

                    }

                }
                _fileManager.WriteAccountsToFile(accounts, "Accounts.txt");
                var recordedTransaction = new TransactionHistory(Guid.NewGuid(), foundAcount.userId, "Credit", depositAmount, sessionUser.FirstName, "Self");
                transactions.Add(recordedTransaction);
                _fileManager.WriteTransactionsToFile(transactions, "Transactions.txt");

                Console.WriteLine($"Success!! You have deposited {depositAmount} and your new balance is {foundAcount.accountBalance}.");
                _accountService.DisplayAccountInfo(sessionUser);
                Console.WriteLine("Press Enter key to return to main menu");
            }
            else
            {
                Console.WriteLine("Sorry, you do not have an account with us.");
            }

        }


        

        public void Transfer(User sessionUser)
        {
            var users =  _fileManager.ReadUsersFromFile("Users.txt");

            var accounts =  _fileManager.ReadAccountsFromFile("Accounts.txt");
            var transactions =  _fileManager.ReadTransactionsFromFile("Transactions.txt");


        //string amount = Console.ReadLine();
            decimal transferAmount = Validations.GetValidInput("Enter amount to Transfer: ", Validations.IsValidAmount, "Use Numbers. Please try again.");

            Console.WriteLine("Enter receiver accNumber:");
            string receiverAccountNumber = Console.ReadLine();

            // Find sender and receiver user IDs using LINQ
            var senderAccount = accounts.SingleOrDefault(account => account.userId.Equals(sessionUser.Id));
            var receiverAccount = accounts.SingleOrDefault(account => account.accountNumber.Equals(receiverAccountNumber));
            User receiver = null;

            // Check if both users were found
            if (senderAccount == null || receiverAccount == null)
            {

                Console.WriteLine($"One or both users not found. Please make sure both sender and receiver are registered.");
                return;
            }
            else
            {
                receiver = users.SingleOrDefault(user => user.Id.Equals(receiverAccount.userId));
            }
            // Check if senderAccount balance less than transfer amount
            if (senderAccount.accountBalance < transferAmount)
            {
                Console.WriteLine("Insufficient funds in the sender's account.");
                return;
            }

            // Perform the transfer
            senderAccount.accountBalance -= transferAmount;
            receiverAccount.accountBalance += transferAmount;

            // Update changes to database
            //db.UpdateEntities(accounts);
            _fileManager.WriteAccountsToFile(accounts, "Accounts.txt");
            Console.WriteLine("Enter description: ");
            string description = Console.ReadLine();

            // Create Trasaction History
            TransactionHistory senderTransaction = new TransactionHistory(Guid.NewGuid(), senderAccount.userId, "Debit", transferAmount,receiver.FirstName,description);
            TransactionHistory receiverTransaction = new TransactionHistory(Guid.NewGuid(), receiverAccount.userId, "Credit", transferAmount, sessionUser.FirstName, description);

            transactions.Add(senderTransaction);
            transactions.Add(receiverTransaction);
            _fileManager.WriteTransactionsToFile(transactions, "Transactions.txt");

            _accountService.DisplayAccountInfo(sessionUser);

            Console.WriteLine($"Transfer successful! {transferAmount} has been transferred to {receiver.FirstName}.");
            Console.WriteLine($"Sender's new balance: {senderAccount.accountBalance}");
            Console.WriteLine($"Receiver's new balance: {receiverAccount.accountBalance}");
        }

    }
}
