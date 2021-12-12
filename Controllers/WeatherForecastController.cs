using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Distributed;

namespace Demo.DotNetWithRedis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet(Name = "GetWeatherForecast")]
        public ContentResult Get(
            [FromServices] IConfiguration config,
            [FromServices] IDistributedCache cache)
        {
            string jsonValue = cache.GetString("Weather");

            if (jsonValue == null)
            {
                using var conexao = new SqlConnection(config.GetConnectionString("SqlServer"));

                using var cmd = conexao.CreateCommand();
                cmd.CommandText =
                    "SELECT * FROM Weather ORDER BY City FOR JSON PATH, ROOT('Weather')";

                conexao.Open();
                jsonValue = (string)cmd.ExecuteScalar();
                conexao.Close();

                DistributedCacheEntryOptions opcoesCache =
                          new DistributedCacheEntryOptions();
                opcoesCache.SetAbsoluteExpiration(
                    TimeSpan.FromMinutes(1));

                cache.SetString("Weather", jsonValue, opcoesCache);
            }

            return Content(jsonValue, "application/json");
        }
    }
}