using Newtonsoft.Json;
using Redis.Infrastructure.Configurations;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Infrastructure.Redis
{
    public class RedisCacheManager : ICacheService
    {
        private readonly IRedisConnectionFactory _connection;
        private readonly IDatabase _database;

        public RedisCacheManager(IRedisConnectionFactory connection)
        {
            _connection = connection;
            _database = _connection.Connection().GetDatabase(0);//içine yazılacak rakam o database'i getirir. bunlar resp.app içinde var.
        }

        public void Add<T>(string key, T data)
        {
            string jsonData = JsonConvert.SerializeObject(data);    
            _database.StringSet(key, jsonData);
        }

        public bool Any(string key)
        {
            return _database.KeyExists(key);
        }

        public void Clear()
        {
            _database.Multiplexer.GetServer("http://localhost:6379").FlushDatabase();
        }

        public T Get<T>(string key)
        {
            if(!Any(key)) return default;
            string stringData = _database.StringGet(key);
            return JsonConvert.DeserializeObject<T>(stringData);    
        }

        public void Remove(string key)
        {
            _database.KeyDelete(key);
        }
    }
}
