using Microsoft.AspNetCore.Mvc;

namespace FileHosting.Storage.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    public WeatherForecastController()
    {
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public int Get()
    {
        return 1;
    }
}