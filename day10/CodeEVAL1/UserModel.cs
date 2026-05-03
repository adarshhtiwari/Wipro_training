namespace Eval1;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }

    // Register with error handling
    public void Register(string username, string password)
    {
        try
        {
            if (username == "")
            {
                Console.WriteLine("Username cannot be empty!");
                Loggers.LogError("Registration failed - Username was empty.");
                return;
            }

            if (password == "")
            {
                Console.WriteLine("Password cannot be empty!");
                Loggers.LogError("Registration failed - Password was empty.");
                return;
            }

            Username = username;
            Password = password;

            Console.WriteLine("User registered successfully!");
            Loggers.Log("User registered: " + Username);
        }
        catch (Exception ex)
        {
            Loggers.LogError("Registration error: " + ex.Message);
            Console.WriteLine("Something went wrong during registration.");
        }
    }

    // Login with error handling
    public bool Authenticate(string username, string password)
    {
        try
        {
            if (username == "")
            {
                Console.WriteLine("Username cannot be empty!");
                Loggers.LogError("Login failed - Username was empty.");
                return false;
            }

            if (password == "")
            {
                Console.WriteLine("Password cannot be empty!");
                Loggers.LogError("Login failed - Password was empty.");
                return false;
            }

            if (Username == username && Password == password)
            {
                Console.WriteLine("Login successful!");
                Loggers.Log("Login successful for: " + Username);
                return true;
            }
            else
            {
                Console.WriteLine("Invalid username or password!");
                Loggers.LogError("Login failed - Invalid credentials.");
                return false;
            }
        }
        catch (Exception ex)
        {
            Loggers.LogError("Login error: " + ex.Message);
            Console.WriteLine("Something went wrong during login.");
            return false;
        }
    }
}