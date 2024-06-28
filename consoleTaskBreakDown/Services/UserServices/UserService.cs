using BankApp.Classes;
using BankApp.Services.FileManagerServices;
using BankApp.Services.UserServices;
using BankApp.Utilities;

public class UserService : IUserService
{
    private readonly string _usersFilePath = "Users.txt"; // Assuming file path is defined here
    private readonly string _accountsFilePath = "Accounts.txt"; // Assuming file path is defined here
    private readonly string _transactionsFilePath = "Transactions.txt";
    private readonly IFileManager _fileManager;

    public UserService(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public User RegisterUser()
    {
        try
        
        {
            // Prompt user for input
            string firstName = Validations.GetValidInput("Enter your FirstName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string lastName = Validations.GetValidInput("Enter your LastName:", Validations.IsValidName, "Name cannot be empty or contain digits. Please try again.");
            string email = Validations.GetValidInput("Enter your email:", Validations.IsValidEmail, "Incorrect email address. Please try again.");
            string password = Validations.GetValidInput("Enter your password:", Validations.IsValidPassword, "Password must contain at least 6 characters, one uppercase letter, one digit, and one special character. Please try again.");

            var users =  _fileManager.ReadUsersFromFile(_usersFilePath);
            //var transactions = _fileManager.ReadTransactionsFromFile(_transactionsFilePath);

            // Debugging: Print out loaded users
            Console.WriteLine("Loaded users:");
           

            // Check if user already exists
            var existingUser = users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            if (existingUser != null)
            {
                Console.WriteLine("User already exists. Registration failed.");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                return null; // Return null to indicate no new user registered
            }

            // Create new user object
            var newUser = new User
            {
                Id = Guid.NewGuid(), // Generate new unique Id
                FirstName = Validations.Capitalize(firstName),
                LastName = Validations.Capitalize(lastName),
                Email = email,
                Password = password
            };

            // Add new user to the list and save to file
            users.Add(newUser);
            _fileManager.WriteUsersToFile(users, _usersFilePath);

            Console.WriteLine("Congratulations! You have been registered successfully.");
            Console.WriteLine("Press any key to continue:");
            Console.ReadLine(); // Wait for user input before continuing

            return newUser; // Return the newly registered user
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred during user registration: {ex.Message}");
            // Log the exception or handle it according to your application's error handling strategy
            return null; // Return null to indicate registration failure
        }
    }
}
