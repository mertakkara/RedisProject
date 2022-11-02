using Jose;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Redis.Infrastructure.Model;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Infrastructure.Configurations
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Urls urls;
        private readonly Lazy<ConnectionMultiplexer> _connection;

        public RedisConnectionFactory(IOptions<Urls> url)
        {
            urls = url.Value;
            _connection = new Lazy<ConnectionMultiplexer>(()=> ConnectionMultiplexer.Connect(urls.Url));
        }

        public ConnectionMultiplexer Connection()
        {
           return _connection.Value;
        }
    }
}
