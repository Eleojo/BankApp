using BankApp.Classes;
using BankApp.Database;
using BankApp.Models;
using BankApp.Services.AccountServices;
using BankApp.Services.AuthServices;
using BankApp.Services.DashBoardServices;
using BankApp.Services.FileManagerServices;
using BankApp.Services.TransactionHistoryServices;
using BankApp.Services.TransactionServices;
using BankApp.Services.UserServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BankApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddScoped<IFileManager, FileManager>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDashBoardService, DashBoardService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();



            services.AddSingleton<Run>();
            var serviceProvider = services.BuildServiceProvider();
            var run = serviceProvider.GetRequiredService<Run>();

            run.StartApp();

            //List<User> transactions = new List<User>();
            //var transaction = new User("Ezine","Micheal","michealezine@gmail.com","12!@Qq");
            //transactions.Add(transaction);
            //var fileManager = new FileManager();
            //fileManager.WriteUsersToFile(transactions, "Users.txt");
        }
    }
}

