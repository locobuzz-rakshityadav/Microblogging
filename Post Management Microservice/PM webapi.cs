// PostController.cs
using Microsoft.AspNetCore.Mvc;

[Route("api/posts")]
[ApiController]
public class PostController : ControllerBase
{
    // GET api/posts/user/{userId}
    [HttpGet("user/{userId}")]
    public ActionResult<Post> GetPostsByUserId(int userId)
    {
        // Retrieve posts from the database using LINQ
        var posts = _dbContext.Posts.Where(p => p.UserId == userId).ToList();
        return Ok(posts);
    }

    // Other endpoints for creating posts, deleting posts, viewing posts, etc.
}
