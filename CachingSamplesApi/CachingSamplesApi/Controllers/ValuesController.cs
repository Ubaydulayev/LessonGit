using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CachingSamplesApi.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CachingSamplesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private DatabaseService _databaseService;

        private readonly IMemoryCache _memoryCache;

        private readonly IDistributedCache _distributedCache;

        public ValuesController(
            DatabaseService databaseService,
            IMemoryCache memoryCache,
            IDistributedCache distributedCache)
        {
            _databaseService = databaseService;
            _memoryCache = memoryCache;
            _distributedCache = distributedCache;
        }

        [HttpPost]
        public IActionResult SaveData(string data)
        {
            var key = Guid.NewGuid().ToString();
            //_databaseService.Data.Add(key, data);
            _memoryCache.Set(key, data);

            _memoryCache.GetOrCreate(key, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(15);

                return data;
            });

            //_distributedCache.Set(key, System.Text.Encoding.UTF8.GetBytes(data),
            //    new DistributedCacheEntryOptions()
            //    {
            //        //AbsoluteExpiration = DateTime.Parse("12-12-2022"),
            //        //AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1),
            //        SlidingExpiration = TimeSpan.FromSeconds(15)
            //    });

            return Ok(key);
        }

        [HttpGet]
        public IActionResult GetData(string key)
        {
            //var value = _databaseService.Data[key];

            var value = _memoryCache.Get(key);

            //if (_memoryCache.TryGetValue(key, out var value))
            //{
            //    return Ok(value);
            //}

            //var bytes = _distributedCache.Get(key);

            //var data = System.Text.Encoding.UTF8.GetString(bytes);

            return Ok(value);
        }
    }
}

