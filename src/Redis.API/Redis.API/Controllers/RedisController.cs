using Microsoft.AspNetCore.Mvc;
using Redis.Infrastructure.Model;
using Redis.Infrastructure.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Redis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RedisController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public RedisController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpPost]
        public IActionResult AddRedisData([FromBody]ModelDto modelDto)
        {
            if (!_cacheService.Any(modelDto.Email))
            {
                _cacheService.Add(modelDto.Email, modelDto);
                return Ok("Ok");
            }
            return BadRequest();
           
        }

        [HttpGet]
        public IActionResult GetRedisData(string key)
        {
            var user = _cacheService.Get<ModelDto>(key);
            return Ok(user);

        }
        [HttpDelete]
        public IActionResult DeleteRedisRecord(string id)
        {
            _cacheService.Remove(id);
            return Ok();
        }



    }
}
