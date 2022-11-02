using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Infrastructure.Redis
{
    public interface ICacheService
    {
        T Get<T>(string key);
        void Add<T>(string key,T data); // void Add(string key,object data);
        void Remove(string key);
        void Clear();
        bool Any(string key);
    }
}
