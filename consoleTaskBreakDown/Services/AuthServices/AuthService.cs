using BankApp.Classes;
using BankApp.Database;
using BankApp.Services.FileManagerServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services.AuthServices
{
    internal class AuthService : IAuthService
    {
        private readonly IFileManager _fileManager;

        public AuthService(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public string ReadPassword()
        {
            string password = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && password.Length > 0)
                {
                    Console.Write("\b \b");
                    password = password[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    password += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return password;
        }

        //public async Task<bool> AuthenticateUser(string email, string password)
        //{
        //    try
        //    {
        //        //var fileManager = new FileManager();

        //        var users = await _fileManager.LoadEntitiesAsync<User>("Users.txt");

        //        var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

        //        if (user != null)
        //        {
        //            UserSession.LoggedInUser = user;
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it appropriately
        //        Console.WriteLine($"An error occurred during authentication: {ex.Message}");
        //        return false; // Return false indicating authentication failure
        //    }
        //}





        public bool AuthenticateUser(string email, string password)
        {
            try
            {
                // Check if _fileManager is null
                if (_fileManager == null)
                {
                    Console.WriteLine("FileManager is null.");
                    return false;
                }

                Console.WriteLine("Attempting to load users from Users.txt...");

                // Attempt to load users from file
                var users =  _fileManager.ReadUsersFromFile("Users.txt");

                // Check if users list is null
                if (users == null)
                {
                    Console.WriteLine("No users found in Users.txt.");
                    return false;
                }

                Console.WriteLine($"Loaded {users.Count()} users from Users.txt.");

                // Check if email and password are valid
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Email or password is null or empty.");
                    return false;
                }

                // Find the user with the matching email and password
                var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                    Console.WriteLine("User authenticated successfully.");
                    UserSession.LoggedInUser = user;
                    return true;
                }
                else
                {
                    Console.WriteLine("User not found or incorrect password.");
                    return false;
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"File not found: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            catch (JsonSerializationException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"JSON reading error: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception with a detailed message
                Console.WriteLine($"An error occurred during authentication: {ex.Message}");
                Console.WriteLine(ex.StackTrace); // Print the stack trace for debugging purposes
                return false; // Return false indicating authentication failure
            }
        }



    }
}
