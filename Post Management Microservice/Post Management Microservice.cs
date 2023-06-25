using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int UserId { get; set; }
}

public class PostService
{
    private readonly PostDbContext _dbContext;

    public PostService()
    {
        _dbContext = new PostDbContext();
        _dbContext.Database.EnsureCreated(); // Create the database if it doesn't exist
    }

    public void CreatePost(Post post)
    {
        _dbContext.Posts.Add(post);
        _dbContext.SaveChanges();
    }

    public bool DeletePost(int postId, int userId)
    {
        var post = _dbContext.Posts.FirstOrDefault(p => p.Id == postId && p.UserId == userId);
        if (post != null)
        {
            _dbContext.Posts.Remove(post);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

    public IQueryable<Post> GetAllPosts()
    {
        return _dbContext.Posts;
    }

    public IQueryable<Post> GetUserPosts(int userId)
    {
        return _dbContext.Posts.Where(p => p.UserId == userId);
    }
}

// Usage example
public class Program
{
    static void Main()
    {
        var postService = new PostService();

        // Create a new post
        var newPost = new Post
        {
            Title = "Hello World",
            Content = "This is my first post",
            UserId = 1
        };
        postService.CreatePost(newPost);

        // Get all posts
        var allPosts = postService.GetAllPosts();
        foreach (var post in allPosts)
        {
            Console.WriteLine("Title: " + post.Title + ", Content: " + post.Content);
        }

        // Get user posts
        var userPosts = postService.GetUserPosts(1);
        foreach (var post in userPosts)
        {
            Console.WriteLine("Title: " + post.Title + ", Content: " + post.Content);
        }

        // Delete a post
        bool isDeleted = postService.DeletePost(1, 1);
        Console.WriteLine("Post deleted: " + isDeleted);
    }
