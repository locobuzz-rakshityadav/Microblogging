using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// User entity class
public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
}

// DbContext class for interacting with the database
public class UserDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Your_SQL_Server_Connection_String");
    }
}

// User service class for handling user-related operations
public class UserService
{
    private readonly UserDbContext _dbContext;

    public UserService()
    {
        _dbContext = new UserDbContext();
        _dbContext.Database.EnsureCreated(); // Create the database if it doesn't exist
    }

    public void CreateUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public bool AuthenticateUser(string username, string password)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        return user != null;
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.Find(id);
    }

    public void UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _dbContext.Users.Find(id);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}

// Usage example
public class Program
{
    static void Main()
    {
        var userService = new UserService();

        // Create a new user
        var newUser = new User
        {
            Username = "john_doe",
            Password = "password123",
            Email = "john.doe@example.com"
        };
        userService.CreateUser(newUser);

        // Authenticate a user
        bool isAuthenticated = userService.AuthenticateUser("john_doe", "password123");
        Console.WriteLine("User authenticated: " + isAuthenticated);

        // Get a user by ID
        var user = userService.GetUserById(1);
        Console.WriteLine("Username: " + user.Username + ", Email: " + user.Email);

        // Update a user
        user.Email = "newemail@example.com";
        userService.UpdateUser(user);
        Console.WriteLine("User updated");

        // Delete a user
        userService.DeleteUser(1);
        Console.WriteLine("User deleted");
    }
}
