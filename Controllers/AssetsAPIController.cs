using RivneOneLove.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace RivneOneLove.Controllers.Asset;

[Route("api/assets")]
[ApiController]
public sealed class AssetsController : Controller
{
    private readonly HttpClient _httpClient;

    public AssetsController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [HttpGet("{id}", Name = "GetAsset")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AssetData>>> GetAsset(string id)
    {
        try
        {
            var response = await _httpClient.GetAsync("https://api.coincap.io/v2/assets/" + id);
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<AssetDataResponse>(stringResult);

            return Ok(apiResponse);
        }
        catch (HttpRequestException e)
        {
            return BadRequest($"Error fetching data from CoinCap API: {e.Message}");
        }
    }
}