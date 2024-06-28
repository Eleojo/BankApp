using BankApp.Classes;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.FileManagerServices
{
    public interface IFileManager
    {
        //Task SaveEntitiesAsync<T>(List<T> entities, string filePath);
        //Task<List<User>> LoadUsersAsync(string filePath);
        //Task<List<Account>> LoadAccountsAsync(string filePath);
        //Task<List<TransactionHistory>> LoadTransactionsAsync(string filePath);

        //List<User> LoadUsersAsync(string filePath);

        void WriteUsersToFile(List<User> users, string filepath);
        void WriteTransactionsToFile(List<TransactionHistory> transactions, string filepath);
        void WriteAccountsToFile(List<Account> accounts, string filepath);
        List<User> ReadUsersFromFile(string filePath);
        List<TransactionHistory> ReadTransactionsFromFile(string filePath);
        List<Account> ReadAccountsFromFile(string filePath);
    }
}
