// FeedController.cs
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/feed")]
[ApiController]
public class FeedController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public FeedController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // GET api/feed/{userId}
    [HttpGet("{userId}")]
    public async Task<ActionResult<Feed>> GetFeedByUserId(int userId)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"http://usermanagement/api/users/{userId}");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadAsAsync<User>();

            // Generate personalized feed based on the user's followers using LINQ and other logic

            return Ok(feed);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return StatusCode((int)response.StatusCode);
    }
}
