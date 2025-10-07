using Articles.AppServices.Contexts.Files.Services;
using Articles.Contracts.Errors;
using Articles.Contracts.Files;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Articles.Hosts.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
    public class CategoryController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        private readonly IDistributedCache _distributedCache;

        public CategoryController(IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpGet("now")]
        public IActionResult GetNow()
        {
            return Ok(DateTime.UtcNow);
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 17)]
        [HttpGet("now/cahsed/responce")]
        public IActionResult GetResponceCashed()
        {

            return Ok(DateTime.UtcNow);
        }


        [HttpGet("now/cahsed/memory")]
        public IActionResult GetMemoryCashed()
        {
            string cacheKey = "CurrentTime_InMemory";

            if (_memoryCache.TryGetValue(cacheKey, out var cachedTime))
            {
                return Ok(cachedTime);
            }

            var cashedDate = _memoryCache.GetOrCreate<DateTime>(cacheKey, entry =>
            {
                var currentTime = DateTime.UtcNow;
                entry.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(30);
                entry.SlidingExpiration = TimeSpan.FromSeconds(5);

                //entry.AddExpirationToken

                entry.Priority = CacheItemPriority.High;
                entry.SetSize(1);

                return currentTime;
            });
            
            return Ok(cashedDate);
        }

        [HttpGet("now/cahsed/distributed")]
        public async Task<IActionResult> GetDistributedCashed()
        {
            var redisKey = "CurrentTime_Distributed";

            string serializedTime = await _distributedCache.GetStringAsync(redisKey);
            if (serializedTime != null)
            {
                var time = JsonSerializer.Deserialize<DateTime>(serializedTime);
                return Ok(time);
            }

            var currentTime = DateTime.UtcNow;

            await _distributedCache.SetStringAsync(
                key: redisKey, 
                value: JsonSerializer.Serialize(currentTime), 
                options: new DistributedCacheEntryOptions
                {

                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(60),
                    SlidingExpiration = TimeSpan.FromSeconds(10)
                });

            return Ok(currentTime);
        }

    }
}