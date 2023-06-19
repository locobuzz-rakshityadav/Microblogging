// FollowController.cs
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[Route("api/follow")]
[ApiController]
public class FollowController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public FollowController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // POST api/follow
    [HttpPost]
    public async Task<ActionResult> FollowUser(FollowRequest request)
    {
        HttpResponseMessage
