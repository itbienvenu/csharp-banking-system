using System;
using System.Linq;



namespace BANKINGSYSTEMCLI
{
    class Result<T>
    {
        public bool isSuccess {get; set; }
        public T Data {get; set;} 

        public string Message {get; set; }

        public static Result<T> Success(T data, string msg) => new Result<T> { isSuccess = true, Data = data, Message = msg};
        public static Result<T> Failure(string msg) => new Result<T> { isSuccess = false, Message = msg };
    }

    public class UserEntity
    {
        public string Names{get; set;}
        public string Email{get; set;}

        public long AccountNUmber {get; set;}

        public string Password {get; set;}
    }

    public class UserDto
    {
        public string Names {get; set;}
        public string Email {get; set; }
    }

    public class LoginResponse
    {
        public string Token {get; set;}

        public UserDto UserData {get; set; }
    }

    class UserService
    {

        private readonly List<UserEntity> _userDB = new List<UserEntity>();
        
        public Result<UserDto> Register(string names, string email, string password)
        {
            if(string.IsNullOrEmpty(email))
            {
                return Result<UserDto>.Failure("Email is required");
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            long userAccountNUmber =  Random.Shared.NextInt64(111111111111L, 999999999999L);

            var newUser = new UserEntity {Names = names, Email = email, Password = hashedPassword, AccountNUmber = userAccountNUmber};
            _userDB.Add(newUser);

            var response = new UserDto {Names = newUser.Names, Email = newUser.Email};

            return Result<UserDto>.Success(response, "User created well");
        }

        public Result<LoginResponse> Login(string email, string password)
        {
            if(string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                return Result<LoginResponse>.Failure("Email or password are required");
            }

            foreach(UserEntity user in _userDB)
            {
                if(BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    if(user.Email == email)
                    {
                        var response = new LoginResponse { UserData = new UserDto { Names = user.Names, Email = user.Email } };
                        return Result<LoginResponse>.Success(response, "Login successful");
                    }
                    return Result<LoginResponse>.Failure("Invalid email or password");
                }
            }

            return Result<LoginResponse>.Failure("Invalid email or password");
        }


        
    }

    // Testing the API

    public class Program
    {
        public static void Main(string[] args)
        {
            var userService = new UserService();
            bool isRunnig = true;


            while(isRunnig)
            {
                Console.WriteLine("Choose from the options here");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");

                int option = int.Parse(Console.ReadLine());
                switch(option)
                {
                    case 1:
                        Console.WriteLine("Enter your names");
                        string names = Console.ReadLine();

                        Console.WriteLine("Enter your email");
                        string email = Console.ReadLine();  

                        Console.WriteLine("Enter your password");
                        string password = Console.ReadLine();

                        var result = userService.Register(names, email, password);
                        Console.WriteLine(result.Message);
                        break;
                    case 2:
                        Console.WriteLine("Enter your email");
                        string loginEmail = Console.ReadLine();

                        Console.WriteLine("Passowrd");
                        string loginPassword = Console.ReadLine();

                        var res = userService.Login(loginEmail, loginPassword);
                        Console.WriteLine(res.Message, res.Data);
                        break;
                    case 3:
                        isRunnig = false;
                        break;

                }

            }
        }
    }
}



