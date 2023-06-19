// UserController.cs
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public UserController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // GET api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUserById(int id)
    {
        HttpResponseMessage response = await _httpClient.GetAsync($"http://postmanagement/api/posts/user/{id}");

        if (response.IsSuccessStatusCode)
        {
            var user = await response.Content.ReadAsAsync<User>();
            return Ok(user);
        }
        else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return NotFound();
        }

        return StatusCode((int)response.StatusCode);
    }

    // Other endpoints for creating users, authenticating users, managing user details, etc.
}
