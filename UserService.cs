using System.Collections.Generic;

namespace BankingSystemCLI
{
    public class UserService 
    {
        private static List<User> _users = new List<User>();

        public void RegisterUser(string firstName, string lastName, string email, string password)
        {
            try
            {
                var newUser = new User(firstName, lastName, email, password);
                
                _users.Add(newUser); 
                
                Console.WriteLine($"\nSuccessfully registered user: {firstName} {lastName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFailed to create a new User: {ex.Message}");
            }
        }

        public bool LoginUser(string email, string password)
        {
            var user = _users.FirstOrDefault(u => u.Email == email);

            if (user != null && user.VerifyPassword(password))
            {
                Console.WriteLine($"\nLogin successful. Welcome back, {user.FirstName}!");
                return true;
            }
            else
            {
                Console.WriteLine("\nLogin failed. Invalid email or password.");
                return false;
            }
        }

        
        public List<User> GetAllUsers()
        {
            return _users;
        }
    }
}