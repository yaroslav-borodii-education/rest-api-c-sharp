using RivneOneLove.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Web;

namespace RivneOneLove.Controllers.Asset;

[Route("api/history")]
[ApiController]
public sealed class CoinCapHistoryController : Controller
{
    private readonly HttpClient _httpClient;

    public CoinCapHistoryController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    [HttpGet("{id}", Name = "GetHistory")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<HistoryDataResponse>> GetHistory(string id, [FromQuery] string interval)
    {
        try
        {
            var builder = new UriBuilder("https://api.coincap.io/v2/assets/" + id + "/history");
            var query = HttpUtility.ParseQueryString(builder.Query);
            query["interval"] = interval;
            builder.Query = query.ToString();
            string url = builder.ToString();

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var stringResult = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<HistoryDataResponse>(stringResult);

            return Ok(apiResponse);
        }
        catch (HttpRequestException e)
        {
            return BadRequest($"Error fetching data from CoinCap API: {e.Message}");
        }
    }
}