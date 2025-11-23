
namespace BankingSystemCLI
{

    public class User
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        private string _userPassword { get; set; }
        public User(string firstName, string lastName, string email, string password)
        {

            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this._userPassword = password;
        }
        public bool VerifyPassword(string attemptedPassword)
        {
            return attemptedPassword == this._userPassword;
        }

    }


    class Program
    {
        public static void Main(string[] args)
        {
            UserService service = new UserService();
            Console.WriteLine(@"Hello this are Options to use this System
            1. Creating a new User
            2. Login User
            3. Get All Users
            4. Exiting the progrm");

            bool isRunning = true;

            while (isRunning)

            {
                Console.WriteLine("Enter Your Choice here: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Creating an account");
                        Console.WriteLine("");

                        Console.Write("First name: ");
                        String firstName = Console.ReadLine();

                        Console.Write("Second Name: ");
                        String lastName = Console.ReadLine();

                        Console.Write("Email: ");
                        String email = Console.ReadLine();

                        Console.Write("Password: ");
                        String password = Console.ReadLine();

                        service.RegisterUser(firstName, lastName, email, password);
                        break;
                    
                    case "2":
                        Console.Write("Email: ");
                        string loginEmail = Console.ReadLine();

                        Console.Write("Password: ");
                        string loginPassword = Console.ReadLine();

                        bool loginSuccess = service.LoginUser(loginEmail, loginPassword);
                        break;

                    case "3":
                    var users = service.GetAllUsers();
                    Console.WriteLine("\nRegistered Users:");
                    foreach (var user in users)
                    {
                        Console.WriteLine($"- {user.FirstName} {user.LastName} ({user.Email})");
                    }
                    break;
                    case "4":
                        Console.WriteLine("Exiting the Program .............");
                        isRunning = false;
                        break;

                }
            }
        }
    }

}